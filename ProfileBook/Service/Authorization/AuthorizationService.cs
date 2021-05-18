using ProfileBook.Enum;
using ProfileBook.Helpers;
using ProfileBook.Service.Localization;
using ProfileBook.Service.Profile;
using ProfileBook.Service.Settings;
using ProfileBook.Service.Theme;

namespace ProfileBook.Service.Authorization
{
    public class AuthorizationService: IAuthorizationService
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IProfileService _profileService;
        private readonly IThemService _themService;
        private readonly ILocalizationService _localizationService;
        public AuthorizationService(ISettingsManager settingsManager, IProfileService profileService, IThemService themService, ILocalizationService localizationService)
        {
            _settingsManager = settingsManager;
            _profileService = profileService;
            _themService = themService;
            _localizationService = localizationService;
        }
        /*--Methods for setting the Id--*/
        public int GetIdCurrentUser()
        {
            return _settingsManager.Id;
        }
        public void SetIdCurrentUser(int id)
        {
            _settingsManager.Id = id;
        }
        public void SetDefaultValueId()
        {
            _settingsManager.Id = 0;
        }
        public void SettingDefaultSettings()
        {
            if (GetIdCurrentUser() != 0|| EnumSet.SortingType.SortDefault!=_profileService.GetValueToSort() || _themService.GetValueDarkTheme() || _localizationService.GetValueLanguage() != ListOfNames.english)
            {
                SetDefaultValueId();
                _profileService.SetDefaultValueToSort();
                _themService.SetDefaultTheme();
                _localizationService.SetDefaultLanguage();
            }
        }

    }
}
