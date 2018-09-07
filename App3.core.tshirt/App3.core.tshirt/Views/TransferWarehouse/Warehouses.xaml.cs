using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.TransferWarehouse
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Warehouses : ContentPage
	{
		public Warehouses()
		{
			InitializeComponent();
			BindingContext = new WarehouseProductTransferViewModel();
		}

		private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SubMenu());
		}
		private void OnTapPrevPage(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
		}
	}
}