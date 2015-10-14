namespace Components
{
	public interface EntityState
	{
		bool TryAdd<Component>(Component component);
		Component Get<Component>();
	}
}
