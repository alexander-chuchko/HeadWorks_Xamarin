
namespace ProfileBook.Service.Settings
{
    public interface ISettingsManager
    {
        int Id { get; set; }
        bool IsSortByName { get; set; }
        bool IsSortByNickName { get; set; }
        bool IsSortByDateAddedToDatabase { get; set; }
        bool IsDarkTheme { get; set; }
        string SelectedLanguage { get; set; }
        void RemoveCurrentId();
        void RemoveDarkTheme();
        void RemoveLanguage();
        void DeleteAllSortSettings();
    }
}
