using System.Collections.Generic;

namespace System
{
    public static class SingleInstance
    {
        private readonly static Dictionary<Type, object> _instances
            = new Dictionary<Type, object>();

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
