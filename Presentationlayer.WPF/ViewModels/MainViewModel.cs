﻿using Presentationlayer.WPF.Commands;
using Presentationlayer.WPF.Models;
using Presentationlayer.WPF.Services;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Presentationlayer.WPF.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        WindowService windowService = new WindowService();
        private string inloggad = "#NA";
        public string Inloggad
        {   
            get { return inloggad; }
            set { inloggad = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            MainVisibility = Visibility.Hidden;
            Loggain();
        }

        public void Loggain()
        {
            LoggaInViewModel loggaIn = new LoggaInViewModel();
            windowService.ShowDialog(loggaIn);
            if (SessionController.Inloggad != null)
            {
                Inloggad = $"{SessionController.Inloggad.Förnamn}";
                MainVisibility = Visibility.Visible;
            }
            else ExitCommand.Execute(true);
        }

        private Visibility mainVisibility = Visibility.Visible;
        public Visibility MainVisibility
        { 
            get { return mainVisibility; } 
            set { mainVisibility = value; OnPropertyChanged(); } 
        }

        private ICommand loggaut = null!;
        public ICommand Loggaut =>
            loggaut ??= loggaut = new RelayCommand(() =>
            {
                MainVisibility = Visibility.Hidden;
                SessionController.Terminate();
                Loggain();
            });

        private ICommand exitCommand = null!;
        public ICommand ExitCommand =>
        exitCommand ??= exitCommand = new RelayCommand(() => App.Current.Shutdown());
    }
}
