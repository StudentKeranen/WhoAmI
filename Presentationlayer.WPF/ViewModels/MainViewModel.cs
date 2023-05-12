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
using System.Collections;

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

        private IEnumerable personalDataSet;
        public IEnumerable PersonalDataSet
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

                var query =
                    (from data1 in cipersData
                     join data2 in companyData on data1[0] equals data2[0] into Data
                     from data3 in Data.DefaultIfEmpty()
                     select new
                     {
                         PersonalInformation = data1[0],
                         CipersData = data1[1],
                         CompanyData = data3?[1] ?? string.Empty
                     })
                    .Union(from data1 in companyData
                           join data2 in cipersData on data1[0] equals data2[0] into Data
                           from data3 in Data.DefaultIfEmpty()
                           select new
                           {
                               PersonalInformation = data1[0],
                               CipersData = data3?[1] ?? string.Empty,
                               CompanyData = data1[1]
                           });

                PersonalDataSet = query;
            });

        private ICommand logOut = null!;
        public ICommand LogOut =>
            logOut ??= logOut = new RelayCommand(() =>
            {
                MainVisibility = Visibility.Hidden;
                SessionController.Terminate();
                PersonalDataSet = null!;
                CompanyInfo = null!;
                LogIn();
            });

        private ICommand exitCommand = null!;
        public ICommand ExitCommand =>
        exitCommand ??= exitCommand = new RelayCommand(() => App.Current.Shutdown());
    }
}
