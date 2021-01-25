using WordLadder.Api;
using WordLadder.Infrastructure;
using Unity;

namespace WordLadder.DependencyInjection
{
    public class UnityConfig
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IFileOperator, FileOperator>();
            container.RegisterType<IWordCalculator<IWord>, WordCalculator>();

            InjectFactory.SetContainer(container);
        }
    }
}
