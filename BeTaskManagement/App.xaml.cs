using BeTaskManagement.Data;
using BeTaskManagement.ViewModels;
using BeTaskManagement.Views;
using Microsoft.EntityFrameworkCore;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Data Source=MARIA;Initial Catalog=mmdb_3;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=True"));



            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<BeTaskViewModel>();
            services.AddTransient<CommentViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            //var mainWindow = new CommentView();
            //var mainWindow = new BeTaskView();
            var mainWindow = new MainWindow();
            //mainWindow.DataContext = ServiceProvider.GetRequiredService<CommentViewModel>();
            //mainWindow.DataContext = ServiceProvider.GetRequiredService<BeTaskViewModel>();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindow.Show();
        }
    }

}
