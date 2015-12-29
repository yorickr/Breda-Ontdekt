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
	public sealed partial class LanguagePage : Page
	{
		public Boolean firstTime = true;

		public LanguagePage()
		{
			this.InitializeComponent();
			if(firstTime == false)
			{
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
		}

		private void UK_Button_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(RoutePage));
		}

		private void NL_Button_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(RoutePage));
		}
	}
}
