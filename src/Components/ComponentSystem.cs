namespace Components
{
	public interface ComponentSystem
	{
		void Assign<Entity, Component>(Entity entity, Component component);
		bool TryAssign<Entity, Component>(Entity entity, Component component);
	}
}
