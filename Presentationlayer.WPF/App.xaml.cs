using Presentationlayer.WPF.Services;
using Presentationlayer.WPF.ViewModels;
using Presentationlayer.WPF.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            Startup += (s, e) =>
            {
                WindowService.RegisterWindow<LoggaInViewModel, InloggningWindow>();
            };
        }
}
}
