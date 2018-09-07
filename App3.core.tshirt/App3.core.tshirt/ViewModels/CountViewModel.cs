using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.App;
using App3.core.tshirt.Models;
using App3.core.tshirt.Services;
using App3.core.tshirt.Views;
using App3.core.tshirt.Views.Count;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App3.core.tshirt.ViewModels
{
	public class CountViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private CountServices countServices;
		private SyncServices syncServices;
		private const int ITEM_PER_PAGE = 10;

		public void Dispose()
		{
			Items.Clear();
			Details.Clear();
			DetailList.Clear();
		}


		public CountViewModel(int? id, string user)
		{
			Items.Clear();
			Details.Clear();
			DetailList.Clear();

			countServices = new CountServices();
			syncServices = new SyncServices();


			if (id == null)
				AsyncLoadItems(); //LoadItems();
			else
			{
				IdPlan = (int)id;
				AsyncLoadDetailsPlan();
			}
			//LoadDetailsPlan((int)id);

			User = user;

		}


		private void AsyncLoadItems()
		{

			Task.Run(async () =>
			{
				var resultAsync = await countServices.GetAll();
				if (resultAsync != null)
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						Items = new ObservableCollection<CountPlan>(resultAsync);
					});
				}
			});
		}


		private void AsyncLoadDetailsPlan()
		{
			IsPage = false;

			Task.Run(async () =>
			{
				var list = new List<ViewCountPlanDetailExtend>();
				var result = await countServices.GetById(IdPlan);
				if (result != null)
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						list.AddRange(result.Listrows.Select(item => new ViewCountPlanDetailExtend()
						{
							Id = item.Id,
							IdCountPlan = item.IdCountPlan,
							Name = item.Name,
							Description = item.Description,
							ProductCode = item.ProductCode,
							Quantity = item.Quantity,
							TotalCounted = item.TotalCounted,
							BarCode = item.BarCode,
							ProductDescription = item.ProductDescription,
							TotalProduct = item.TotalProduct,
							HasDetails = (item.TotalProduct > 0) ? "Images/check.png" : "Images/uncheck.png",
							ProductOk = (item.Quantity <= item.TotalProduct) ? "Images/yes.png" : "Images/no.png"
						}));

						if (list.Count() > 0)
						{
							PlanName = result.Listrows.FirstOrDefault().Name;
							PlanDescription = result.Listrows.FirstOrDefault().Name + " " + result.Listrows.FirstOrDefault().Description;
						}


						ItemsCount = result.Count;
						NumberPage = 1;

						if (ItemsCount > 10)
						{
							decimal valueItemscount = Convert.ToDecimal(ItemsCount);
							decimal valueItemsperpages = Convert.ToDecimal(ITEM_PER_PAGE);
							decimal valueResult = Math.Ceiling(valueItemscount / valueItemsperpages);

							TotalPage = Convert.ToInt32(valueResult);

							MsgPage = Convert.ToString(NumberPage) + " / " + Convert.ToString(TotalPage);
							IsPage = true;
						}


						Details = new ObservableCollection<ViewCountPlanDetailExtend>(list);
					});
				}
			});


		}

		#region METHODS



		private void NextPageList()
		{
			if (TotalPage != NumberPage)
			{
				Pagination();

				NumberPage += 1;
				MsgPage = Convert.ToString(NumberPage) + " / " + Convert.ToString(TotalPage);
			}
		}

		private void PrevPageList()
		{

			NumberPage -= 1;
			if (NumberPage > 0)
			{

				Pagination();
				MsgPage = Convert.ToString(NumberPage) + " / " + Convert.ToString(TotalPage);
			}
		}

		private async void Pagination()
		{
			var list = new List<ViewCountPlanDetailExtend>();
			int _skip = NumberPage * ITEM_PER_PAGE;
			var result = await countServices.GetByIdSkipTake(IdPlan, _skip, ITEM_PER_PAGE);
			list.AddRange(result.Listrows.Select(item => new ViewCountPlanDetailExtend()
			{
				Id = item.Id,
				IdCountPlan = item.IdCountPlan,
				Name = item.Name,
				Description = item.Description,
				ProductCode = item.ProductCode,
				Quantity = item.Quantity,
				TotalCounted = item.TotalCounted,
				BarCode = item.BarCode,
				ProductDescription = item.ProductDescription,
				TotalProduct = item.TotalProduct,
				HasDetails = (item.TotalProduct > 0) ? "Images/check.png" : "Images/uncheck.png",
				ProductOk = (item.Quantity <= item.TotalProduct) ? "Images/yes.png" : "Images/no.png"
			}));

			Details = new ObservableCollection<ViewCountPlanDetailExtend>(list);
		}

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}


		private async void LoadItems()
		{
			var result = await countServices.GetAll();
			if (result != null)
			{
				Items = new ObservableCollection<CountPlan>(result);
			}
		}

		private void AsyncSearch()
		{
			try
			{

				Task.Run(async () =>
				{
					var _list = Details;
					var result = await countServices.GetByPlanAndProduct(IdPlan, BCode);   //_list.FirstOrDefault(a => a.BarCode == BCode);
					if (result.Any())
					{
						Device.BeginInvokeOnMainThread(() =>
						{
							Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PlanDetails(result, User, PlanDescription, BCode));
						});
					}
					else
						MessageResult = "Producto no Registrado";

				});


			}
			catch (Exception ex)
			{
				App.Current.MainPage.DisplayAlert("TSHIRT", "Error: " + BCode + " : " + ex.Message, "OK");
			}
		}


		public async void Reload()
		{

			App.Current.MainPage.Navigation.NavigationStack.Last().FindByName<Button>("BtnRefresh").Text = "Buscando...";
			App.Current.MainPage.Navigation.NavigationStack.Last().FindByName<Button>("BtnRefresh").IsEnabled = false;

			var result = await syncServices.Execute("IV10300");

			if (result)
			{
				LoadItems();
			}
			else
			{
				await App.Current.MainPage.DisplayAlert("TSHIRT", "Error al sincronizar los planes de conteo", "OK");
			}

			App.Current.MainPage.Navigation.NavigationStack.Last().FindByName<Button>("BtnRefresh").Text = "Datos Actualizados";
			App.Current.MainPage.Navigation.NavigationStack.Last().FindByName<Button>("BtnRefresh").IsEnabled = true;
		}

		public async void ValidatePlan()
		{
			List<ViewCountPlanDetailExtend> rows = new List<ViewCountPlanDetailExtend>();
			bool result = true;
			foreach (var item in Details.ToList().Where(item => item.TotalProduct == 0))
			{
				result = false;
			}


			// rows =  Details.Where(item => item.TotalProduct > 0).ToList();
			rows = Details.ToList();

			if (!result)
			{
				var answer = await App.Current.MainPage.DisplayAlert("TSHIRT", PlanName + " Aun tiene productos sin contar, Desea Continuar?", "SI", "NO");
				if (answer)
				{
					await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PlanResume(rows, PlanName, User));
				}
			}
			else
				await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PlanResume(rows, PlanName, User));



		}

		public async void Close()
		{
			await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new MainMenu(User));
		}

		#endregion

		#region PROPERTIES

		private ObservableCollection<CountPlan> _items = new ObservableCollection<CountPlan>();

		public ObservableCollection<CountPlan> Items
		{
			get { return _items; }
			set
			{
				_items = value;
				HeightList = (_items.Count * 45) + (_items.Count * 5);
				OnPropertyChanged("Items");
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


		private ObservableCollection<ViewCountPlanDetailExtend> _details = new ObservableCollection<ViewCountPlanDetailExtend>();

		public ObservableCollection<ViewCountPlanDetailExtend> Details
		{
			get { return _details; }
			set
			{
				_details = value;
				HeightList = (_details.Count * 80) + (_details.Count * 5);
				OnPropertyChanged("Details");
			}
		}


		private ObservableCollection<ViewCountPlanDetailExtend> _detailsList = new ObservableCollection<ViewCountPlanDetailExtend>();

		public ObservableCollection<ViewCountPlanDetailExtend> DetailList
		{
			get { return _detailsList; }
			set
			{
				_detailsList = value;
				HeightList = (_detailsList.Count * 80) + (_detailsList.Count * 5);
				OnPropertyChanged("DetailList");
			}
		}


		private string _planDescription;

		public string PlanDescription
		{
			get { return _planDescription; }
			set
			{
				_planDescription = value;
				OnPropertyChanged("PlanDescription");
			}
		}


		private string _messageResult;

		public string MessageResult
		{
			get { return _messageResult; }
			set
			{
				_messageResult = value;
				OnPropertyChanged("MessageResult");
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

		private string _user;

		public string User
		{
			get { return _user; }
			set
			{
				_user = value;
				OnPropertyChanged("User");
			}
		}

		private string _plan;

		public string PlanName
		{
			get { return _plan; }
			set
			{
				_plan = value;
				OnPropertyChanged("PlanName");
			}
		}

		private int _idPlan;

		public int IdPlan
		{
			get { return _idPlan; }
			set
			{
				_idPlan = value;
				OnPropertyChanged("IdPlan");
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
		#endregion


		#endregion

		#region COMMANDS

		public ICommand ProcessPlan => new RelayCommand(ValidatePlan);

		public ICommand ClosePlan => new RelayCommand(Close);

		public ICommand SearchProduct
		{
			get { return new RelayCommand(AsyncSearch); }
		}

		public ICommand Refresh
		{
			get { return new RelayCommand(Reload); }
		}

		//PrevPage

		public ICommand PrevPage
		{
			get { return new RelayCommand(PrevPageList); }
		}


		public ICommand NextPage
		{
			get { return new RelayCommand(NextPageList); }
		}
		#endregion

	}
}