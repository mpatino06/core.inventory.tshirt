using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using App.Mobile.Droid.Models;
using App.Mobile.Droid.Pages.Change;
using App.Mobile.Droid.Infrastructure;
using App.Mobile.Droid.Pages.Reception;
using App.Mobile.Droid.Services;
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
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Pages.Change.DetailChangeP(item.OrderReqCode.ToString()));
                });
            }
        }
        #endregion
    }
}
