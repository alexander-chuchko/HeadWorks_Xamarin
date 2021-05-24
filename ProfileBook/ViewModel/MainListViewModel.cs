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
using ProfileBook.Resource;
using ProfileBook.Service.Localization;
using ProfileBook.Helpers;
using ProfileBook.Dialogs;
using ProfileBook.Enum;
using System.Collections.Generic;
using ProfileBook.Model.Pfofile;

namespace ProfileBook.ViewModel
{
    public class MainListViewModel:BaseViewModel, IInitializeAsync
    {
        #region---PrivateFields---
        private bool _isVisibleLabel;
        private bool _isVisibleListView;
        private ProfileViewModel _profileViewModel;
        private ObservableCollection<ProfileViewModel> _profilelViewModelList;
        private readonly IThemService _themService;
        private readonly IDialogService _dialogService;
        private readonly IProfileService _profileService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILocalizationService _localizationService;
        #endregion
        public MainListViewModel(INavigationService navigationService, IDialogService diulogService, IProfileService profileService, IAuthorizationService authorizationService, IThemService themService, ILocalizationService localizationService) :base(navigationService)
        {
            _authorizationService = authorizationService;
            _profileService = profileService;
            _dialogService = diulogService;
            _themService = themService;
            _localizationService = localizationService;
             NavigationToSettingsView = new Command(ExecuteGoToSettingsPage);
            NavigationToAddProfileUser = new Command(ExecuteGoToAddProfileUser);
            RemoveCommand = new Command(RemoveModel);
            UpdateCommand = new Command(UpdateModel);
            NavigationToSingIn = new Command(ExecuteGoBack);
            ProfileViewModelList = new ObservableCollection<ProfileViewModel>();
        }
        #region---PublicProperties---
        public ICommand NavigationToSettingsView { get; set; }
        public ICommand NavigationToAddProfileUser { get; set; }
        public ICommand NavigationToSingIn { get; set; }
        public ICommand RemoveCommand { set; get; }
        public ICommand UpdateCommand { set; get; }

        public ProfileViewModel ProfileViewModel
        {
            set { SetProperty(ref _profileViewModel, value); }
            get { return _profileViewModel; }
        }
        public bool IsVisableListView
        {
            get{ return _isVisibleListView;}
            set{ SetProperty(ref _isVisibleListView, value);}
        }
        public bool IsVisableLabel
        {
            get{ return _isVisibleLabel;}
            set{ SetProperty(ref _isVisibleLabel, value);}
        }
        public ObservableCollection<ProfileViewModel> ProfileViewModelList
        {
            get { return _profilelViewModelList; }
            set { SetProperty(ref _profilelViewModelList, value); }
        }
        #endregion

        #region---Methods---
        private async void ExecuteGoToAddProfileUser()
        {
            await _navigationService.NavigateAsync(($"{ nameof(AddEditProfileView)}"));
        }
        private async void ExecuteGoToSettingsPage()
        {
            await _navigationService.NavigateAsync($"{ nameof(SettingsView)}");
        }
        private async void UpdateModel(object selectObject)
        {
            ProfileViewModel profilVieweModel = selectObject as ProfileViewModel;
            if (profilVieweModel != null)
            {
                var parametr = new NavigationParameters();
                parametr.Add(ListOfNames.profileUser, profilVieweModel);
                await _navigationService.NavigateAsync(($"{ nameof(AddEditProfileView)}"), parametr);
            }
        }
        private void DeletingCurrentUserSettings() //When logging out, delete all user settings
        {
            _authorizationService.Unauthorize();
        }
        private async void ExecuteGoBack()
        {
            DeletingCurrentUserSettings();
            await _navigationService.NavigateAsync($"/{ nameof(NavigationPage)}/{ nameof(SignInView)}");
        }
        private async void RemoveModel(object selectObject)
        {
            ProfileViewModel profileViewModel = selectObject as ProfileViewModel;
            if (profileViewModel != null)
            {
                var confirmConfig = new ConfirmConfig()
                {
                    Message = AppResource.you_really_want_to_delete_this_profile,
                    OkText = AppResource.delete.ToUpper(),
                    CancelText = AppResource.cancel.ToUpper()
                };
                var result = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                if (result)
                {
                    var profileModel = profileViewModel.ToProfileModel();
                    bool resultOfAction= await _profileService.DeleteProfileModelToStorageAsync(profileModel);
                    if(resultOfAction)
                    {
                        ProfileViewModelList.Remove(profileViewModel);
                    }
                    if(ProfileViewModelList.Count==0)
                    {
                        ToggleVisibility(true, false);
                    }
                }
            }
        }
        private async void OnShowDialogExecuted()
        {
            await _dialogService.ShowDialogAsync($"{ nameof(PopupsContent)}", new DialogParameters
            {
                {ListOfNames.pathSelectedPicture, ProfileViewModel.ImageSource}
            });
        }
        private void ToggleVisibility(bool visableLabel, bool visableListView)
        {
            IsVisableListView = visableListView;
            IsVisableLabel = visableLabel;
        }
        private IEnumerable<ProfileViewModel> ConvertingProfileModelToProfileViewModel(IEnumerable <ProfileModel> listProfileModels)
        {
            var profileViewModelList = new ObservableCollection<ProfileViewModel>();
            foreach (var profileModel in listProfileModels)
            {
                var convertingProfileViewModel = profileModel.ToProfileViewModel();
                if (convertingProfileViewModel != null)
                {
                    profileViewModelList.Add(convertingProfileViewModel);
                }
            }
            return profileViewModelList;
        }
        #endregion
        #region---Overriding---
        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var listProfileModelsOfCurrentUser = await _profileService.GetProfileListAsync();
            if (listProfileModelsOfCurrentUser.ToList().Count == 0 || listProfileModelsOfCurrentUser == null)
            {
                ToggleVisibility(true, false);
            }
            else
            {
                ToggleVisibility(false, true);
                var profileViewModelList = ConvertingProfileModelToProfileViewModel(listProfileModelsOfCurrentUser);
                if (_profileService.GetValueToSort() == EnumSet.SortingType.SortByName)
                {
                    ProfileViewModelList.AddRange(profileViewModelList.OrderBy(x => x.Name).ToList());
                }
                else if (_profileService.GetValueToSort() == EnumSet.SortingType.SortByNickName)
                {
                    ProfileViewModelList.AddRange(profileViewModelList.OrderBy(x => x.NickName).ToList());
                }
                else if (_profileService.GetValueToSort() == EnumSet.SortingType.SortByDateAddedToDatabase)
                {
                    ProfileViewModelList.AddRange(profileViewModelList.OrderBy(x => x.MomentOfRegistration).ToList());
                }
                else
                {
                    ProfileViewModelList.AddRange(profileViewModelList);
                }
            }
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(ProfileViewModel))
            {
                OnShowDialogExecuted();
            }
        }
        #endregion
    }
}
