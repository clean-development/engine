using System;

namespace Components
{
	public interface StateManager
	{
		EntityState GetOrAdd<Entity>(Entity entity);
		EntityState Get<Entity>(Entity entity);
	}
}
