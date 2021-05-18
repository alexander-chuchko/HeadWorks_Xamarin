
using ProfileBook.Enum;

namespace ProfileBook.Service.Theme
{
    public interface IThemService
    {
        bool GetValueDarkTheme();
        void SetValueDarkTheme(bool value);
        void SetDefaultTheme();
        void PerformThemeChange(EnumSet.Theme theme);
    }
}
