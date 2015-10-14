using System;
using System.Collections.Concurrent;

namespace Components
{
	internal sealed class ConcurrentDictionaryStateManager : StateManager
	{
		private readonly ConcurrentDictionary<Type, EntityState> _entityState
			= new ConcurrentDictionary<Type, EntityState>();

		public EntityState GetOrAdd(Type entityType)
			=> _entityState.GetOrAdd(entityType, Create);

		private EntityState Create(Type entityType)
			=> new ConcurrentDictionaryEntityState(entityType);

		private sealed class ConcurrentDictionaryEntityState : EntityState
		{
			private readonly Type _entityType;
			private readonly ConcurrentDictionary<Type, object> _componentState
				= new ConcurrentDictionary<Type, object>();

			public ConcurrentDictionaryEntityState(Type entityType)
			{
				_entityType = entityType;
			}

			public bool TryAdd(object component)
				=> _componentState.TryAdd(component.GetType(), component);
		}
	}
}
