using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using System.Threading.Tasks;
using App3.core.tshirt.Services;
using App3.core.tshirt.Models;

namespace App3.core.tshirt.ViewModels
{
    public class OrderProviderViewModel : INotifyPropertyChanged
    {

        private OrderService orderService;

        public OrderProviderViewModel(string codigoProveedor, string nombreProveedor)
        {
            orderService = new OrderService();
            LoadOrders(codigoProveedor, nombreProveedor);

        }

        #region PROPERTIES

        public ICommand SearchCommand
        {
            get { return new RelayCommand(ButtonRecibir); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _providerName;

        public string ProviderName
        {
            get { return _providerName; }
            set
            {
                _providerName = value;
                OnPropertyChanged("ProviderName");
            }
        }


        private int _heightList;

        public int HeightList
        {
            get { return _heightList; }
            set
            {
                _heightList = value;
                OnPropertyChanged("HeightList");
            }
        }


        private ObservableCollection<OrderTShirt> _Orders = new ObservableCollection<OrderTShirt>();

        public ObservableCollection<OrderTShirt> Orders
        {
            get { return _Orders; }
            set
            {
                _Orders = value;
                HeightList = (_Orders.Count * 40) + (_Orders.Count * 10);
                OnPropertyChanged("Orders");
            }
        }

        public bool _count { get; set; }

        public bool CountOrders
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged("CountOrders");
            }
        }


        public string _message { get; set; }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }
        #endregion

        private void LoadOrders(string codigoProveedor, string nameProvider)
        {

            Task.Run(async () =>
            {
                var result = await orderService.GetOrderProviders(codigoProveedor);
                if (result != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ProviderName = nameProvider;
                        Orders = new ObservableCollection<OrderTShirt>(result.Where(a => a.Value1.Trim() != "1"));
                        CountOrders = Orders.Any() ? true : false;
                        Message = CountOrders ? string.Empty : "EL Proveedor no tiene Ordenes Activas";
                    });
                }
            });



            //var result = await orderService.GetOrderProviders(codigoProveedor);
            //if (result != null)
            //{
            //    ProviderName = nameProvider;
            //    Orders = new ObservableCollection<OrderTShirt>(result.Where(a => a.Value1.Trim() != "1"));
            //    CountOrders = Orders.Any() ? true : false;
            //    Message = CountOrders ? string.Empty : "EL Proveedor no tiene Ordenes Activas";
            //}
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonRecibir()
        {


            var ord = new List<OrderTShirt>(Orders.Where(a => a.IsSelected == true));


            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new OrderPage(ord, null));
        }
    }
}
