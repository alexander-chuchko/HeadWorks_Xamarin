﻿using Prism.Mvvm;
using Prism.Navigation;

namespace ProfileBook.ViewModel
{
    public class BaseViewModel : BindableBase 
    {
        protected readonly INavigationService _navigationService;
        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
