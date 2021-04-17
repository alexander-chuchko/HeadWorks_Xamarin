using ProfileBook.Service.Settings;

namespace ProfileBook.Service.Authorization
{
    public class Authorization: IAuthorization
    {
        private ISettingsManager _settingsManager;
        public Authorization(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
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
        public void DeleteAllSettings()
        {
            _settingsManager.DeleteAllSettings();
        }
        public bool IsSortByName()
        {
            return _settingsManager.IsSortByName;
        }
        public void SetValueToSortByName(bool value)
        {
            _settingsManager.IsSortByName = value;
        }
        public bool IsSortByNickName()
        {
            return _settingsManager.IsSortByNickName;
        }
        public void SetValueToSortByNickName(bool value)
        {
            _settingsManager.IsSortByNickName = value;
        }
        public bool IsSortByDateAddedToDatabase()
        {
            return _settingsManager.IsSortByDateAddedToDatabase;
        }
        public void SetValueToSortByDateAddedToDatabase(bool value)
        {
            _settingsManager.IsSortByDateAddedToDatabase = value;
        }
        public void ClearAllSettings()
        {
            _settingsManager.ClearAllSettings();
        }
    }
}
