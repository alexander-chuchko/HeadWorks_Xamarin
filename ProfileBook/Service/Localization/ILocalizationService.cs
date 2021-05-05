using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Service.Localization
{
    public interface ILocalizationService
    {
        string GetValueLanguage();
        void SetValueLanguage(string value);
        void RemoveLanguage();
    }
}
