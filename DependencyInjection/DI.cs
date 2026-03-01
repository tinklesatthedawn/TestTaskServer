using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    public class DI
    {
        private static IServiceCollection services = new ServiceCollection();
           
        public static void AddTransient<T, K>() where T : class where K : class, T
        {
            services.AddTransient<T, K>();
        }

        public static void AddTransient<T>() where T : class
        {
            services.AddTransient<T>();
        }

        public static K Get<K>()
        {
            var result = services.BuildServiceProvider().GetService<K>();
            if (result != null) 
            {
                return result;
            }
            throw new NullReferenceException();
        }
    }
}
