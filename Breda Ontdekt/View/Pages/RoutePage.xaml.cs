using Breda_Ontdekt.Model.Entities;
using Breda_Ontdekt.ViewModel;
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
	public sealed partial class RoutePage : Page
	{
        private RoutePageModel model;
        private TransferClass transfer;

		public RoutePage()
		{
			this.InitializeComponent();
		}
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            transfer = (TransferClass)e.Parameter;
            model = new RoutePageModel(transfer);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(LanguagePage), transfer);
		}

		private void RouteListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            
		}

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MapPage), transfer);
        }

        private void routes_ItemClick(object sender, ItemClickEventArgs e)
        {
            transfer.route = (Model.Entities.Route)e.ClickedItem;
            this.Frame.Navigate(typeof(MapPage), transfer);
        }
    }
}
