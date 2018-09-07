using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using App3.core.tshirt.Views;
using App3.core.tshirt.Views.Count;
using App3.core.tshirt.Views.Reception;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App3.core.tshirt.ViewModels
{
	public class MenuItemViewModel
	{
		public int Id { get; set; }
		public string Icon { get; set; }
		public string Name { get; set; }
		public string Page { get; set; }
		public string UserLogin { get; set; }

		public ICommand NavigateCommand
		{
			get { return new RelayCommand(Navigate); }
		}

		private void Navigate()
		{

			switch (Id)
			{
				case 0:
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new MainMenu("UserLogin"));
					break;
				case 1:
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Search());
					break;
				case 2:
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PlanList(UserLogin));
					break;
				case 4: //Transferencia entre Bodegas
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Views.TransferWarehouse.SubMenu());
					break;
				case 5: //Salida a Producción
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Views.Output.SubMenuOutput());
					break;
				case 6: // Cambio Producto
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Views.Change.SubMenuChangeP());
					break;
				case 10: // Cambio Producto -Crear Solicitud
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Views.Change.ClientProduct());
					break;
				case 11: // Cambio Producto - Lista
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Views.Change.ListChange());
					break;
				case 12: // Transferencia entre Bodegas - Crear Solicitud
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Views.TransferWarehouse.Warehouses());
					break;
				case 13: // Transferencia entre Bodegas - Lista
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Views.TransferWarehouse.List());
					break;
				case 14: // Salida a Producción - Crear Solicitud
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Views.Output.OutputProducts());
					break;
				case 15: // Salida a Producción - Lista
					Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Views.Output.ListOutput());
					break;

				default:
					break;
			}
		}
	}
}