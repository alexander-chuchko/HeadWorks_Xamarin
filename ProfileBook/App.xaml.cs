using Acr.UserDialogs;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Prism.Unity;
using ProfileBook.Dialogs;
using ProfileBook.Enum;
using ProfileBook.Helpers;
using ProfileBook.Service;
using ProfileBook.Service.Authorization;
using ProfileBook.Service.Localization;
using ProfileBook.Service.Profile;
using ProfileBook.Service.Repository;
using ProfileBook.Service.Settings;
using ProfileBook.Service.Theme;
using ProfileBook.Service.User;
using ProfileBook.Services.Repository;
using ProfileBook.View;
using ProfileBook.ViewModel;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        private IAuthorizationService _authorizationService;
        private IThemService _themService;
        private ILocalizationService _localizationService;

        IAuthorizationService AuthorizationService =>_authorizationService ??(_authorizationService= Container.Resolve<IAuthorizationService>());
        IThemService ThemService => _themService ?? (_themService = Container.Resolve<IThemService>());
        ILocalizationService LocalizationService => _localizationService ?? (_localizationService = Container.Resolve<ILocalizationService>());

        public App() :this(null)
        {
            
        }
        public App(IPlatformInitializer initializer) :base(initializer)
        {   
        }
        #region---Overrides---
        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterPopupDialogService();
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());

            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<IProfileService>(Container.Resolve<ProfileService>());
            containerRegistry.RegisterInstance<IThemService>(Container.Resolve<ThemService>());
            containerRegistry.RegisterInstance<ILocalizationService>(Container.Resolve<LocalizationService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            //Registration
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SingUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterDialog<PopupsContent, PopupsContentViewModel>();
        }
        protected override async void OnInitialized()
        {
            InitializeComponent();

            if (AuthorizationService.IsAuthorized)
            {
                if(ThemService.GetValueTheme()!=EnumSet.Theme.Light)
                {
                    ThemService.PerformThemeChange(ThemService.GetValueTheme());
                }
                if(LocalizationService.GetValueLanguage()!=ListOfNames.english)
                {
                    LocalizationService.ChangeApplicationLanguage(LocalizationService.GetValueLanguage());
                }
                await NavigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainListView)}"));
            }
            else
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInView)}");
            }
            #endregion
        }
    }
}