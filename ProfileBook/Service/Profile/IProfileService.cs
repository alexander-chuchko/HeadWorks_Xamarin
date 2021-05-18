using ProfileBook.Enum;
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
        Task<IEnumerable<ProfileModel>> GetAllProfileModelAsync();
        EnumSet.SortingType GetValueToSort();
        void SetValueToSort(EnumSet.SortingType sortingType);
        void SetDefaultValueToSort();
    }
}
