using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Breda_Ontdekt.ViewModel.Lib
{
    public static class ErrorHandler
    {
        public static async void handleError(string errorCode, string buttonText)
        {
            MessageDialog messageDialog = new MessageDialog(errorCode);
            messageDialog.Commands.Add(new UICommand(buttonText));
            await messageDialog.ShowAsync();
        }
    }
}
