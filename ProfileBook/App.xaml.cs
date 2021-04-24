﻿using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Prism.Unity;
using ProfileBook.Dialogs;
using ProfileBook.Service;
using ProfileBook.Service.Authorization;
using ProfileBook.Service.Profile;
using ProfileBook.Service.Repository;
using ProfileBook.Service.Settings;
using ProfileBook.Service.User;
using ProfileBook.Services.Repository;
using ProfileBook.View;
using ProfileBook.ViewModel;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        public App():this(null)
        {
            
        }
        public App(IPlatformInitializer initializer):base(initializer)
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
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<IProfileService>(Container.Resolve<ProfileService>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());

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