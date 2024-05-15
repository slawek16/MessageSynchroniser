using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using WpfClient2.HostBuilders;
using WpfClient2.Services;
using WpfClient2.ViewModels;

namespace WpfClient2
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
				.AddViewModels()
				.ConfigureServices((hostContext, services) =>
				{
					services.AddSingleton<ISignalRService, SignalRService>();
					services.AddSingleton(s => new MainWindow()
					{
						DataContext = s.GetRequiredService<MainViewModel>()
					});
				})
				.Build();
        }

		protected override void OnStartup(StartupEventArgs e)
		{
			_host.Start();
			MainWindow = _host.Services.GetService<MainWindow>();
			MainWindow.Show();
			base.OnStartup(e);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			_host.Dispose();

			base.OnExit(e);
		}
	}

}
