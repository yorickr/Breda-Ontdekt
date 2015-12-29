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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Breda_Ontdekt.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage instance
        {
            get; set;
        }

        public MainPage()
        {
            this.InitializeComponent();
            instance = this;
            Frame.Navigate(typeof(LanguagePage));
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
                    this.Frame.Navigate(typeof(HelpPage));
                    break;
                case "InfoPanel":
                    this.Frame.Navigate(typeof(InfoPage));
                    break;
                case "LanguagePanel":
                    this.Frame.Navigate(typeof(LanguagePage));
                    break;
                case "ResetPanel":
                    this.Frame.Navigate(typeof(LanguagePage));
                    break;
                case "VVVPanel":
                    //not implemented yet
                    break;
                default:
                    throw new Exception();
            }
            HamburgerMenu.IsPaneOpen = false;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchMenu();
        }
    }
}
