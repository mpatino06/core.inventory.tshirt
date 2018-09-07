using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App3.core.tshirt.Models;
using App3.core.tshirt.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App3.core.tshirt.ViewModels
{
	public class CountDetailViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private CountServices countServices;

		public CountDetailViewModel(List<ViewCountPlanDetailItem> items, string user, string description, string barCodeProduct,
			string nameProduct)
		{
			countServices = new CountServices();
			PlanDescription = description;
			BarCodeProduct = barCodeProduct;
			NameProduct = nameProduct;
			User = user;
			PlanId = items.FirstOrDefault().CountPlanId; // idPlan;
			ProductCode = items.FirstOrDefault().ProductCode; // product;
			LoadDetails(items, user);

		}


		private void LoadDetails(List<ViewCountPlanDetailItem> items, string user)
		{
			Task.Run(() =>
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					if (items.Count > 0)
					{
						var data = new List<ViewCountPlanDetailItem>();
						data = items.Where(a => a.UserCode == user).ToList();
						Details = new ObservableCollection<ViewCountPlanDetailItem>(items.Where(a => a.UserCode == user).ToList());
						LastRowCount = (!Details.Any()) ? 0 : Details.LastOrDefault().Count;
						CountProduct = int.Parse(items.Sum(a => a.Quantity).ToString());

					}
					else
					{
						Details = new ObservableCollection<ViewCountPlanDetailItem>(items);
						LastRowCount = 0;
					}
				});
			});
		}

		private async void Save()
		{
			var answer = await App.Current.MainPage.DisplayAlert("TSHIRT", "Esta seguro que desea Guardar?", "SI", "NO");
			if (answer)
			{

				var qry = NewDetails.Select(a => new CountPlanDetailItem()
				{
					Id = a.Id,
					CountPlanId = a.CountPlanId,
					UserCode = a.UserCode,
					DateCreated = a.DateCreated,
					Quantity = a.Quantity,
					ProductCode = a.ProductCode,
					Count = a.Count
				}).ToList();
				var result = await countServices.SaveDetail(qry);
				if (result == "OK")
				{

					Device.BeginInvokeOnMainThread(async () =>
					{
						await
							Xamarin.Forms.Application.Current.MainPage
								.Navigation.PopAsync();
					});

				}
				else
				{
					await App.Current.MainPage.DisplayAlert("TSHIRT - Error", result, "OK");
				}
			}

		}


		#region PROPERTIES

		private ObservableCollection<IGrouping<String, CountPlanDetailItem>> _groupedList = null;

		public ObservableCollection<IGrouping<String, CountPlanDetailItem>> GroupedList
		{
			get { return _groupedList; }
			set
			{
				_groupedList = value;
				HeightList = (_details.Count * 40) + (_details.Count * 10);
				OnPropertyChanged("GroupedList");
			}
		}

		private ObservableCollection<ViewCountPlanDetailItem> _details = new ObservableCollection<ViewCountPlanDetailItem>();

		public ObservableCollection<ViewCountPlanDetailItem> Details
		{
			get { return _details; }
			set
			{
				_details = value;
				HeightList = (_details.Count * 40) + (_details.Count * 10);
				OnPropertyChanged("Details");
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

		private string _barCodeProduct;

		public string BarCodeProduct
		{
			get { return _barCodeProduct; }
			set
			{
				_barCodeProduct = value;
				OnPropertyChanged("BarCodeProduct");
			}
		}


		private string _nameProduct;

		public string NameProduct
		{
			get { return _nameProduct; }
			set
			{
				_nameProduct = value;
				OnPropertyChanged("NameProduct");
			}
		}

		private int _addQuantity;

		public int AddQuantity
		{
			get { return _addQuantity; }
			set
			{
				_addQuantity = value;
				OnPropertyChanged("AddQuantity");
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

		private ObservableCollection<ViewCountPlanDetailItem> _newdetails = new ObservableCollection<ViewCountPlanDetailItem>();

		public ObservableCollection<ViewCountPlanDetailItem> NewDetails
		{
			get { return _newdetails; }
			set
			{
				_newdetails = value;
				OnPropertyChanged("NewDetails");
			}
		}

		private int _planId;

		public int PlanId
		{
			get { return _planId; }
			set
			{
				_planId = value;
				OnPropertyChanged("PlanId");
			}
		}


		private string _productCode;

		public string ProductCode
		{
			get { return _productCode; }
			set
			{
				_productCode = value;
				OnPropertyChanged("ProductCode");
			}
		}

		private int _lastRowCount;

		public int LastRowCount
		{
			get { return _lastRowCount; }
			set
			{
				_lastRowCount = value;
				OnPropertyChanged("LastRowCount");
			}
		}

		private int _countProduct;
		public int CountProduct
		{
			get { return _countProduct; }
			set
			{
				_countProduct = value;
				OnPropertyChanged("CountProduct");
			}
		}


		private void AddItem()
		{


			var details = new ViewCountPlanDetailItem();


			details.CountPlanId = PlanId;
			details.UserCode = User;
			details.DateCreated = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
			details.Quantity = AddQuantity;
			details.ProductCode = ProductCode;
			details.Count = LastRowCount + 1;

			CountProduct = CountProduct + AddQuantity;

			NewDetails.Add(details);

			int countItemsDetail = _details.Count == 0 ? 1 : _details.Count;

			if (Details.Count == 1)
			{
				HeightList = (countItemsDetail * 60) + (countItemsDetail * 15);
			}
			else
			{
				HeightList = (countItemsDetail * 40) + (countItemsDetail * 12);
			}

			//HeightList = (countItemsDetail*40) + (countItemsDetail*10);

			Details.Add(details);

			//else
			//{
			//    await App.Current.MainPage.DisplayAlert("TSHIRT - Error", "Ingrese un valor Númerico", "OK");
			//}
			AddQuantity = 0;

			App.Current.MainPage.Navigation.NavigationStack.Last().FindByName<Entry>("EntrQuantity").Focus();

		}



		#endregion


		#region COMMANDS

		public ICommand QuantityByNumber
		{
			get { return new RelayCommand(AddItem); }
		}

		public ICommand SaveCount
		{
			get { return new RelayCommand(Save); }
		}





		#endregion


		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}


	}
}

