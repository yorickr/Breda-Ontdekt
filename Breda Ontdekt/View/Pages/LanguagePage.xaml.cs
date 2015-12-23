using Breda_Ontdekt.ViewModel.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
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
	public sealed partial class LanguagePage : Page
	{
		private Boolean _firstTime = true;

		public LanguagePage()
		{
			this.InitializeComponent();
			this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof (MapPage));
		}

		private void UK_Button_Click(object sender, RoutedEventArgs e)
		{
			if (_firstTime == true)
			{
				_firstTime = false;
				this.BackButton.Visibility = Visibility.Visible;
                Setting.switchLanguage("en-GB", new RoutePage(), this.Frame);
                Frame.Navigate(typeof(RoutePage));
			}
			else if (_firstTime == false)
			{
                Setting.switchLanguage("en-GB", new MapPage(), this.Frame);
                Frame.Navigate(typeof(MapPage));
            }

		}

		private void NL_Button_Click(object sender, RoutedEventArgs e)
		{
			if (_firstTime == true)
			{
				_firstTime = false;
				this.BackButton.Visibility = Visibility.Visible;
                Setting.switchLanguage("nl-NL", new RoutePage(), this.Frame);
                Frame.Navigate(typeof(RoutePage));
            }
						else if (_firstTime == false)
			{
                Setting.switchLanguage("nl-NL", new MapPage(), this.Frame);
                Frame.Navigate(typeof(MapPage));
            }

		}
	}
}
