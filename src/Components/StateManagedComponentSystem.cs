using System;

namespace Components
{
	internal sealed class StateManagedComponentSystem : ComponentSystem
	{
		private readonly StateManager _state;

		public StateManagedComponentSystem(StateManager state)
		{
			if (state == null)
				throw new ArgumentNullException(nameof(state));
			_state = state;
		}

		public void Assign<Entity, Component>(Entity entity, Component component)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			if (component == null)
				throw new ArgumentNullException(nameof(component));

			var entityType = typeof(Entity);
			var componentType = typeof(Component);
			if (!TryAssign(entityType, componentType, component))
				throw new ComponentAlreadyAssignedException(entityType, componentType);
		}

		public bool TryAssign<Entity, Component>(Entity entity, Component component)
		{
			if (entity == null || component == null) return false;

			return TryAssign(typeof(Entity), typeof(Component), component);
		}

		private bool TryAssign(Type entityType, Type componentType, object component)
			=> _state
				.GetOrAdd(entityType)
				.TryAdd(component);
	}
}
