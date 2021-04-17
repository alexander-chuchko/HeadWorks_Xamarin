﻿using Prism.Navigation;
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

namespace ProfileBook.ViewModel
{
    public class MainListViewModel:BaseViewModel
    {
        private string _titlePage;
        private bool _isVisableLabel;
        private IDialogService _diulogService;
        private IProfileService _profileService;
        private IAuthorization _authorization;
        private ObservableCollection<ProfileModel> profilelList;
        private bool _isVisableListView;
        private ProfileModel _profileModel;
        private string _nameCheckedButton;
        public ICommand NavigationToSettingsView { get; set; }
        public ICommand NavigationToAddProfileUser { get; set; }
        public ICommand NavigationToSingIn { get; set; }
        public ICommand RemoveCommand { protected set; get; }
        public ICommand UpdateCommand { protected set; get; }
        public MainListViewModel(INavigationService navigationService, IDialogService diulogService, IProfileService profileService, IAuthorization authorization):base(navigationService)
        {
            TitlePage = ($"{ nameof(MainList)}");
            _authorization = authorization;
            _profileService = profileService;
            _diulogService = diulogService;
            NavigationToSettingsView = new Command(ExecuteGoToSettingsPage);
            NavigationToAddProfileUser = new Command(ExecuteGoToAddProfileUser);
            RemoveCommand = new Command(RemoveModel);
            UpdateCommand = new Command(UpdateModel);
            NavigationToSingIn = new Command(ExecuteGoBack);
        }
        #region---PublicProperties---
        public string NameIsChecked
        {
            set => SetProperty(ref _nameCheckedButton, value);
            get => _nameCheckedButton;
        }
        public ProfileModel ProfileModel
        {
            set => SetProperty(ref _profileModel, value);
            get => _profileModel;
        }
        public bool IsVisableListView
        {
            get => _isVisableListView;
            set => SetProperty(ref _isVisableListView, value);
        }
        public bool IsVisableLabel
        {
            get => _isVisableLabel;
            set => SetProperty(ref _isVisableLabel, value);
        }
        public string TitlePage
        {
            get => _titlePage;
            set => SetProperty(ref _titlePage, value);
        }
        public ObservableCollection<ProfileModel> ProfileList
        {
            get => profilelList;
            set => SetProperty(ref profilelList, value);
        }
        #endregion

        #region---PrivateMethods---
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
        private void DeletingCurrentUserSettings()
        {
            _authorization.RemoveIdCurrentUser();
            _authorization.DeleteAllSettings();
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
                    Message = "You really want to delete this profile?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };
                var result = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                if (result)
                {
                    _profileService.RemoveProfileModel(profileModel);
                    ProfileList.Remove(profileModel);
                }
            }
        }
        #region---PublicMethods---

        #endregion
        public void OnShowDialogExecuted()
        {
            _diulogService.ShowDialog("PopupsContent", new DialogParameters
            {
                {"message", ProfileModel.ImageSource}
            });
        }
        #endregion
        #region ---Overriding---
        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            var listOfUsers = await _profileService.GetAllProfileModel();
            if (listOfUsers.Count == 0)
            {
                IsVisableLabel = true;
                IsVisableListView = false;
            }
            else
            {
                IsVisableLabel = false;
                IsVisableListView = true;
                var resultsOfSelectingProfilesById = listOfUsers.Where(x => x.UserId == _authorization.GetIdCurrentUser());
                if(_authorization.IsSortByName())
                {
                    ProfileList = new ObservableCollection<ProfileModel>(resultsOfSelectingProfilesById.OrderBy(x => x.Name).ToList());
                }
                else if(_authorization.IsSortByNickName())
                {
                    ProfileList= new ObservableCollection<ProfileModel>(resultsOfSelectingProfilesById.OrderBy(x => x.NickName).ToList());
                }
                else if(_authorization.IsSortByDateAddedToDatabase())
                {
                    ProfileList= new ObservableCollection<ProfileModel>(resultsOfSelectingProfilesById.OrderBy(x => x.MomentOfRegistration).ToList());
                }
                else
                {
                    ProfileList = new ObservableCollection<ProfileModel>(resultsOfSelectingProfilesById);
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