using BeTaskManagement.Data;
using BeTaskManagement.ViewModels;
using BeTaskManagement.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BeTaskManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));



            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<BeTaskViewModel>();
            services.AddTransient<CommentViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>()
            };
            mainWindow.Show();
        }
    }

}
