using Breda_Ontdekt.Model.Entities;
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
        public static void switchLanguage(string language, Page page, Frame frame, TransferClass transfer)
        {
            try
            {
                ApplicationLanguages.PrimaryLanguageOverride = language;
                frame.Navigate(page.GetType(), transfer);
            }
            catch (Exception)
            {
                ErrorHandler.handleError("language change error", "ok");
            }
        }
    }
}
