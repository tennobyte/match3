namespace ECS_Engine
{
    abstract class Component: IComponent
    {
        public Entity Entity { get; set; }
    }
}
