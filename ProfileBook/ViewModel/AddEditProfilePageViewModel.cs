using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Helpers;
using ProfileBook.Model;
using ProfileBook.Service.Authorization;
using ProfileBook.Service.Profile;
using ProfileBook.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class AddEditProfilePageViewModel: BindableBase, INavigatedAware
    {
        private string _titlePage;
        private string _nickName;
        private string _name;
        private string _description;
        private string pathPicture;
        private bool _isEnable;
        private ProfileModel _profileUser;
        public Action OpenGallery { get; set; }
        public Action TakePhoto { get; set; }

        private IAuthorization _authorization;
        private IProfileService _profileService;
        private INavigationService _navigationService;
        public ICommand TapCommand { get; set; }
        public ICommand NavigationCommand { get; set; }
        public AddEditProfilePageViewModel(INavigationService navigationService, IAuthorization authorization, IProfileService profileService):base()
        {
            _navigationService = navigationService;
            _authorization = authorization;
            _profileService = profileService;
            Name = string.Empty;
            NickName = string.Empty;
            IsEnable = false;
            NavigationCommand = new Command(NavigationToMainList);
             TapCommand = new Command(TouchedPicture);
            OpenGallery = SelectImage;
            TakePhoto = TakingPictures;
            TitlePage = ($"{ nameof(AddEditProfilePage)}");
        }
        public bool IsEnable
        {
            get => _isEnable;
            set => SetProperty(ref _isEnable, value);
        }
        public ProfileModel ProfileUser
        {
            get => _profileUser;
            set => SetProperty(ref _profileUser, value);
        }
        public string TitlePage
        {
            get => _titlePage;
            set => SetProperty(ref _titlePage, value);
        }
        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public string PathPicture
        {
            get => pathPicture;
            set => SetProperty(ref pathPicture, value);
        }
        public async void ExecuteNavigateToNavigationToMainList()
        {
            await _navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainList)}"));
        }
       public void TouchedPicture()
        {
            IUserDialogs userDialogs = UserDialogs.Instance;
            ActionSheetConfig config = new ActionSheetConfig();
            List<ActionSheetOption> Options = new List<ActionSheetOption>();
            Options.Add(new ActionSheetOption("Pick at Gallery", OpenGallery, "folder_image.png"));
            Options.Add(new ActionSheetOption("Take photo with camera", TakePhoto, "camera.png"));
            ActionSheetOption cancel = new ActionSheetOption("Cancel", null, null);
            config.Options = Options;
            config.Cancel = cancel;
            userDialogs.ActionSheet(config);
        }
        public void NavigationToMainList()
        {
            if (Validation.IsInformationInNameAndNickName(Name, NickName))
            {
                if (ProfileUser != null)
                {
                    UpdateProfileModel();
                }
                else
                {
                    AddProfileModel();
                }
                ExecuteNavigateToNavigationToMainList();
            }
            else
            {
                ListOfMessages.ShowInformationIsMissingInTheFieldsNameAndNickName();
            }
        }
        public void UpdateProfileModel()
        {
            ProfileUser.ImageSource = PathPicture;
            ProfileUser.Name = Name;
            ProfileUser.NickName = NickName;
            _profileService.UpdateProfileModel(ProfileUser);
        }
        public void AddProfileModel()
        {
            ProfileModel profileModel = new ProfileModel()
            {
                UserId = _authorization.GetIdCurrentUser(),
                Description = Description,
                ImageSource = PathPicture,
                MomentOfRegistration = DateTime.Now,
                Name = Name,
                NickName = NickName
            };
            _profileService.InsertProfileModel(profileModel);
        }
        public async void SelectImage()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });
            if(result!=null)
            {
                var stream = await result.OpenReadAsync();
                //PictureSource = ImageSource.FromStream(() => stream);
                PathPicture = result.FullPath;
            }
        }
        public async void TakingPictures()
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
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.Count != 0)
            {
                ProfileUser = parameters.GetValue<ProfileModel>("ProfileUser");
                if(ProfileUser!=null)
                {
                    Description = ProfileUser.Description;
                    PathPicture = ProfileUser.ImageSource;
                    NickName = ProfileUser.NickName;
                    Name = ProfileUser.Name;
                }
            }
            else
            {
                PathPicture = "pic_profile.png";
            }
        }
    }
}
