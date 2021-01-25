using WordLadder.DependencyInjection;
using Unity;

namespace WordLadder
{
    public static class Bootstrapper
    {
        internal static IUnityContainer Container { get; set; }


        public static void Initialize()
        {
            Container = BuildUnityContainer();
        }

        /// <summary>
        /// Register the Types
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterTypes(IUnityContainer container)
        {
            UnityConfig.Register(container);
        }

        /// <summary>
        /// Build the Unity Container
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer BuildUnityContainer()
        {
            IUnityContainer container = new UnityContainer();

            InjectFactory.SetContainer(container.CreateChildContainer());

            RegisterTypes(container);

            return container;
        }
    }
}
