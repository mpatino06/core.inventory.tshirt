using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using App3.core.tshirt.Models;
using App3.core.tshirt.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App3.core.tshirt.ViewModels
{
    public class SearchProductChangeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ProductChangeServices _produtChangeServices;

        public SearchProductChangeViewModel()
        {
            _produtChangeServices = new ProductChangeServices();
            ProductChangeCollection = new ObservableCollection<Models.OrderReqDetailExtend>();
            Search();
        }

        #region Properties
        private string _productChangeCode;
        public string ProductChangeCode
        {
            get { return _productChangeCode; }
            set { _productChangeCode = value; RaiseOnPropertyChange(); }
        }

        private ObservableCollection<Models.OrderReqDetailExtend> _productChangeCollection;
        public ObservableCollection<Models.OrderReqDetailExtend> ProductChangeCollection
        {
            get { return _productChangeCollection; }
            set
            {
                _productChangeCollection = value;
                HeightList = (_productChangeCollection.Count * 80) + (_productChangeCollection.Count * 10);
                RaiseOnPropertyChange();
            }
        }

        private int _heightList;
        public int HeightList
        {
            get { return _heightList; }
            set { _heightList = value; RaiseOnPropertyChange(); }
        }


		private ObservableCollection<ListSearchProduct> _listSearchCollection;
		public ObservableCollection<ListSearchProduct> ListSearchCollection
		{
			get { return _listSearchCollection; }
			set
			{
				_listSearchCollection = value;
				HeightList = (_listSearchCollection.Count * 80) + (_listSearchCollection.Count * 10);
				RaiseOnPropertyChange();
			}
		}

		private string _searchUser;
		public string SearchUser
		{
			get { return _searchUser; }
			set { _searchUser = value; RaiseOnPropertyChange(); }
		}

		private string _searchDate;
		public string SearchDate
		{
			get { return _searchDate; }
			set { _searchDate = value; RaiseOnPropertyChange(); }
		}

		#endregion


		#region Methods


		public async void Search()
        {
            int? code = null;

            if (!string.IsNullOrEmpty(ProductChangeCode))
            {
                code = int.Parse(ProductChangeCode);
            }

            var result = await _produtChangeServices.GetAll();

            ProductChangeCollection = result != null ? new ObservableCollection<Models.OrderReqDetailExtend>(result) : new ObservableCollection<Models.OrderReqDetailExtend>();


            HeightList = (ProductChangeCollection.Count * 80) + (ProductChangeCollection.Count * 10);
        }

        public void RaiseOnPropertyChange([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


        #region Commands

        public ICommand DetailCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var item = (e as Models.OrderReqDetailExtend);
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Views.Change.DetailChangeP(item.OrderReqCode.ToString()));
                });
            }
        }

		public ICommand SearchCommand => new RelayCommand(GetListSearch);
		#endregion

		#region LIST

		public async void GetListSearch()
		{
			string codigo = string.Empty;
			List<ListSearchProduct> _list = new List<ListSearchProduct>();

			SearchUser = string.Empty;
			SearchDate = string.Empty;
			ListSearchCollection = new ObservableCollection<ListSearchProduct>(_list);

			if (!string.IsNullOrEmpty(ProductChangeCode))
			{
				codigo = ProductChangeCode;

				var result = await _produtChangeServices.GetListDetailByCode(codigo);
				if (result != null)
				{
					SearchUser = result.FirstOrDefault().UserUpdated;
					SearchDate = result.FirstOrDefault().DateProductChanged;

					_list = result.Select(a => new ListSearchProduct
					{
						Product = a.ProductCode + " " + a.ProductName,
						ProductChanged = a.ProductCodeChanged + " " + a.ProductNameChanged,
						Quantity = a.Quantity
					}).ToList();

					ListSearchCollection = new ObservableCollection<ListSearchProduct>(_list);

				}
				else {
					await App.Current.MainPage.DisplayAlert("TSHIRT", "Numero de Pedido no encontrado", "OK");
				}

			}
			else
			{
				await App.Current.MainPage.DisplayAlert("TSHIRT", "Ingreser Número de Pedido", "OK");
			}

			

		}



		#endregion
	}
}
