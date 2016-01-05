using Breda_Ontdekt.Model;
using Breda_Ontdekt.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Image> images;

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

                    if (site.imageUrls != null)
                        LoadImages();

                    if (site.description != null)
                    {
                        siteInfo.Text = site.description;
                    }
                }
            }
            catch { }
        }

        private void LoadImages()
        {
            images = new ObservableCollection<Image>();
            site.imageUrls.ForEach(u =>
            {
                Image j = new Image();
                j.url = u;
                images.Add(j);
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
