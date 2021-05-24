using ProfileBook.Enum;

namespace ProfileBook.Service.Theme
{
    public interface IThemService
    {
        void PerformThemeChange(EnumSet.Theme theme);
        EnumSet.Theme GetValueTheme();
        void SetValueTheme(EnumSet.Theme themType);
    }
}
