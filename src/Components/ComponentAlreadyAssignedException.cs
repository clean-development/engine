using System;

namespace Components
{
	public class ComponentAlreadyAssignedException : Exception
	{
		private Type _entityType;
		private Type _componentType;

		public ComponentAlreadyAssignedException(Type entityType, Type componentType)
		{
			_entityType = entityType;
			_componentType = componentType;
		}
	}
}
