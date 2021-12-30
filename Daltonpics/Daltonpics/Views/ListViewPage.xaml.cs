using Daltonpics.Tools;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Daltonpics.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public ListViewPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Is called when the window appears . We are going to use it to remove
        /// the navigation bar and then have more space on the screen .
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.SetNavBarIsVisible(this, false);
            viewModel.FillList();
        }


        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            viewModel.ActiveTest = ((IshiharaTestItem)e.Item);
            //await DisplayAlert("Item Tapped", "An item was tapped.", "OK");
            viewModel.DisplayPopup = true;

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async void BtnReturnClicked(object sender, EventArgs e)
        {
            viewModel.IshiharaTestItemList.Clear();
            await Shell.Current.GoToAsync("//TestPage");
        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.IshiharaTestItemList.Clear();
            Shell.Current.GoToAsync("//TestPage");
            return true;
        }

        private void ReturnToGrid(object sender, EventArgs e)
        {
            viewModel.DisplayPopup = false;
        }
    }
}
