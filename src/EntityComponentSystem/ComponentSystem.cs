namespace EntityComponentSystem
{
	public interface ComponentSystem
	{
		bool Assign<Entity, Component>(Entity entity, Component component);
		EntityState Get<Entity>(Entity entity);
		bool Unassign<Entity, Component>(Entity entity, Component component);
	}
}
