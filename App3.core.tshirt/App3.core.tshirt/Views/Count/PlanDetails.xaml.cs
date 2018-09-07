using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.Models;
using App3.core.tshirt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.Count
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlanDetails : ContentPage
	{
		private int idPlan = 0;
		public string _user { get; set; }
		public string _description { get; set; }
		public string _barCodeProduct { get; set; }
		public string _nameProduct { get; set; }
		public List<ViewCountPlanDetailItem> _countPlnDetailItems { get; set; }


		public PlanDetails(List<ViewCountPlanDetailItem> items, string user, string description, string barCodeProduct)
		{

			this._countPlnDetailItems = items;
			this._user = user;
			this._description = description;
			this._barCodeProduct = barCodeProduct;
			this._nameProduct = items.FirstOrDefault().Description;


			idPlan = items.FirstOrDefault().Id;
			InitializeComponent();

		}

		private void GetPlanDetails(List<ViewCountPlanDetailItem> items, string user, string description, string barCodeProduct, string nameProduct)
		{

			BindingContext = new CountDetailViewModel(items, user, description, barCodeProduct, nameProduct);
		}



		private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
		}

		protected override void OnAppearing()
		{
			GetPlanDetails(_countPlnDetailItems, _user, _description, _barCodeProduct, _nameProduct);
			base.OnAppearing();

			EntrQuantity.Focus();
			EntrQuantity.Text = "";

		}
	}
}
