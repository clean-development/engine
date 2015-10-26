namespace EntityComponentSystem
{
	public interface EntityState
	{
		bool Add<Component>(Component component);
		Component Get<Component>();
		bool Remove<Component>(Component component);
	}
}
