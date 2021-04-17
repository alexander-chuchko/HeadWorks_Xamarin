using ProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Service.Profile
{
    public interface IProfileService
    {
        void UpdateProfileModel(ProfileModel profileModel);
        void InsertProfileModel(ProfileModel profileModel);
        void RemoveProfileModel(ProfileModel profileModel);
        Task<List<ProfileModel>> GetAllProfileModel();
    }
}
