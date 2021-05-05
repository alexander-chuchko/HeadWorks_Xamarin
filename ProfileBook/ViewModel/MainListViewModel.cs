using Prism.Navigation;
using ProfileBook.Model;
using ProfileBook.View;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.ComponentModel;
using Prism.Services.Dialogs;
using ProfileBook.Service.Profile;
using Acr.UserDialogs;
using ProfileBook.Service.Authorization;
using ProfileBook.Extension;
using ProfileBook.Service.Theme;
using ProfileBook.Enum;
using ProfileBook.Resource;

namespace ProfileBook.ViewModel
{
    public class MainListViewModel:BaseViewModel
    {
        #region---PrivateFields---
        private bool _isVisibleLabel;
        private readonly IDialogService _dialogService;
        private readonly IProfileService _profileService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IThemService _themService;
        private ObservableCollection<ProfileModel> _profilelList;
        private bool _isVisibleListView;
        private ProfileModel _profileModel;
        private string _nameCheckedButton;
        #endregion
        public MainListViewModel(INavigationService navigationService, IDialogService diulogService, IProfileService profileService, IAuthorizationService authorizationService, IThemService themService) :base(navigationService)
        {
            _authorizationService = authorizationService;
            _profileService = profileService;
            _dialogService = diulogService;
            _themService = themService;
             NavigationToSettingsView = new Command(ExecuteGoToSettingsPage);
            NavigationToAddProfileUser = new Command(ExecuteGoToAddProfileUser);
            RemoveCommand = new Command(RemoveModel);
            UpdateCommand = new Command(UpdateModel);
            NavigationToSingIn = new Command(ExecuteGoBack);
            ProfileList = new ObservableCollection<ProfileModel>();
        }
        #region---PublicProperties---
        public ICommand NavigationToSettingsView { get; set; }
        public ICommand NavigationToAddProfileUser { get; set; }
        public ICommand NavigationToSingIn { get; set; }
        public ICommand RemoveCommand { set; get; }
        public ICommand UpdateCommand { set; get; }
        public string NameIsChecked
        {
            set
            {
                SetProperty(ref _nameCheckedButton, value);
            }
            get
            {
                return _nameCheckedButton;
            }
        }
        public ProfileModel ProfileModel
        {
            set
            {
                SetProperty(ref _profileModel, value);
            }
            get
            {
                return _profileModel;
            }
        }
        public bool IsVisableListView
        {
            get
            {
                return _isVisibleListView;
            }
            set
            {
                SetProperty(ref _isVisibleListView, value);
            }
        }
        public bool IsVisableLabel
        {
            get
            {
                return _isVisibleLabel;
            }
            set
            {
                SetProperty(ref _isVisibleLabel, value);
            }
        }
        public ObservableCollection<ProfileModel> ProfileList
        {
            get
            {
                return _profilelList;
            }
            set
            {
                SetProperty(ref _profilelList, value);
            }
        }
        #endregion
        #region---Methods---
        private async void ExecuteGoToAddProfileUser()
        {
            await _navigationService.NavigateAsync(($"{ nameof(AddEditProfilePage)}"));
        }
        private async void ExecuteGoToSettingsPage()
        {
            await _navigationService.NavigateAsync($"{ nameof(Settings)}");
        }
        private async void UpdateModel(object selectObject)
        {
            ProfileModel profileModel = selectObject as ProfileModel;
            if (profileModel != null)
            {
                var parametr = new NavigationParameters();
                parametr.Add("ProfileUser", profileModel);
                await _navigationService.NavigateAsync(($"{ nameof(AddEditProfilePage)}"), parametr);
            }
        }
        private void DeletingCurrentUserSettings() //When logging out, delete all user settings
        {
            _profileService.DeleteAllSortSettings();
            _authorizationService.RemoveIdCurrentUser();
            _themService.RemoveThemeDark();
            //_themService.ChangeTheme(EnumSet.Theme.Light);
        }
        private async void ExecuteGoBack()
        {
            DeletingCurrentUserSettings();
            await _navigationService.NavigateAsync($"/{ nameof(NavigationPage)}/{ nameof(MainPage)}");
        }
        private async void RemoveModel(object selectObject)
        {
            ProfileModel profileModel = selectObject as ProfileModel;
            if (profileModel != null)
            {
                var confirmConfig = new ConfirmConfig()
                {
                    Message = AppResource.Youreallywanttodeletethisprofile,
                    OkText = AppResource.Delete,
                    CancelText = AppResource.Cancel
                };
                var result = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                if (result)
                {
                    await _profileService.RemoveProfileModelAsync(profileModel);
                    ProfileList.Remove(profileModel);
                }
            }
        }
        #region---PublicMethods---

        #endregion
        public void OnShowDialogExecuted()
        {
            _dialogService.ShowDialog("PopupsContent", new DialogParameters
            {
                {"path", ProfileModel.ImageSource}
            });
        }
        #endregion
        #region---OverloadedMethods---
        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            var listOfUsers = await _profileService.GetAllProfileModelAsync();
            if (listOfUsers.Count == 0)
            {
                IsVisableLabel = true;
                IsVisableListView = false;
            }
            else
            {
                IsVisableLabel = false;
                IsVisableListView = true;
                var resultsOfSelectingProfilesById = listOfUsers.Where(x => x.UserId == _authorizationService.GetIdCurrentUser());
                if(_profileService.GetValueToSortByName())
                {
                    ProfileList.AddRange(resultsOfSelectingProfilesById.OrderBy(x => x.Name).ToList());
                }
                else if(_profileService.GetValueSortByNickName())
                {
                    ProfileList.AddRange(resultsOfSelectingProfilesById.OrderBy(x => x.NickName).ToList());
                }
                else if(_profileService.GetValueSortByDateAddedToDatabase())
                {
                    ProfileList.AddRange(resultsOfSelectingProfilesById.OrderBy(x => x.MomentOfRegistration).ToList());;
                }
                else
                {
                    ProfileList.AddRange(resultsOfSelectingProfilesById);
                }
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters) { }
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(ProfileModel))
            {
                OnShowDialogExecuted();
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.Count != 0)
            {
                NameIsChecked = parameters.GetValue<string>("NameIsChecked");
            }
        }
        #endregion
    }
}
