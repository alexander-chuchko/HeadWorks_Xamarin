using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using System;

namespace ProfileBook.ViewModel
{
    class PopupImageViewModel:BindableBase, INavigatedAware
    {
        private string _pathImage;
        private ProfileModel _profileModel;
        private readonly INavigationService _navigationService;

        public PopupImageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
            if(parameters.Count!=0)
            {
                ProfileModel = parameters.GetValue<ProfileModel>("ProfileBook");
                PathImage = ProfileModel.ImageSource;
            }
        }
    }
}

#if false
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using System;

namespace ProfileBook.ViewModel
{
    class PopupImageViewModel:BindableBase, INavigatedAware
    {
        private string _pathImage;
        private ProfileModel _profileModel;
        private readonly INavigationService _navigationService;

        public PopupImageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
            if(parameters.Count!=0)
            {
                ProfileModel = parameters.GetValue<ProfileModel>("ProfileBook");
                PathImage = ProfileModel.ImageSource;
            }
        }
    }
}
#endif
