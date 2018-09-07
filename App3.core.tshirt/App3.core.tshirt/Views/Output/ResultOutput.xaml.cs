using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.Output
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultOutput : ContentPage
	{
		public ResultOutput(string id)
		{
			InitializeComponent();
			btnGuardar.Text = "Nro SOL" + id;
		}
		private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SubMenuOutput());
		}

		private void BtnGuardar_OnClicked(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SubMenuOutput());
		}
	}
}
