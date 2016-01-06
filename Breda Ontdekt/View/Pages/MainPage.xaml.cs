using Breda_Ontdekt.Model;
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
using Breda_Ontdekt.ViewModel.Lib;
using Windows.UI.Core;
using Breda_Ontdekt.Model.Entities;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Breda_Ontdekt.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private TransferClass transfer;

		public static MainPage instance
        {
            get; set;
        }

        public MainPage()
        {
            this.InitializeComponent();
            transfer = new TransferClass();
            instance = this;
            Frame.Navigate(typeof(StartPage));

        }

        private void ListView_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if(e.Cumulative.Translation.X < -20)
            {
                HamburgerMenu.IsPaneOpen = false;
            }
        }

        private void Grid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if(e.Cumulative.Translation.X > 50)
            {
                HamburgerMenu.IsPaneOpen = true;
            }
        }

        public void SwitchMenu()
        {
            HamburgerMenu.IsPaneOpen = !HamburgerMenu.IsPaneOpen;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            StackPanel panel = (StackPanel)e.ClickedItem;
            switch (panel.Name)
            {
                case "HelpPanel":
                    this.Frame.Navigate(typeof(HelpPage),transfer);
                    break;
                case "LanguagePanel":
                    transfer.resetted = true;
                    this.Frame.Navigate(typeof(LanguagePage),transfer);
                    break;
                case "ResetPanel":
                    this.Frame.Navigate(typeof(LanguagePage),transfer);
                    break;
                case "VVVPanel":
                    transfer.isReturn = true;
                    this.Frame.Navigate(typeof(MapPage), transfer);
                    break;
                default:
                    throw new Exception();
            }
            HamburgerMenu.IsPaneOpen = false;

			SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
			Frame.CanGoBack ?
			AppViewBackButtonVisibility.Visible :
			AppViewBackButtonVisibility.Collapsed;
		}

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchMenu();
        }

        public void refreshMenu(string language)
        {
            switch(language)
            {
                case "en-US":
                    Language.Text = "Select Route";
                    Reset.Text = "Reset App";
                    VVV.Text = "Go back to the VVV";
                    break;
                case "nl-NL":
                    Language.Text = "Selecteer een Route";
                    Reset.Text = "Reset de Applicatie";
                    VVV.Text = "Ga terug naar de VVV";
                    break;
            }
        }
    }
}
