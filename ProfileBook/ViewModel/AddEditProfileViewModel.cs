using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Helpers;
using ProfileBook.Model;
using ProfileBook.Resource;
using ProfileBook.Service.Authorization;
using ProfileBook.Service.Profile;
using ProfileBook.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ProfileBook.Extension;
using Prism.Services;

namespace ProfileBook.ViewModel
{
    public class AddEditProfileViewModel: BaseViewModel, INavigatedAware
    {
        #region---PrivateFields---
        private string _nickName;
        private string _name;
        private string _description;
        private string pathPicture;
        private bool _isEnable;
        private ProfileViewModel _profileViewModel;
        private readonly IAuthorizationService _authorizationService;
        private readonly IProfileService _profileService;
        private readonly IPageDialogService _pageDialogService;
        #endregion
        public AddEditProfileViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IProfileService profileService, IPageDialogService pageDialogService) :base(navigationService)
        {
            Name = string.Empty;
            NickName = string.Empty;
            IsEnable = false;
            _authorizationService = authorizationService;
            _profileService = profileService;
            _pageDialogService = pageDialogService;
            SaveCommand = new Command(SaveProfileModel);
             TapCommand = new Command(TouchedPicture);
            OpenGallery = SelectImage;
            TakePhoto = TakingPictures;
        }
        #region---PublicProperties---
        public Action OpenGallery { get; set; }
        public Action TakePhoto { get; set; }

        public ICommand TapCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public bool IsEnable
        {
            get{return _isEnable;}
            set { SetProperty(ref _isEnable, value); }
        }
        public ProfileViewModel ProfileViewModel
        {
            get { return _profileViewModel; }
            set { SetProperty(ref _profileViewModel, value); }
        }
        public string NickName
        {
            get { return _nickName; }
            set { SetProperty(ref _nickName, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        public string PathPicture
        {
            get { return pathPicture; }
            set { SetProperty(ref pathPicture, value); }
        }
        #endregion
        #region---Methods---
        private async void ExecuteNavigateToNavigationToMainList()
        {
            await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainList)}"));
        }
        private void TouchedPicture()
       {
            IUserDialogs userDialogs = UserDialogs.Instance;
            ActionSheetConfig config = new ActionSheetConfig();
            List<ActionSheetOption> Options = new List<ActionSheetOption>();
            Options.Add(new ActionSheetOption(AppResource.pick_at_gallery, OpenGallery, ListOfNames.pictureForFolder));
            Options.Add(new ActionSheetOption(AppResource.take_photo_with_camera, TakePhoto, ListOfNames.pictureForCamera));
            ActionSheetOption cancel = new ActionSheetOption(AppResource.cancel.ToUpper(), null, null);
            config.Options = Options;
            config.Cancel = cancel;
            userDialogs.ActionSheet(config);
        }
        private async Task<bool> IsFieldsFilled()
        {
            var resultFilling = true;
            if (!Validation.IsInformationInNameAndNickName(Name, NickName))
            {
                resultFilling = false;
               await _pageDialogService.DisplayAlertAsync(AppResource.information_is_missing_in_the_fields_name_and_nick_name, AppResource.invalid_data_entered, "OK");
            }
            return resultFilling;
        }
        private async void SaveProfileModel()
        {
            if (await IsFieldsFilled())
            {
                if (ProfileViewModel != null)
                {
                    await UpdateProfileModel();
                }
                else
                {
                    await AddProfileModel();
                }
                ExecuteNavigateToNavigationToMainList();
            }
        }
        private async Task UpdateProfileModel()
        {
            ProfileViewModel.ImageSource = PathPicture;
            ProfileViewModel.Name = Name;
            ProfileViewModel.NickName = NickName;
            ProfileViewModel.Description = Description;
            var profileModel = ProfileViewModel.ToProfileModel();
            if(profileModel!=null)
            {
                await _profileService.UpdateProfileModelAsync(profileModel);
            }
        }
        private async Task AddProfileModel()
        {
            ProfileViewModel profileViewModel = new ProfileViewModel()
            {
                UserId = _authorizationService.GetIdCurrentUser(),
                Description = Description,
                ImageSource = PathPicture,
                MomentOfRegistration = DateTime.Now,
                Name = Name,
                NickName = NickName
            };
            var profileModel = profileViewModel.ToProfileModel();
            if(profileModel!=null)
            {
                await _profileService.InsertProfileModelAsync(profileModel);
            }
        }
        private async void SelectImage()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title =AppResource.please_pick_a_photo
            });
            if(result!=null)
            {
                var stream = await result.OpenReadAsync();
                //PictureSource = ImageSource.FromStream(() => stream);
                PathPicture = result.FullPath;
            }
        }
        private async void TakingPictures()
        {
            var result = await MediaPicker.CapturePhotoAsync();
            if(result!=null)
            {
                var stream = await result.OpenReadAsync();
                var newFile = Path.Combine(FileSystem.AppDataDirectory, result.FileName);
                var newStream = File.OpenWrite(newFile);
                await stream.CopyToAsync(newStream);
                PathPicture = result.FullPath;
            }
        }
        #endregion
        #region--Iterface INavigatedAware implementation-- 
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue<ProfileViewModel>(ListOfNames.profileUser, out ProfileViewModel profileViewModel))
            {
                ProfileViewModel = parameters.GetValue<ProfileViewModel>(ListOfNames.profileUser);
                if (ProfileViewModel != null)
                {
                    Description = ProfileViewModel.Description;
                    PathPicture = ProfileViewModel.ImageSource;
                    NickName = ProfileViewModel.NickName;
                    Name = ProfileViewModel.Name;
                }
            }
            else
            {
                PathPicture = ListOfNames.basePicture;
            }
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }
        #endregion
    }
}
