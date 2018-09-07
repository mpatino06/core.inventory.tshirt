using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.Change
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailChangeP : ContentPage
	{
		public DetailChangeP(string nroPedido)
		{
			InitializeComponent();
			BindingContext = new OrderReqDetailViewModel(nroPedido);
		}
		private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SubMenuChangeP());
		}
		public void ButtonBackClick()
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();

		}

	}
}
