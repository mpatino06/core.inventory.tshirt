using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.TransferWarehouse
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductsTransfer : ContentPage
	{
		public ProductsTransfer()
		{
			InitializeComponent();
		}

		private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Warehouses());
		}
	}
}