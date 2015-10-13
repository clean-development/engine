using System;
using System.Collections.Concurrent;

namespace Components
{
	internal sealed class StateManageComponentSystem : ComponentSystem
	{
		private static ConcurrentDictionary<Type, ConcurrentDictionary<Type, object>> _state
			= new ConcurrentDictionary<Type, ConcurrentDictionary<Type, object>>();

		public void Assign<Entity, Component>(Entity entity, Component component)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			if (component == null)
				throw new ArgumentNullException(nameof(component));

			var entityType = typeof(Entity);
			var componentType = typeof(Component);
			var entityState = _state.GetOrAdd(entityType, type => new ConcurrentDictionary<Type, object>());
			if (!entityState.TryAdd(componentType, component))
				throw new ComponentAlreadyAssignedException(entityType, componentType);
		}
	}
}
