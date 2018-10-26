using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.Count
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Plan : ContentPage
	{
		public int _id { get; set; }
		public string _user { get; set; }

		public Plan(int id, string user)
		{
			_user = user;
			_id = id;
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			GetPlan();
			EntBarcode.Text = "";
			EntBarcode.Focus();
		}


		void GetPlan()
		{
			BindingContext = new CountViewModel1(_id, _user);
		}


		private void OnTapGestureRecognizerTapped()
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
		}

		private void OnTapNextPage(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PlanList(_user));
		}
	}
}
