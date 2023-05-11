using Presentationlayer.WPF.Commands;
using Presentationlayer.WPF.Models;
using Presentationlayer.WPF.Services;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Businesslayer;
using Entitylayer;
using System.Linq;

namespace Presentationlayer.WPF.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        WindowService windowService = new WindowService();
        DataController dataController = new DataController();

        
        private string loggedInUser = "#NA";
        public string LoggedInUser
        {   
            get { return loggedInUser; }
            set { loggedInUser = value; OnPropertyChanged(); }
        }
        
        private Visibility mainVisibility = Visibility.Visible;
        public Visibility MainVisibility
        {
            get { return mainVisibility; }
            set { mainVisibility = value; OnPropertyChanged(); }
        }
        
        public MainViewModel()
        {
            MainVisibility = Visibility.Hidden;
            LogIn();
        }

        public void LogIn()
        {
            LoginViewModel logIn = new LoginViewModel();
            windowService.ShowDialog(logIn);
            if (SessionController.LoggedIn != null)
            {
                LoggedInUser = $"{SessionController.LoggedIn.UserId}";
                MainVisibility = Visibility.Visible;
            }
            else ExitCommand.Execute(true);
        }

        private Company companyInfo;
        public Company CompanyInfo
        {
            get { return companyInfo; }
            set { companyInfo = value; OnPropertyChanged(); }
        }

        private List<string[]> personalDataSet;
        public List<string[]> PersonalDataSet
        {
            get { return personalDataSet; }
            set { personalDataSet = value; OnPropertyChanged(); }
        }

        private ICommand fetchDataCommand = null!;
        public ICommand FetchDataCommand =>
            fetchDataCommand ??= fetchDataCommand = new RelayCommand(() =>
            {
                Company currentCompany;
                List<string[]> companyData = dataController.CheckForFile(out currentCompany);
                CompanyInfo = currentCompany;

                List<string[]> cipersData = dataController.FetchPersonalData();

                PersonalDataSet = cipersData.Concat(companyData).ToList();
            });

        private ICommand logOut = null!;
        public ICommand LogOut =>
            logOut ??= logOut = new RelayCommand(() =>
            {
                MainVisibility = Visibility.Hidden;
                SessionController.Terminate();
                LogIn();
            });

        private ICommand exitCommand = null!;
        public ICommand ExitCommand =>
        exitCommand ??= exitCommand = new RelayCommand(() => App.Current.Shutdown());
    }
}
