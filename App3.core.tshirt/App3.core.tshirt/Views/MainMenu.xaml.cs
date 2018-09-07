using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainMenu : ContentPage
	{
		public MainMenu(string user)
		{
			User = user;
			InitializeComponent();
			LoadMenu();
			ListMenu.ItemsSource = MenuItems;
		}

		void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		=> ((ListView)sender).SelectedItem = null;



		private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
		{
			//Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Config());
		}

		public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

		public string User { get; set; }


		public void LoadMenu()
		{

			MenuItems = new ObservableCollection<MenuItemViewModel>();

			MenuItems.Add(new MenuItemViewModel
			{
				Id = 1,
				Icon = "recepcion.png",
				Name = "Recepcion",
				Page = "Pages/Recepcion/Search",
				UserLogin = User
			});
			MenuItems.Add(new MenuItemViewModel
			{
				Id = 2,
				Icon = "conteo.png",
				Name = "Conteo",
				Page = "Pages/Count/PlanList",
				UserLogin = User
			});
			MenuItems.Add(new MenuItemViewModel
			{
				Id = 4,
				Icon = "transferencia.png",
				Name = "Transferencia entre Bodegas",
				Page = "Pages/WarehouseTransfer/SubMenu",
				UserLogin = User
			});
			MenuItems.Add(new MenuItemViewModel
			{
				Id = 5,
				Icon = "salida.png",
				Name = "Salida a Producción",
				Page = "Pages/Count/PlanList"
			});
			MenuItems.Add(new MenuItemViewModel
			{
				Id = 6,
				Icon = "cambio.png",
				Name = "Cambio Producto",
				Page = "Pages/Transaction/ClientProduct"
			});

		}


	}
}