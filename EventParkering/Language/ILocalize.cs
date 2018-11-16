using System;
using System.Globalization;

namespace TifAppMobile.Language
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);
    }
}
