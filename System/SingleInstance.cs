namespace System
{
    [Obsolete("This class is deprecated. Use Singleton instead.")]
    public static class SingleInstance
    {
        public static void Set<T>(T instance) where T : class
            => Singleton.Set(instance);

        public static T Get<T>() where T : class
            => Singleton.Get<T>();

        public static T Of<T>() where T : class, new()
            => Singleton.Of<T>();
    }
}
