using ProfileBook.Service.Settings;

namespace ProfileBook.Service.Authorization
{
    public class AuthorizationService: IAuthorizationService
    {
        private ISettingsManager _settingsManager;
        public AuthorizationService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
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
        public void RemoveIdCurrentUser()
        {
            _settingsManager.RemoveCurrentId();
        }
    }
}
