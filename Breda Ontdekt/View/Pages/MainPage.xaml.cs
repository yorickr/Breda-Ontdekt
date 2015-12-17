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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Breda_Ontdekt.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            List<string> testList = new List<string>();
            testList.Add("some");
            testList.Add("testing");
            bool a = await Storage.SaveMyListData(testList, "filename.txt");
            textBox.Text = a.ToString();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            try {
                await Storage.GetMyListData("filename.txt");
                // foreach(string s in list){
                textBox.Text += "loaded";
                // }
            }
            catch(Exception ex)
            {
                textBox.Text = ex.ToString();
            }
        }
    }
}
