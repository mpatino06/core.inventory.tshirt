using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.core.tshirt.Views.Change
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Add : ContentPage
	{
		public Add(string pedido, string cliente, string producto, string NombreProducto, int quantity, string warehouse)
		{
			InitializeComponent();
			BindingContext = new OrderReqAddViewModel(pedido, cliente, producto, NombreProducto, quantity, warehouse);
		}
	}
}