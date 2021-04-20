using ProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Service.Profile
{
    public interface IProfileService
    {
        Task UpdateProfileModel(ProfileModel profileModel);
        Task InsertProfileModel(ProfileModel profileModel);
        Task RemoveProfileModel(ProfileModel profileModel);
        Task<List<ProfileModel>> GetAllProfileModel();
    }
}
