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
using Windows.UI.Core;
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
        TransferClass transfer;

        public InfoPage()
        {
            this.InitializeComponent();
            if (!ViewModel.AppGlobal.ZoomedIn)
                siteInfo.FontSize = 18;
            else
                siteInfo.FontSize = 40;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if ((TransferClass)e.Parameter != null)
                {

                    //try to get site when navigate to this page
                    transfer = (TransferClass)e.Parameter;
                    site = transfer.info;
                    
                    if (site.lastPoint)
                    {
                        //TODO make button to move back to vvv
                    }

                    siteName.Text = site.name;
                    if (site.lastPoint)
                        BackButton.Content = "go back to vvv";
                    if (site.imageUrls != null)
                        LoadImages();
                    

                    if (site.description != null)
                    {
                        siteInfo.Text = site.description;
                    }
                }
            }
            catch { }
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += backButton_Tapped;
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

        private void backButton_Tapped(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
            e.Handled = true;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();

            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            currentView.BackRequested -= backButton_Tapped;
        }

        private async void ToVVV_Click(object sender, RoutedEventArgs e)
        {
           await Windows.System.Launcher.LaunchUriAsync(site.videoUrl);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (site.lastPoint)
            {
                transfer.isReturn = true;
                this.Frame.Navigate(typeof(MapPage), transfer);
            }
            else
                if (Frame.CanGoBack) Frame.GoBack();
        }

        private void ZoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.AppGlobal.ZoomedIn)
            {
                siteInfo.FontSize = 18;
                ViewModel.AppGlobal.ZoomedIn = false;
            }
            else
            {
                siteInfo.FontSize = 40;
                ViewModel.AppGlobal.ZoomedIn = true;
            }

        }
    }
}
