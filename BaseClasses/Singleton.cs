namespace SingletonPattern_Log_CRUD.BaseClasses
{
    public abstract class Singleton<T> where T : class
    {
        private static readonly Lazy<T> instance = new Lazy<T>(
            () => CreateInstance()
            );

        public static T Instance => instance.Value;

        protected Singleton()
        {
        }

        private static T CreateInstance()
        {
            return Activator.CreateInstance(typeof(T), nonPublic: true) as T;
        }
    }
}
