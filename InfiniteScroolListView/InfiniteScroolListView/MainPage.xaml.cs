using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InfiniteScroolListView
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        ObservableCollection<string> Items;
        bool isLoading;
        public MainPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Items = new ObservableCollection<string>();
            listview.ItemsSource = Items;
            listview.ItemAppearing += async(sender, e) =>
            {
                if (isLoading || Items.Count == 0)
                    return;

               
                if (e.Item.ToString() == Items[Items.Count - 1])
                {
                     await LoadData();
                }
            };

            await LoadData();

        }

        private async Task LoadData()
        {
            isLoading = true;
            lding.IsRunning = true;
            lding.IsVisible = true;

            //simulator delayed load
            Device.StartTimer(TimeSpan.FromSeconds(2), () => {
                for (int i = 0; i < 20; i++)
                {
                    Items.Add(string.Format("Item {0}", Items.Count));
                }
                lding.IsRunning = false;
                lding.IsVisible = false;
                isLoading = false;
                //stop timer
                return false;
            });
        }
    }
}
