using Breda_Ontdekt.Model;
using Breda_Ontdekt.Model.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Breda_Ontdekt.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InfoPage : Page
    {
        private ObjectInfo site;

        public InfoPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if ((TransferClass)e.Parameter != null)
                {

                    //try to get site when navigate to this page
                    TransferClass transfer = (TransferClass)e.Parameter;
                    site = transfer.info;
                    
                    //for testing
                    site.isPassed = true;

                    siteName.Text = site.name;
                    if (site.description != null)
                    {
                        siteInfo.Text = site.description;
                    }
                }
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
