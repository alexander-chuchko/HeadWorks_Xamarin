using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Service.Settings
{
    public interface ISettingsManager
    {
        int Id { get; set; }
        bool IsSortByName { get; set; }
        bool IsSortByNickName { get; set; }
        bool IsSortByDateAddedToDatabase { get; set; }
        void RemoveCurrentId();
        void DeleteAllSettings();
        void ClearAllSettings();
    }
}
