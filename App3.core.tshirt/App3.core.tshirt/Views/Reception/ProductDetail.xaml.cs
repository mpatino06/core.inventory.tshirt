using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.Models;
using App3.core.tshirt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.Reception
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductDetail : ContentPage
	{
		public ProductDetail(OrderDetailExtend details)
		{
			InitializeComponent();
			BindingContext = new OrderDetailViewModel(details);
		}

		private void OnTapGestureRecognizerTapped2(object sender, EventArgs e)
		{

			OrderDetailViewModel mvm = sender as OrderDetailViewModel;


			var arrayCodes = ((OrderDetailViewModel)BindingContext).OrderProduct;

			var list = new List<OrderTShirt>();
			foreach (var item in arrayCodes)
			{
				list.Add(new OrderTShirt() { Code = item });
			}


			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new OrderPage(list, null));
		}


		protected override void OnAppearing()
		{
			base.OnAppearing();
			EntrQuantity.Focus();
			EntrQuantity.Text = "";

		}
	}
}
