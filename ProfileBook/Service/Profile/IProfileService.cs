using ProfileBook.Enum;
using ProfileBook.Model.Pfofile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Service.Profile
{
    public interface IProfileService
    {
        Task<bool> UpdateProfileModelToStorageAsync(ProfileModel profileModel);
        Task<bool> SaveOrUpdateProfileModelToStorageAsync(ProfileModel profileModel);
        Task<bool> DeleteProfileModelToStorageAsync(ProfileModel profileModel);
        Task<IEnumerable<ProfileModel>> GetProfileListAsync();
        EnumSet.SortingType GetValueToSort();
        void SetValueToSort(EnumSet.SortingType sortingType);
    }
}
