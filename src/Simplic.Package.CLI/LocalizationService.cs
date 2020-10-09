using Simplic.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.CLI
{
    class LocalizationService : ILocalizationService
    {
        public CultureInfo CurrentLanguage => throw new NotImplementedException();

        public void ChangeLanguage(CultureInfo language)
        {
        }

        public IList<CultureInfo> GetAvailableLanguages()
        {
            return new List<CultureInfo>();
        }

        public void LoadDatabaseLocalization()
        {
        }

        public IDictionary<string, string> Search(string searchKey)
        {
            return new Dictionary<string, string>();
        }

        public string Translate(string key)
        {
            return key;
        }

        public string Translate(string key, params string[] formatValues)
        {
            return key;
        }
    }
}
