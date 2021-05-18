
namespace ProfileBook.Service.Settings
{
    public interface ISettingsManager
    {
        int Id { get; set; }
        bool IsDarkTheme { get; set; }
        string SelectedLanguage { get; set; }
        int SortingType { get; set; }
    }
}
