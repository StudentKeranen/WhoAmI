﻿namespace Presentationlayer.WPF.Services
{
    public interface IWindowService
    {
        void Show<TViewModel>(TViewModel model);
        bool ShowDialog<TViewModel>(TViewModel model);
    }
}