using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class ShowImageViewModel:BindableBase, INavigatedAware
    {
        private string _pathImage;
        private ProfileModel _profileModel;
        private readonly INavigationService _navigationService;
        private readonly ICommand command;

        public ShowImageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            command = new Command(ReturnOnNavigation);
        }
        public ProfileModel ProfileModel
        {
            set => SetProperty(ref _profileModel, value);
            get => _profileModel;
        }
        public string PathImage
        {
            set => SetProperty(ref _pathImage, value);
            get => _pathImage;
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.Count != 0)
            {
                ProfileModel = parameters.GetValue<ProfileModel>("ProfileBook");
                PathImage = ProfileModel.ImageSource;
            }
        }

        public async void ReturnOnNavigation()
        {
            await _navigationService.NavigateAsync("MainList");
            //_navigationService.GoBackAsync();
        }
    }
}
