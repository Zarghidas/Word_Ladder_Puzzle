using Unity;

namespace WordLadder.DependencyInjection
{
    /// <summary>
    /// Class to Set the Unity Container and Resolve Dependencies
    /// </summary>
    public static class InjectFactory
    {
        private static IUnityContainer _container { get; set; }

        public static T Resolve<T>()
        {
            if (_container == null)
                return default(T);

            return _container.Resolve<T>();
        }

        public static void SetContainer(IUnityContainer container)
        {
            _container = container;
        }
    }
}
