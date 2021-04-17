using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Service.Authorization
{
    public interface IAuthorization
    {
        int GetIdCurrentUser();
        void SetIdCurrentUser(int id);
        void RemoveIdCurrentUser();
        bool IsSortByName();
        void SetValueToSortByName(bool value);
        bool IsSortByNickName();
        void SetValueToSortByNickName(bool value);
        bool IsSortByDateAddedToDatabase();
        void SetValueToSortByDateAddedToDatabase(bool value);
        void DeleteAllSettings();
        void ClearAllSettings();
    }
}
