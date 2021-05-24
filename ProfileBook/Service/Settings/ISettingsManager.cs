namespace ProfileBook.Service.Settings
{
    public interface ISettingsManager
    {
        int AuthorizedUserID { get; set; }
        int SortingType { get; set; }
        int ThemType { get; set; }
        string SelectedLanguage { get; set; }
        void ClearData();
    }
}
