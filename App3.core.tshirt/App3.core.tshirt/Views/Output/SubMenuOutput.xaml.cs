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

namespace App3.core.tshirt.Views.Output
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubMenuOutput : ContentPage
	{
		public ObservableCollection<MenuItemViewModel> Menu { get; set; }
		public SubMenuOutput()
		{
			InitializeComponent();
			BindingContext = new SubMenuViewModelTransaction();

		}
		void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
	   => ((ListView)sender).SelectedItem = null;
	}

	public class SubMenuViewModelTransaction : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<MenuItemViewModel> Menu { get; set; }
		public SubMenuViewModelTransaction()
		{
			LoadMenu();
		}


		public void LoadMenu()
		{
			Menu = new ObservableCollection<MenuItemViewModel>();

			Menu.Add(new MenuItemViewModel
			{
				Id = 14,
				Icon = "agregar.png",
				Name = "Crear Solicitud",
				Page = ""
			});
			Menu.Add(new MenuItemViewModel
			{
				Id = 15,
				Icon = "ver.png",
				Name = "Ver Solicitud",
				Page = ""
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