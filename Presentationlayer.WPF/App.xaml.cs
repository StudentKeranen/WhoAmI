using Presentationlayer.WPF.Services;
using Presentationlayer.WPF.ViewModels;
using Presentationlayer.WPF.Views;
using System.Windows;

namespace Presentationlayer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += (s, e) => //ta bort denna sen
            {
                WindowService.RegisterWindow<LoginViewModel, LoginWindow>();
            };

            Datalayer.AppDbContext dbContext = new Datalayer.AppDbContext();
            dbContext.seed();
        }
    }
}
