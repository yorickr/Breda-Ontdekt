using Breda_Ontdekt.Model.Entities;
using Breda_Ontdekt.ViewModel.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
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
	public sealed partial class LanguagePage : Page
	{
		private Boolean _firstTime = true;

        private TransferClass transfer;

		public LanguagePage()
		{
			this.InitializeComponent();
			this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

		}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            transfer = (TransferClass)e.Parameter;
        }

        private void UK_Button_Click(object sender, RoutedEventArgs e)
		{
            this.transfer.language = "EN";
            _firstTime = false;
            Setting.switchLanguage("en-GB");
            Frame.Navigate(typeof(RoutePage), transfer);

        }

		private void NL_Button_Click(object sender, RoutedEventArgs e)
		{
            this.transfer.language = "NL";
            _firstTime = false;
            Setting.switchLanguage("nl-NL");
            Frame.Navigate(typeof(RoutePage), transfer);
        }
	}
}
