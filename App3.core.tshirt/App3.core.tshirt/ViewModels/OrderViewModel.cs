using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using App3.core.tshirt.Infrastructure;
using App3.core.tshirt.Services;
using App3.core.tshirt.Models;
using App3.core.tshirt.Views.Reception;

namespace App3.core.tshirt.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private const int Open = (int)EnumTShirt.OrderStatus.OPEN;
        private const int Close = (int)EnumTShirt.OrderStatus.CLOSED;

        public event PropertyChangedEventHandler PropertyChanged;

        private OrderService orderService;
        private RctServices rctServices;
        private const int ITEM_PER_PAGE = 10;


        public OrderViewModel(List<OrderTShirt> codes, List<ViewOrder> order)
        {
            orderService = new OrderService();
            rctServices = new RctServices();
            LoadOrders(codes, order);
            LoadWarehouse();
        }

        #region Properties


        private string _purchaseOrder;

        public string PurchaseOrder
        {
            get { return _purchaseOrder; }
            set
            {
                _purchaseOrder = value;
                OnPropertyChanged("PurchaseOrder");
            }
        }


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

        private string _providerCode;
        public string ProviderCode
        {
            get { return _providerCode; }
            set
            {
                _providerCode = value;
                OnPropertyChanged("ProviderCode");
            }
        }


        private string _lote;

        public string Lote
        {
            get { return _lote; }
            set
            {
                _lote = value;
                OnPropertyChanged("Lote");
            }
        }


        private string _bCode;

        public string BCode
        {
            get { return _bCode; }
            set
            {
                _bCode = value;
                OnPropertyChanged("BCode");
            }
        }

        private ObservableCollection<ViewOrder> _Orders = new ObservableCollection<ViewOrder>();

        public ObservableCollection<ViewOrder> Orders
        {
            get { return _Orders; }
            set
            {
                _Orders = value;
                //HeightList = (_Orders.Count * 50) + (_Orders.Count * 5);
                OnPropertyChanged("Orders");
            }
        }


        private ObservableCollection<ViewOrder> _orderList = new ObservableCollection<ViewOrder>();

        public ObservableCollection<ViewOrder> OrderList
        {
            get { return _orderList; }
            set
            {
                _orderList = value;
                HeightList = (_orderList.Count * 45) + (_orderList.Count * 5);
                OnPropertyChanged("OrderList");
            }
        }

        private string _message; 

        public string MessageResult
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("MessageResult");
            }
        }

        private ObservableCollection<string> _warehouseCollection;
        public ObservableCollection<string> WarehouseCollection
        {
            get
            {
                return _warehouseCollection;
            }
            set
            {
                _warehouseCollection = value;
                OnPropertyChanged("WarehouseCollection");
            }
        }

        private string[] _orderProduct;

        public string[] OrderProduct
        {
            get { return _orderProduct; }
            set
            {
                _orderProduct = value;
                OnPropertyChanged("OrderProduct");
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

        private ObservableCollection<string> _ordercodesCollection;
        public ObservableCollection<string> OrdercodesCollection
        {
            get
            {
                return _ordercodesCollection;
            }
            set
            {
                _ordercodesCollection = value;
                OnPropertyChanged("OrdercodesCollection");
            }
        }

        private string _orderSelect;

        public string OrderSelect
        {
            get { return _orderSelect; }
            set
            {
                _orderSelect = value;
                OnPropertyChanged("OrderSelect");
            }
        }

        private string _warehouseSelect;

        public string WarehouseSelect
        {
            get { return _warehouseSelect; }
            set
            {
                _warehouseSelect = value;
                OnPropertyChanged("WarehouseSelect");
            }
        }



        #region PAGINACION

        private int _numberPage;

        public int NumberPage
        {
            get { return _numberPage; }
            set
            {
                _numberPage = value;
                OnPropertyChanged("NumberPage");
            }
        }

        private int _itemsCount;

        public int ItemsCount
        {
            get { return _itemsCount; }
            set
            {
                _itemsCount = value;
                OnPropertyChanged("ItemsCount");
            }
        }

        private int _totalPage;

        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged("TotalPage");
            }
        }

        private string _msgPage;

        public string MsgPage
        {
            get { return _msgPage; }
            set
            {
                _msgPage = value;
                OnPropertyChanged("MsgPage");
            }
        }


        private bool _isPage;

        public bool IsPage
        {
            get { return _isPage; }
            set
            {
                _isPage = value;
                OnPropertyChanged("IsPage");
            }
        }


        private bool _isVisibleNextPage;
        public bool IsVisibleNextPage
        {
            get { return _isVisibleNextPage; }
            set
            {
                _isVisibleNextPage = value;
                OnPropertyChanged("IsVisibleNextPage");
            }
        }

        private bool _isVisiblePrevPage;
        public bool IsVisiblePrevPage
        {
            get { return _isVisiblePrevPage; }
            set
            {
                _isVisiblePrevPage = value;
                OnPropertyChanged("IsVisiblePrevPage");
            }
        }

        #endregion

        #endregion


        #region COMMANDS

        public ICommand SaveRecepcion
        {
            get { return new RelayCommand(Save); }
        }

        public ICommand SearchProvider
        {
            get { return new RelayCommand(Search); }
        }

        public ICommand PrevPage
        {
            get { return new RelayCommand(PrevPageList); }
        }

        public ICommand NextPage
        {
            get { return new RelayCommand(NextPageList); }
        }

    
        public ICommand PickerSelect
        {
            get { return new RelayCommand(ChangeWarehouse); }
        }

        #endregion

        #region Methods

        private async void Save()
        {
            var rct = new RctExtendModel();

            if (ValidateOrder())
            {
                var answer = await App.Current.MainPage.DisplayAlert("TSHIRT", "Esta seguro que desea Guardar?", "SI", "NO");
                if (answer)
                {
                    //var _details = OrderProduct.Select(items => new Detail()
                    //{
                    //    OrderCode = items                        
                    //}).Distinct().ToList();

                    var _details = OrderList.Where(a => a.TotalProduct > 0).Select(items => new Detail()
                    {
                        OrderCode = items.Code,
                        Status = (items.Quantity > items.TotalProduct) ? Open.ToString() : Close.ToString(),
                        ProductCode = items.ProductCode,
                        Warehouse = items.OrderValue1,
                        Quantity = items.TotalProduct
                    }).ToList();



                    rct.Lot = Lote;
                    rct.ProviderCode = ProviderCode;
                    rct.UserId = 1;
                    rct.Details = _details;

                    var result = await rctServices.Add(rct);
                    if (result != null)
                        await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Rct(ProviderName, result));
                }
            }
        }

        private async void LoadOrders(List<OrderTShirt> codes, List<ViewOrder> order)
        {

            OrderProduct = codes.Select(a => a.Code).Distinct().ToArray();
            PurchaseOrder = string.Join(",", OrderProduct);
            OrdercodesCollection = new ObservableCollection<string>(OrderProduct);
            OrderSelect = OrdercodesCollection.FirstOrDefault().ToString();
            WarehouseSelect = string.Empty;

            var result = order == null ? await orderService.GetListOrder(codes) : order;


            if (result != null)
            {
                ProviderName = result.FirstOrDefault().ProviderName;
                ProviderCode = result.FirstOrDefault().ProviderCode;
                Lote = result.FirstOrDefault().Value2;
                var list = result.Where(a => a.Quantity > 0).Select(item => new ViewOrder()
                {
                    Id = item.Id,
                    IdOrder = item.IdOrder,
                    Code = item.Code,
                    Description = item.Description,
                    ProviderCode = item.ProviderCode,
                    ProviderName = item.ProviderName,
                    ProviderBarcode = item.ProviderBarcode,
                    ProductCode = item.ProductCode,
                    IdProduct = item.IdProduct,
                    ProductName = item.ProductName,
                    BarcodeProduct = item.BarcodeProduct,
                    Quantity = item.Quantity,
                    OrderValue1 = item.OrderValue2, //item.Value2,
                    OrderValue2 = item.TotalProduct >= item.Quantity ? "Images/yes.png" : "Images/no.png",
                    OrderValue3 = item.Code + " " + item.ProductCode + " " + item.ProductName,
                    OrderValue4 =  item.OrderValue4,
                    OrderValue5 = item.OrderValue5,
                    TotalProduct = item.TotalProduct
                }).ToList();


                ItemsCount = list.Count();
                NumberPage = 1;

                if (ItemsCount > 10)
                {
                    decimal valueItemscount = Convert.ToDecimal(list.Count());
                    decimal valueItemsperpages = Convert.ToDecimal(ITEM_PER_PAGE);
                    decimal valueResult = Math.Ceiling(valueItemscount / valueItemsperpages);

                    TotalPage = Convert.ToInt32(valueResult);

                    MsgPage = Convert.ToString(NumberPage) + " / " + Convert.ToString(TotalPage);
                    IsPage = true;
                    IsVisiblePrevPage = false;
                    IsVisibleNextPage = true;
                }

                OrderList = new ObservableCollection<ViewOrder>(list.Take(ITEM_PER_PAGE));

                Orders = new ObservableCollection<ViewOrder>(list);
            }
        }

        private void NextPageList()
        {
            if (TotalPage != NumberPage)
            {
                IsVisiblePrevPage = true;
                var list = Orders.Skip(NumberPage * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
                if (WarehouseSelect != null)
                {

                    var _orderChanges = list.Select(a => new ViewOrder
                    {
                        Id = a.Id,
                        IdOrder = a.IdOrder,
                        Code = a.Code,
                        Description = a.Description,
                        ProviderCode = a.ProviderCode,
                        ProviderName = a.ProviderName,
                        ProviderBarcode = a.ProviderBarcode,
                        ProductCode = a.ProductCode,
                        IdProduct = a.IdProduct,
                        ProductName = a.ProductName,
                        BarcodeProduct = a.BarcodeProduct,
                        Quantity = a.Quantity,
                        OrderValue1 = WarehouseSelect,
                        OrderValue2 = a.OrderValue2,
                        OrderValue3 = a.OrderValue3,
                        OrderValue4 = a.OrderValue4,
                        OrderValue5 = a.OrderValue5,
                        TotalProduct = a.TotalProduct
                    });
                    OrderList = new ObservableCollection<ViewOrder>(_orderChanges);
                }
                else
                {
                    OrderList = new ObservableCollection<ViewOrder>(list);
                }
                
                NumberPage += 1;
                MsgPage = Convert.ToString(NumberPage) + " / " + Convert.ToString(TotalPage);
                if (NumberPage == TotalPage) IsVisibleNextPage = false;
            }
        }

        //ChangeWarehouse
        private void ChangeWarehouse()
        {
            var result = WarehouseSelect;

            var _orderChanges = OrderList.Select(a => new ViewOrder
            {
                Id = a.Id,
                IdOrder = a.IdOrder,
                Code = a.Code,
                Description = a.Description,
                ProviderCode = a.ProviderCode,
                ProviderName = a.ProviderName,
                ProviderBarcode = a.ProviderBarcode,
                ProductCode = a.ProductCode,
                IdProduct = a.IdProduct,
                ProductName = a.ProductName,
                BarcodeProduct = a.BarcodeProduct,
                Quantity = a.Quantity,
                OrderValue1 = WarehouseSelect,
                OrderValue2 = a.OrderValue2,
                OrderValue3 = a.OrderValue3,
                OrderValue4 = a.OrderValue4,
                OrderValue5 = a.OrderValue5,
                TotalProduct = a.TotalProduct
            });
            OrderList = new ObservableCollection<ViewOrder>(_orderChanges);
        }


        private void PrevPageList()
        {
            NumberPage -= 1;
            if (NumberPage > 0)
            {
                IsVisibleNextPage = true;
                MsgPage = Convert.ToString(NumberPage) + " / " + Convert.ToString(TotalPage);
                var list = (NumberPage > 1)
                    ? Orders.Skip(NumberPage * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList()
                    : Orders.Take(ITEM_PER_PAGE).ToList();

                if (WarehouseSelect != null)
                {

                    var _orderChanges = list.Select(a => new ViewOrder
                    {
                        Id = a.Id,
                        IdOrder = a.IdOrder,
                        Code = a.Code,
                        Description = a.Description,
                        ProviderCode = a.ProviderCode,
                        ProviderName = a.ProviderName,
                        ProviderBarcode = a.ProviderBarcode,
                        ProductCode = a.ProductCode,
                        IdProduct = a.IdProduct,
                        ProductName = a.ProductName,
                        BarcodeProduct = a.BarcodeProduct,
                        Quantity = a.Quantity,
                        OrderValue1 = WarehouseSelect,
                        OrderValue2 = a.OrderValue2,
                        OrderValue3 = a.OrderValue3,
                        OrderValue4 = a.OrderValue4,
                        OrderValue5 = a.OrderValue5,
                        TotalProduct = a.TotalProduct
                    });
                    OrderList = new ObservableCollection<ViewOrder>(_orderChanges);
                }
                else
                {
                    OrderList = new ObservableCollection<ViewOrder>(list);
                }

                
                if (NumberPage == 1) IsVisiblePrevPage = false;
            }
        }


        private void LoadWarehouse()
        {
            string[] list = { "TSHIRT", "FLEXO", "COSTURA" };

            //list
            //list.Add(new Warehouse() {Id =0, Name = "T-SHIRTS"});
            //list.Add(new Warehouse() { Id = 1, Name = "FLEXO" });
            //list.Add(new Warehouse() { Id = 2, Name = "COSTURA" });

            WarehouseCollection = new ObservableCollection<string>(list);

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        private async void Search()
        {
            var details = new OrderDetailExtend();
            var _list = Orders;
            var result = _list.FirstOrDefault(a => a.BarcodeProduct == BCode);
            if (result != null)
            {

                details.order = OrderSelect; // PurchaseOrder;
                details.providerName = ProviderName;
                details.productBarcode = BCode;
                details.productName = result.ProductName; ;
                details.productCode = result.ProductCode;
                details.providerCode = result.ProviderCode;
                details.codeOrders = OrderProduct;
                details.OrderProducts = _list.Where(a => a.BarcodeProduct == BCode).Select(a => a.Code).Distinct().ToArray();
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ProductDetail(details));
            }
            else
            {
                MessageResult = "Producto no Registrado";
            }

        }
        public bool ValidateOrder()
        {
            bool result = true;

            if (string.IsNullOrEmpty(Lote))
            {
                App.Current.MainPage.DisplayAlert("TSHIRT", "Debe ingresar un codigo de Lote", "OK");
                return false;
            }

            if (OrderList.Where(a => a.TotalProduct > 0).Any(item => string.IsNullOrEmpty(item.OrderValue1)))
            {
                App.Current.MainPage.DisplayAlert("TSHIRT", "Ingresar nombre de Almacen en productos con cantidades mayores a 0", "OK");
                return false;
            }

            return result;

        }
        #endregion
    }
}
