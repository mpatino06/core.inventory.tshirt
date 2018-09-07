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
	public partial class OrderPage : ContentPage
	{
		private List<OrderTShirt> _code { get; set; }
		private List<ViewOrder> _orders { get; set; }
		private int CountPage { get; set; }

		public OrderPage(List<OrderTShirt> codes, List<ViewOrder> orders)
		{
			CountPage = 0;
			_code = codes;
			_orders = orders;
			InitializeComponent();
		}

		protected override void OnAppearing()
		{

			base.OnAppearing();
			CountPage += 1;

			GetOrderPage();
			EntBarcode.Text = "";
			EntBarcode.Focus();
		}

		protected override bool OnBackButtonPressed()
		{
			Device.BeginInvokeOnMainThread(async () => {
				var result = await this.DisplayAlert("TSHIRT!", "Desea Salir?", "SI", "NO");
				if (result) await this.Navigation.PopAsync();
			});

			return true;
		}


		private void GetOrderPage()
		{
			this.BindingContext = CountPage == 1 ? new OrderViewModel(_code, _orders) : new OrderViewModel(_code, null);
		}

	}
}