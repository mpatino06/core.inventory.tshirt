using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.Reception
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Search : ContentPage
	{
		public Search()
		{
			InitializeComponent();
			BindingContext = new SearchProviderOrderViewModel();
		}

		private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			SearchProviderOrderViewModel mvm = sender as SearchProviderOrderViewModel;
			if (e.SelectedItem == null)
			{
				return;
			}
			var x = (Provider)e.SelectedItem;

			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new OderList(x.Code, x.Name));
		}

		private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			EntSearch.Focus();
		}
	}
}