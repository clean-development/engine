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

			if (!TryAssignInternal(entity, component))
				throw new ComponentAlreadyAssignedException(typeof(Entity), typeof(Component));
		}

		public bool TryAssign<Entity, Component>(Entity entity, Component component)
		{
			if (entity == null || component == null) return false;

			return TryAssignInternal(entity, component);
		}

		private bool TryAssignInternal<Entity, Component>(Entity entity, Component component)
			=> _state
				.GetOrAdd(entity)
				.TryAdd(component);
	}
}
