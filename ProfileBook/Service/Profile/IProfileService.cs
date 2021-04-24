using ProfileBook.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Service.Profile
{
    public interface IProfileService
    {
        Task UpdateProfileModelAsync(ProfileModel profileModel);
        Task InsertProfileModelAsync(ProfileModel profileModel);
        Task RemoveProfileModelAsync(ProfileModel profileModel);
        Task<List<ProfileModel>> GetAllProfileModelAsync();
        bool GetValueToSortByName();
        void SetValueToSortByName(bool value);
        bool GetValueSortByNickName();
        void SetValueToSortByNickName(bool value);
        bool GetValueSortByDateAddedToDatabase();
        void SetValueToSortByDateAddedToDatabase(bool value);
        void DeleteAllSortSettings();
    }
}
