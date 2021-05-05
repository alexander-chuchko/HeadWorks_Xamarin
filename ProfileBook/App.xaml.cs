using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Prism.Unity;
using ProfileBook.Dialogs;
using ProfileBook.Resource;
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
using System.Globalization;
using System.Threading;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        public App():this(null)
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
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<IProfileService>(Container.Resolve<ProfileService>());
            containerRegistry.RegisterInstance<IThemService>(Container.Resolve<ThemService>());
            containerRegistry.RegisterInstance<ILocalizationService>(Container.Resolve<LocalizationService>());
            //Registration
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SingUpViewModel>();
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfilePage, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<Settings, SettingsViewModel>();
            containerRegistry.RegisterDialog<PopupsContent, PopupsContentViewModel>();
        }
        protected override async void OnInitialized()
        {
            InitializeComponent();
            //Кладем первую страницу в стек навигации
            //var result=await NavigationService.NavigateAsync("NavigationPage/MainPage");

            var result = await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
            //var result = await NavigationService.NavigateAsync($"{nameof(Settings)}");
            //var result = await NavigationService.NavigateAsync($"{nameof(MainPage)}"));
            //var result = await NavigationService.NavigateAsync(($"{ nameof(SignUp)}"));
            //var result = await NavigationService.NavigateAsync(($"{nameof(NavigationPage)}/{ nameof(MainList)}"));
            //var result = await NavigationService.NavigateAsync(($"{nameof(NavigationPage)}/{ nameof(AddEditProfilePage)}"));
            //var result = await NavigationService.NavigateAsync(($"{ nameof(NavigationPage)}/{ nameof(MainList)}"));
            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
            #endregion
        }

    }
}