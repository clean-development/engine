using System;

namespace Components
{
	public interface StateManager
	{
		EntityState GetOrAdd(Type entityType);
	}
}
