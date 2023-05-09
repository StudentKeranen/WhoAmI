using Businesslayer;
using Presentationlayer.WPF.Commands;
using Presentationlayer.WPF.Models;
using Presentationlayer.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;

namespace Presentationlayer.WPF.ViewModels
{
    class LoginViewModel: ObservableObject
    {
        private string username;
        public string Username 
        {
            get { return username; }
            set { username = value; OnPropertyChanged(); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private ICommand loginCommand = null!;
        public ICommand LoginCommand =>
        loginCommand ??= loginCommand = new RelayCommand<ICloseable>((view) =>
        { 
            SessionController.Instance(Username, Password);
            if (SessionController.LoggedIn != null) CloseCommand.Execute(view);
            else App.Current.Shutdown();
        });

        private ICommand closeCommand = null!;
        public ICommand CloseCommand => closeCommand ??= closeCommand = new RelayCommand<ICloseable>((closeable) =>
        {
            closeable.Close();
        });
    }
}
