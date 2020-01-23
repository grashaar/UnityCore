using System.Collections.Generic;

namespace System
{
    public static class SingleInstance
    {
        private readonly static Dictionary<Type, object> _instances
            = new Dictionary<Type, object>();

        public static void Set<T>(T instance) where T : class
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            var type = typeof(T);

            if (!_instances.ContainsKey(type))
            {
                _instances.Add(type, instance);
            }
        }

        public static T Get<T>() where T : class
        {
            var type = typeof(T);

            if (!_instances.ContainsKey(type))
                throw new InvalidOperationException($"No instance of type {type} has been set.");

            return _instances[type] as T;
        }

        public static T Of<T>() where T : class, new()
        {
            var type = typeof(T);

            if (!_instances.ContainsKey(type))
            {
                _instances.Add(type, new T());
            }

            return _instances[type] as T;
        }
    }
}
