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
	public partial class Result : ContentPage
	{
		public Result(string id)
		{
			InitializeComponent();
			btnGuardar.Text = "Nro SOL" + id;
		}

		private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SubMenu());
		}

		private void BtnGuardar_OnClicked(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SubMenu());
		}
	}
}