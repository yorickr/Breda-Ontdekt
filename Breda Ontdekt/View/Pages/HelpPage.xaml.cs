using Breda_Ontdekt.Model.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class HelpPage : Page
    {

        private TransferClass transfer;
        private int headerFont;
        private int textFont;
        private double margin;

        public HelpPage()
        {
            this.InitializeComponent();
            if (!ViewModel.AppGlobal.ZoomedIn)
            {
                headerFont = 14;
                textFont = 12;
                margin = 18;
            }
            else
            {
                headerFont = 26;
                textFont = 24;
                margin = 30;
            }
            setSizes();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            transfer = (TransferClass)e.Parameter;
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += backButton_Tapped;
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

        private void setSizes()
        {
            header1.FontSize = headerFont;
            header2.FontSize = headerFont;
            header3.FontSize = headerFont;
            header4.FontSize = headerFont;
            header5.FontSize = headerFont;
            header6.FontSize = headerFont;
            header7.FontSize = headerFont;
            header8.FontSize = headerFont;
            header9.FontSize = headerFont;
            text1.FontSize = textFont;
            text1.Margin = new Thickness(0, margin, 0, 0);
            text2.FontSize = textFont;
            text2.Margin = new Thickness(0, margin, 0, 0);
            text3.FontSize = textFont;
            text3.Margin = new Thickness(0, margin, 0, 0);
            text4.FontSize = textFont;
            text4.Margin = new Thickness(0, margin, 0, 0);
            text5.FontSize = textFont;
            text5.Margin = new Thickness(0, margin, 0, 0);
            text6.FontSize = textFont;
            text6.Margin = new Thickness(0, margin, 0, 0);
            text7.FontSize = textFont;
            text7.Margin = new Thickness(0, margin, 0, 0);
            text8.FontSize = textFont;
            text8.Margin = new Thickness(0, margin, 0, 0);
            text9.FontSize = textFont;
            text9.Margin = new Thickness(0, margin, 0, 0);
        }

        private void ZoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.AppGlobal.ZoomedIn)
            {
                //siteInfo.FontSize = 18;
                headerFont = 14;
                textFont = 12;
                margin = 18;
                ViewModel.AppGlobal.ZoomedIn = false;
            }
            else
            {
                //siteInfo.FontSize = 40;
                headerFont = 26;
                textFont = 24;
                margin = 30;
                ViewModel.AppGlobal.ZoomedIn = true;
            }
            setSizes();
        }
    }
}
