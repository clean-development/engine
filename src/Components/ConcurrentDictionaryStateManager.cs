using System;
using System.Collections.Concurrent;

namespace Components
{
	internal sealed class ConcurrentDictionaryStateManager : StateManager
	{
		private readonly ConcurrentDictionary<object, EntityState> _entityState
			= new ConcurrentDictionary<object, EntityState>();

		public EntityState GetOrAdd<Entity>(Entity entity)
			=> _entityState.GetOrAdd(entity, Create);

		public EntityState Get<Entity>(Entity entity)
		{
			EntityState entityState;
			if (!_entityState.TryGetValue(entity, out entityState))
				return null;
			return entityState;
		}

		private EntityState Create(object entity)
			=> new ConcurrentDictionaryEntityState(entity);

		private sealed class ConcurrentDictionaryEntityState : EntityState
		{
			private readonly object _entity;
			private readonly ConcurrentDictionary<Type, object> _componentState
				= new ConcurrentDictionary<Type, object>();

			public ConcurrentDictionaryEntityState(object entity)
			{
				_entity = entity;
			}

			public bool Add<Component>(Component component)
				=> _componentState.TryAdd(typeof(Component), component);

			public Component Get<Component>()
			{
				object componentObject = null;
				if (_componentState.TryGetValue(typeof(Component), out componentObject))
					return (Component)componentObject;
				return default(Component);
			}

			public bool Remove<Component>(Component component)
			{
				object _;
				return _componentState.TryRemove(typeof(Component), out _);
			}
		}
	}
}
