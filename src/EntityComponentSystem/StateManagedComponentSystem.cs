using System;

namespace EntityComponentSystem
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

		public bool Assign<Entity, Component>(Entity entity, Component component)
		{
			if (entity == null || component == null) return false;

			return _state.GetOrAdd(entity).Add(component);
		}

		public EntityState Get<Entity>(Entity entity)
			=> _state.Get(entity);

		public bool Unassign<Entity, Component>(Entity entity, Component component)
		{
			if (entity == null || component == null) return false;

			return _state.Get(entity)?.Remove(component) ?? false;
		}
	}
}
