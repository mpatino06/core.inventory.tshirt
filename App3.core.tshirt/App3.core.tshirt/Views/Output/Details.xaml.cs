using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.Output
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Details : ContentPage
	{
		public Details(int outputId)
		{
			InitializeComponent();
			BindingContext = new OutputDetailViewModel(outputId);
		}


		private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SubMenuOutput());
		}

		public void ButtonBackClick()
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();

		}
	}
}