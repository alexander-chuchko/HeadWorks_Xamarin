using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Service.Authorization
{
    public interface IAuthorizationService
    {
        int GetIdCurrentUser();
        void SetIdCurrentUser(int id);
        void RemoveIdCurrentUser();
        //bool GetValueSortByNickName();
        //void SetValueToSortByNickName(bool value);
        //bool GetSortByDateAddedToDatabase();
        //void SetValueToSortByDateAddedToDatabase(bool value);
    }
}
