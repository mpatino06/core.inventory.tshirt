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
	public partial class Rct : ContentPage
	{
		public Rct(string ordercode, RctExtendModel rct)
		{
			InitializeComponent();
			BindingContext = new RctViewModel(ordercode, rct);
		}

		private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new MainMenu(string.Empty));
		}
	}
}
