using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using ProfileBook.Helpers;
using ProfileBook.Model.Pfofile;
using System;

namespace ProfileBook.Dialogs
{
    public class PopupsContentViewModel : BindableBase, IDialogAware
    {
        private string _pathImage;
        private ProfileModel _profileModel;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService; //{ get; }

        public PopupsContentViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            CloseCommand = new DelegateCommand(() => RequestClose?.Invoke(null));
        }
        public DelegateCommand CloseCommand { get; }
        
        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }
        public void OnDialogClosed()
        {
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            PathImage = parameters.GetValue<string>(ListOfNames.pathSelectedPicture);
        }
        public ProfileModel ProfileModel
        {
            set { SetProperty(ref _profileModel, value); }
            get { return _profileModel; }
        }
        public string PathImage
        {
            set { SetProperty(ref _pathImage, value); }
            get { return _pathImage; }
        }
    }
}
