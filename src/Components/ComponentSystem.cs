namespace Components
{
	public interface ComponentSystem
	{
		bool Assign<Entity, Component>(Entity entity, Component component);
		EntityState Get<Entity>(Entity entity);
	}
}
