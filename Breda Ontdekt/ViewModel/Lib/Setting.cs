using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.UI.Xaml.Controls;

namespace Breda_Ontdekt.ViewModel.Lib
{
    public class Setting
    {
        // nl-nl
        // en-US
        public static void switchLanguage(string language)
        {
            try
            {
                ApplicationLanguages.PrimaryLanguageOverride = language;
            }
            catch (Exception)
            {
                ErrorHandler.handleError("language change error", "ok");
            }
        }

        public static string getCurrentLanguage()
        {
            return ApplicationLanguages.PrimaryLanguageOverride;
        }
    }
}
