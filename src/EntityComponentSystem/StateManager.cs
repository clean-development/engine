using System;

namespace EntityComponentSystem
{
	public interface StateManager
	{
		EntityState GetOrAdd<Entity>(Entity entity);
		EntityState Get<Entity>(Entity entity);
	}
}
