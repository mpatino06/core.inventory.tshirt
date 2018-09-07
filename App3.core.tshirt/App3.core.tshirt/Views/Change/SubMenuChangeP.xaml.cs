using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.Change
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubMenuChangeP : ContentPage
	{
		public SubMenuChangeP()
		{
			InitializeComponent();
			BindingContext = new SubMenuViewModelProductChange();
		}

		private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
			=> ((ListView)sender).SelectedItem = null;

		private void OnTapPrevPage(object sender, EventArgs e)
		{
			Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
		}
	}

	public class SubMenuViewModelProductChange : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<MenuItemViewModel> Menu { get; set; }

		public SubMenuViewModelProductChange()
		{
			LoadMenu();
		}

		public void LoadMenu()
		{
			Menu = new ObservableCollection<MenuItemViewModel>();

			Menu.Add(new MenuItemViewModel
			{
				Id = 10,
				Icon = "agregar.png",
				Name = "Crear Solicitud",
				Page = "Pages/Recepcion/Recepcion"
			});
			Menu.Add(new MenuItemViewModel
			{
				Id = 11,
				Icon = "ver.png",
				Name = "Ver Solicitud",
				Page = "Pages/Count/PlanList"
			});
			Menu.Add(new MenuItemViewModel
			{
				Id = 0,
				Icon = "menuback.png",
				Name = "Menu Principal",
				Page = ""
			});

		}
	}
}