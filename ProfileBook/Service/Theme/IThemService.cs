
using ProfileBook.Enum;

namespace ProfileBook.Service.Theme
{
    public interface IThemService
    {
        bool GetValueDarkTheme();
        void SetValueValueDarkTheme(bool value);
        void RemoveThemeDark();
        void SetDefaultTheme();
    }
}
