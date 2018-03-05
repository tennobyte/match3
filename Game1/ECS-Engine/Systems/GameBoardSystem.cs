using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine
{
    class GameboardSystem: System
    {
        private Entity gameboardEntity;
        private Entity score;
        private Entity timer;

        private int verticalElementsCount;
        private int horizontalElementsCount;
        private int spacing;
        private Entity[,] gems;
        private Random random;
        private Vector2 initialPosition;
        private GameState gameState;
        private Entity firstClickedElement;
        private Entity secondClickedElement;
        private bool isSwapping;

        private enum GameState: int
        {
            CheckingUnavaliable,
            WaitingForInput,
            CheckingMatches,
            Fading,
            Respawning,
            GameOver
        }

        public GameboardSystem()
            : base(typeof(Transform), typeof(Gameboard))
        {

        }


        public override void Init()
        {
            gameboardEntity = Scene.Entities.FirstOrDefault(e => e.HasExactComponents(CompatibleTypes));

            verticalElementsCount = gameboardEntity.GetComponent<Gameboard>().verticalElementsCount;
            horizontalElementsCount = gameboardEntity.GetComponent<Gameboard>().horizontalElementsCount;
            spacing = gameboardEntity.GetComponent<Gameboard>().spacing;
            initialPosition = gameboardEntity.GetComponent<Transform>().Position;
            gameState = GameState.CheckingMatches;

            isSwapping = false;


            random = new Random();
            gems = new Entity[horizontalElementsCount, verticalElementsCount];
            Scene currentScene = ECS.Instance.GetCurrentScene();

            //initialising gems
            for (int y = 0; y < verticalElementsCount; y++)
            {
                for (int x = 0; x < horizontalElementsCount; x++)
                {
                    Entity gem = new Entity("gem:" + y + "_" + x);
                    gem.AddComponents(new Transform(new Vector2((int)initialPosition.X + x * spacing/2,
                                                    (int)initialPosition.Y +  y * spacing/2), 0.4f, 0), 
                        new GameboardElement(new Point (x,y)), 
                        new SpriteRenderer(),
                        new Animator(),
                        new Collider());
                    ChangeGemTypeRandomly(gem);
                    gem.GetComponent<Collider>().Rectangle = new Rectangle((int)(initialPosition.X + x * spacing / 2 - gem.GetComponent<SpriteRenderer>().Texture.Width / 2 * gem.GetComponent<Transform>().Scale),
                                                                            (int)(initialPosition.Y + y * spacing / 2 - gem.GetComponent<SpriteRenderer>().Texture.Height / 2 * gem.GetComponent<Transform>().Scale),
                                                                             spacing/2, spacing/2);
                    ECS.Instance.GetCurrentScene().AddEntity(gem);
                    gameboardEntity.AddChild(gem);
                    gems[x, y] = gem;
                }
            }

            Entity gameBoardTile = new Entity("tile");
            Texture2D tileTexture = ContentHolder.boardTile;
            gameBoardTile.AddComponents(new Transform(new Vector2(initialPosition.X, initialPosition.Y), 0.5f, 0), 
                new SpriteRenderer(tileTexture, tileTexture.Width / 2, 8, 8, 0.1f));
            Scene.AddEntity(gameBoardTile);

            score = new Entity("score");
            score.AddComponents(new Transform(new Vector2(5, 60)), new TextRenderer("Score: ", ContentHolder.font, 0f), new Score());
            Scene.AddEntity(score);

            timer = new Entity("timer");
            timer.AddComponents(new Transform(new Vector2(5, 20)), new TextRenderer("Time left: ", ContentHolder.font, 0f), new Timer(60));
            Scene.AddEntity(timer);
        }

        public override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.CheckingUnavaliable:
                    if (!IsElementsMoving())
                    {
                        gameState = GameState.CheckingMatches;
                    }
                    break;
                case GameState.CheckingMatches:
                    var matchingElements = FindAllMatchingElements();
                    if (matchingElements.Count > 0)
                    {
                        SetFading(matchingElements);
                        gameState = GameState.Fading;
                        isSwapping = false;
                        firstClickedElement = null;
                        secondClickedElement = null;
                    }
                    else
                    {
                        if (isSwapping && firstClickedElement != null && secondClickedElement != null)
                        {
                            Console.WriteLine("swapping failed");
                            gameState = GameState.CheckingUnavaliable;
                            SwapElements(firstClickedElement, secondClickedElement);
                            isSwapping = false;
                            firstClickedElement = null;
                            secondClickedElement = null;
                            return;
                        }
                        gameState = GameState.WaitingForInput;
                        EnableColliders();
                    }
                    break;
                case GameState.WaitingForInput:
                    if (timer.GetComponent<Timer>().IsOutOfTime())
                    {
                        DisableColliders();
                        gameState = GameState.GameOver;
                        return;
                    }
                    HandleClickedElements();
                    if (firstClickedElement != null && secondClickedElement != null)
                    {
                        if (IsNeighbours(firstClickedElement, secondClickedElement))
                        {
                            DisableColliders();
                            Console.WriteLine("swappity swap");
                            isSwapping = true;
                            SwapElements(firstClickedElement, secondClickedElement);
                            gameState = GameState.CheckingUnavaliable;
                        }
                        else
                        {
                            firstClickedElement = null;
                            secondClickedElement = null;
                        }
                        return;
                    }
                    break;
                case GameState.Fading:
                    if (!IsElementsFading())
                    {
                        gameState = GameState.Respawning;
                    }
                    break;
                case GameState.Respawning:
                    RespawnElements(FindAllMatchingElements());
                    gameState = GameState.CheckingUnavaliable;
                    break;
                case GameState.GameOver:
                    
                    break;
                default:
                    gameState = GameState.CheckingUnavaliable;
                    break;
            }
            Console.WriteLine(gameState);
        }

        private void SwapElements(Entity element1, Entity element2)
        {
            var tempBoardPosition = element1.GetComponent<GameboardElement>().BoardPosition;

            element1.GetComponent<Animator>().SetTargetPosition(element2.GetComponent<Transform>().Position);
            element1.GetComponent<Animator>().ToggleMoving();
            element2.GetComponent<Animator>().SetTargetPosition(element1.GetComponent<Transform>().Position);
            element2.GetComponent<Animator>().ToggleMoving();

            element1.GetComponent<GameboardElement>().BoardPosition = element2.GetComponent<GameboardElement>().BoardPosition;
            gems[element1.GetComponent<GameboardElement>().BoardPosition.X, element1.GetComponent<GameboardElement>().BoardPosition.Y] = element1;
            element2.GetComponent<GameboardElement>().BoardPosition = tempBoardPosition;
            gems[element2.GetComponent<GameboardElement>().BoardPosition.X, element2.GetComponent<GameboardElement>().BoardPosition.Y] = element2;
        }

        private void EnableColliders()
        {
            for (int y = 0; y < verticalElementsCount; y++)
            {
                for (int x = 0; x < horizontalElementsCount; x++)
                {
                    gems[x, y].GetComponent<Collider>().IsActive = true;
                }
            }
        }

        private void DisableColliders()
        {
            for (int y = 0; y < verticalElementsCount; y++)
            {
                for (int x = 0; x < horizontalElementsCount; x++)
                {
                    gems[x, y].GetComponent<Collider>().IsActive = false;
                }
            }
        }

        private bool IsNeighbours(Entity firstElement, Entity secondElement)
        {
            var firstPosition = firstElement.GetComponent<GameboardElement>().BoardPosition;
            var secondPosition = secondElement.GetComponent<GameboardElement>().BoardPosition;
            var diffirence = firstPosition - secondPosition;
            Console.WriteLine(diffirence);
            if ((Math.Abs(diffirence.X) == 0 && Math.Abs(diffirence.Y) == 1) || (Math.Abs(diffirence.X) == 1 && Math.Abs(diffirence.Y) == 0))
            {
                Console.WriteLine("is neighbours");
                return true;
            }
            return false;
        }


        private void HandleClickedElements()
        {
            for (int i = 0; i < verticalElementsCount; i++)
            {
                for (int j = 0; j < horizontalElementsCount; j++)
                {
                    if (gems[i, j].GetComponent<Collider>().IsClicked)
                    {
                        if(firstClickedElement == null)
                        {
                            firstClickedElement = gems[i, j];
                            firstClickedElement.GetComponent<Collider>().ResetStatus();
                            firstClickedElement.GetComponent<Animator>().ToggleRotation();
                            secondClickedElement = null;
                            return;
                        }
                        else
                        {
                            if (firstClickedElement == gems[i, j])
                            {
                                firstClickedElement.GetComponent<Collider>().ResetStatus();
                                firstClickedElement.GetComponent<Animator>().ToggleRotation();
                                firstClickedElement = null;
                                return;
                            }
                            else
                            {
                                secondClickedElement = gems[i, j];
                                firstClickedElement.GetComponent<Animator>().ToggleRotation();
                                secondClickedElement.GetComponent<Collider>().ResetStatus();
                                return;
                            }
                        }
                    }
                }
            }
        }

        private bool IsElementsMoving()
        {
            for (int i = 0; i < verticalElementsCount; i++)
            {
                for (int j = 0; j < horizontalElementsCount; j++)
                {
                    if (gems[i, j].GetComponent<Animator>().IsMoving)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsElementsFading()
        {
            for (int i = 0; i < verticalElementsCount; i++)
            {
                for (int j = 0; j < horizontalElementsCount; j++)
                {
                    if (gems[i, j].GetComponent<Animator>().IsFading)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void SetFading(List<Entity> elements)
        {
            foreach(Entity element in elements){
                element.GetComponent<Animator>().ToggleFading();
            }
        }

        private void RespawnElements(List<Entity> elementsToRespawn)
        {
            score.GetComponent<Score>().IncreaseScore(elementsToRespawn.Count);
            for (int i = 0; i < horizontalElementsCount; i++)
            {
                List<Entity> columnToRespawn = elementsToRespawn.Where(e => 
                                    e.GetComponent<GameboardElement>().BoardPosition.X == i).ToList();
                int offsetCounter = 0;
                List<Entity> columnToCollapse = GetColumn(i).Except(columnToRespawn).ToList();
                foreach (Entity element in columnToCollapse)
                {
                    var elementComponent = element.GetComponent<GameboardElement>();
                    int count = columnToRespawn.Where(e => e.GetComponent<GameboardElement>().BoardPosition.Y > 
                                                                        elementComponent.BoardPosition.Y).Count();
                    MoveElementDown(element, count);
                }

                foreach (Entity element in columnToRespawn)
                {
                    var elementComponent = element.GetComponent<GameboardElement>();
                    elementComponent.BoardPosition = new Point(i, offsetCounter);
                    element.GetComponent<Transform>().SetPosition(new Vector2((int)initialPosition.X + i * spacing/2,
                                                            (int)initialPosition.Y  - (columnToRespawn.Count - offsetCounter ) * spacing/2));
                    var animatorComponent = element.GetComponent<Animator>();
                    animatorComponent.ToggleMoving();
                    animatorComponent.SetTargetPosition(new Vector2((int)(initialPosition.X + elementComponent.BoardPosition.X * spacing/2),
                                                        (int)initialPosition.Y  + elementComponent.BoardPosition.Y * spacing/2));
                    ChangeGemTypeRandomly(element);
                    gems[elementComponent.BoardPosition.X, elementComponent.BoardPosition.Y] = element;
                    offsetCounter++;
                }
            }
        }

        private void MoveElementDown(Entity element, int offset)
        {
            var elementComponent = element.GetComponent<GameboardElement>();
            elementComponent.MoveDownBy(offset);
            gems[elementComponent.BoardPosition.X, elementComponent.BoardPosition.Y] = element;
            var animator = element.GetComponent<Animator>();
            animator.ToggleMoving();
            animator.SetTargetPosition(new Vector2((int)(initialPosition.X + elementComponent.BoardPosition.X * spacing/2),
                                           (int)initialPosition.Y + elementComponent.BoardPosition.Y * spacing/2));
        }

        private void ChangeGemTypeRandomly(Entity gem)
        {
            GameboardElement gameboardElement = gem.GetComponent<GameboardElement>();
            SpriteRenderer spriteRenderer = gem.GetComponent<SpriteRenderer>();
            int gemsSize = Enum.GetNames(typeof(Gem)).Length;
            gameboardElement.GemType = (Gem) random.Next(0, gemsSize);
            spriteRenderer.Texture = ContentHolder.gemTextures[gameboardElement.GemType];
        }

        private List<Entity> FindAllMatchingElements()
        {
            List<Entity> columnMatches = new List<Entity>();
            List<Entity> rowMatches = new List<Entity>();
            for (int i = 0; i < verticalElementsCount; i++)
            {
                rowMatches.AddRange(FindMatches(GetRow(i)));
            }
            for (int j = 0; j < horizontalElementsCount; j++)
            {
                columnMatches.AddRange(FindMatches(GetColumn(j)));
            }
            return columnMatches.Concat(rowMatches).Distinct().ToList();
        }

        private List<Entity> GetRow(int rowIndex)
        {
            List<Entity> result = new List<Entity>();
            for (int i = 0; i < verticalElementsCount; i++)
            {
                result.Add(gems[i, rowIndex]);
            }
            return result;
        }

        private List<Entity> GetColumn(int columnIndex)
        {
            List<Entity> result = new List<Entity>();
            for (int i = 0; i < horizontalElementsCount; i++)
            {
                result.Add(gems[columnIndex, i]);
            }
            return result;
        }

        private List<Entity> FindMatches(List<Entity> entityList)
        {
            var query = Enumerable.Range(1, entityList.Count - 2).Where(n =>
              entityList[n].GetComponent<GameboardElement>().GemType ==
              entityList[n - 1].GetComponent<GameboardElement>().GemType &&
              entityList[n].GetComponent<GameboardElement>().GemType ==
              entityList[n + 1].GetComponent<GameboardElement>().GemType).ToList();

            List<Entity> result = new List<Entity>();
            foreach (int index in query)
            {
                result.AddRange(entityList.GetRange(index - 1, 3));
            }
            return result.Distinct().ToList();
        }
    }
}
