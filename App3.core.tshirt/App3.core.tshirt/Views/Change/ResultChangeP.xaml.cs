using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.Change
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultChangeP : ContentPage
	{
		public ResultChangeP()
		{
			InitializeComponent();
			//BindingContext = new ContentPageViewModel();
		}


		private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SubMenuChangeP());
		}

		private void BtnGuardar_OnClicked(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SubMenuChangeP());
		}
	}
}
