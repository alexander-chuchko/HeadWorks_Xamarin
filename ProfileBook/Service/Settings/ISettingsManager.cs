
namespace ProfileBook.Service.Settings
{
    public interface ISettingsManager
    {
        int Id { get; set; }
        bool IsSortByName { get; set; }
        bool IsSortByNickName { get; set; }
        bool IsSortByDateAddedToDatabase { get; set; }
        void RemoveCurrentId();
        void DeleteAllSortSettings();
    }
}
