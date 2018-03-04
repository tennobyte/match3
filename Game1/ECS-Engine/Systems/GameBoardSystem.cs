using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private int verticalElementsCount;
        private int horizontalElementsCount;
        private int spacing;
        private Entity[,] gems;
        private Random random;
        private bool isAbleToCheck;
        private Vector2 initialPosition;

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
            isAbleToCheck = false;


            random = new Random();
            gems = new Entity[horizontalElementsCount, verticalElementsCount];
            Scene currentScene = ECS.Instance.GetCurrentScene();

            //initialising gems
            for (int y = 0; y < verticalElementsCount; y++)
            {
                for (int x = 0; x < horizontalElementsCount; x++)
                {
                    Entity gem = new Entity("gem:" + y + "_" + x);
                    gem.AddComponents(new Transform(new Vector2((int)initialPosition.X + x * 64,
                                                    (int)initialPosition.Y +  y * 64), 0.4f, 0), 
                        new GameboardElement(new Point (x,y)), 
                        new SpriteRenderer(),
                        new Animator());
                    ChangeGemTypeRandomly(gem);
                    ECS.Instance.GetCurrentScene().AddEntity(gem);
                    gameboardEntity.AddChild(gem);
                    gems[x, y] = gem;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (isAbleToCheck)
            {
                var matchingElements = FindAllMatchingElements();
                RespawnElements(matchingElements);
                isAbleToCheck = false;
            }
            else
            {
                //gems.Cast<Entity>().ToList(); //too slow!
                for (int i = 0; i < verticalElementsCount; i++)
                {
                    for (int j = 0; j < horizontalElementsCount; j++)
                    {
                        if(gems[i,j].GetComponent<Animator>().IsMoving)
                        {
                            Console.WriteLine("Something is moving!");
                            return;
                        }
                    }
                }
                isAbleToCheck = true;
            }
        }

        private void RespawnElements(List<Entity> elementsToRespawn)
        {
            
            for (int i = 0; i < horizontalElementsCount; i++)
            {
                List<Entity> columnToRespawn = elementsToRespawn.Where(e => 
                                    e.GetComponent<GameboardElement>().BoardPosition.X == i).ToList();
                int offsetCounter = columnToRespawn.Count - 1;

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

                    element.GetComponent<Transform>().SetPosition(new Vector2((int)initialPosition.X + i * 64,
                                                            (int)initialPosition.Y - (offsetCounter+1) * 64));

                    var animatorComponent = element.GetComponent<Animator>();
                    animatorComponent.ToggleMoving();
                    animatorComponent.SetTargetPosition(new Vector2((int)(initialPosition.X + i * 64),
                                                        (int)initialPosition.Y + offsetCounter * 64));

                    ChangeGemTypeRandomly(element);

                    gems[elementComponent.BoardPosition.X, elementComponent.BoardPosition.Y] = element;

                    offsetCounter--;
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
            animator.SetTargetPosition(new Vector2((int)(initialPosition.X + elementComponent.BoardPosition.X * 64),
                                           (int)initialPosition.Y + elementComponent.BoardPosition.Y * 64));
        }

        private void ChangeGemTypeRandomly(Entity gem)
        {
            GameboardElement gameboardElement = gem.GetComponent<GameboardElement>();
            SpriteRenderer spriteRenderer = gem.GetComponent<SpriteRenderer>();
            int gemsSize = Enum.GetNames(typeof(Gems)).Length;
            gameboardElement.GemType = (Gems) random.Next(0, gemsSize);
            spriteRenderer.Texture = ContentHolder.gemTextures[gameboardElement.GemType];
        }

        private void SwapElements(Entity gem1, Entity gem2)
        {

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

            List<Entity> result = columnMatches.Concat(rowMatches).Distinct().ToList();

            return result;
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
