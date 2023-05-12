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
using System.Windows;
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

        private ICommand logInCommand = null!;
        public ICommand LogInCommand =>
        logInCommand ??= logInCommand = new RelayCommand<ICloseable>((view) =>
        {
            try
            {
                SessionController.Instance(Username, Password);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Username = string.Empty;
                Password = string.Empty;
            }
            if (SessionController.LoggedIn != null) CloseCommand.Execute(view);
        });

        private ICommand closeCommand = null!;
        public ICommand CloseCommand => closeCommand ??= closeCommand = new RelayCommand<ICloseable>((closeable) =>
        {
            closeable.Close();
        });
    }
}
