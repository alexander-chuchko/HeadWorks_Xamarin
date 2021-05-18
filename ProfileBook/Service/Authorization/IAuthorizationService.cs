
namespace ProfileBook.Service.Authorization
{
    public interface IAuthorizationService
    {
        int GetIdCurrentUser();
        void SetIdCurrentUser(int id);
        void SetDefaultValueId();
        void SettingDefaultSettings();
    }
}
