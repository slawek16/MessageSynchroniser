using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfClient2.ViewModels;

namespace WpfClient2.HostBuilders
{
	public static class ViewModelsBuilderExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();
            });
            return hostBuilder;
        }
    }
}
