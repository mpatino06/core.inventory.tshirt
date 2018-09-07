using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using App3.core.tshirt.Infrastructure;
using App3.core.tshirt.Models;
using App3.core.tshirt.Services;
using App3.core.tshirt.Views.Count;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App3.core.tshirt.ViewModels
{
	public class CountResumeViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public Color COLORDEFAULT = Color.Green;
		private CountServices countServices;
		private const int ITEM_PER_PAGE = 10;

		public CountResumeViewModel(List<ViewCountPlanDetailExtend> plan, string planName, string user)
		{
			User = user;
			countServices = new CountServices();
			//int total = 0, _quantity = 0, _totalProduct = 0;
			var list = new List<ViewCountPlanDetailExtend>();


			Porcentaje = 3; //int.Parse(Resources.PorcentajeConteo); //Obtiene valor porcentaje


			//TODO hablar con ERIK sobre porcentaje
			////CALCULA PORCENTAJE
			//foreach (var row in plan)
			//{
			//    _quantity += row.Quantity;
			//    _totalProduct += row.TotalProduct;
			//}

			//total = (((_totalProduct * 100) / _quantity) - 100) * -1;
			////FIN CALCULA PORCENTAJE

			////ASIGNA COLOR E IMAGEN SEGUN PORCENTAJE
			//if (total == 0)
			//{
			//    EstatusPlanConteo = (int)EnumTShirt.CountEstatus.SINDIFERENCIA;
			//    ImageStatusPlan = "@drawable/yes.png";
			//    ColorText = Color.Green;
			//}
			//else if (total < Porcentaje)
			//{
			//    EstatusPlanConteo = (int)EnumTShirt.CountEstatus.DIFERENCIAMEDIA;
			//    ImageStatusPlan = "@drawable/warning.png";
			//    ColorText = Color.Navy;
			//}
			//else
			//{
			//    EstatusPlanConteo = (int)EnumTShirt.CountEstatus.DIFERENCIAMAYOR;
			//    ImageStatusPlan = "@drawable/no.png";
			//    ColorText = Color.Red;
			//}

			//
			EstatusPlanConteo = (int)EnumTShirt.CountEstatus.DIFERENCIAMEDIA;
			ImageStatusPlan = "@drawable/warning.png";
			ColorText = Color.Navy;




			ItemsCount = plan.Count();
			NumberPage = 1;
			if (ItemsCount > 10)
			{
				decimal valueItemscount = Convert.ToDecimal(list.Count());
				decimal valueItemsperpages = Convert.ToDecimal(ITEM_PER_PAGE);
				decimal valueResult = Math.Ceiling(valueItemscount / valueItemsperpages);

				TotalPage = Convert.ToInt32(valueResult);

				MsgPage = Convert.ToString(NumberPage) + " / " + Convert.ToString(TotalPage);
				IsPage = true;
			}


			foreach (var item in plan)
			{
				list.Add(
					new ViewCountPlanDetailExtend()
					{
						Id = item.Id,
						IdCountPlan = item.IdCountPlan,
						Name = item.Name,
						Description = item.Description,
						ProductCode = item.ProductCode,
						Quantity = item.Quantity,
						TotalCounted = item.TotalCounted,
						BarCode = item.BarCode,
						ProductDescription = item.ProductDescription.Trim(),
						TotalProduct = item.TotalProduct,
						HasDetails = item.HasDetails,
						ProductOk = item.ProductOk,
						//RColor = (item.Quantity <= item.TotalProduct) ? COLORDEFAULT : ColorText
					});
			}

			DetailList = new ObservableCollection<ViewCountPlanDetailExtend>(list.Take(ITEM_PER_PAGE));
			Details = new ObservableCollection<ViewCountPlanDetailExtend>(list);
			PlanName = planName;


		}

		private void NextPageList()
		{
			if (TotalPage != NumberPage)
			{
				var list = Details.Skip(NumberPage * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
				DetailList = new ObservableCollection<ViewCountPlanDetailExtend>(list);
				NumberPage += 1;
				MsgPage = Convert.ToString(NumberPage) + " / " + Convert.ToString(TotalPage);
			}
		}

		private void PrevPageList()
		{

			NumberPage -= 1;
			if (NumberPage > 0)
			{
				MsgPage = Convert.ToString(NumberPage) + " / " + Convert.ToString(TotalPage);
				var list = (NumberPage > 1)
					? Details.Skip(NumberPage * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList()
					: Details.Take(ITEM_PER_PAGE).ToList();
				DetailList = new ObservableCollection<ViewCountPlanDetailExtend>(list);
			}
		}


		#region PROPERTIES
		private ObservableCollection<ViewCountPlanDetailExtend> _details = new ObservableCollection<ViewCountPlanDetailExtend>();
		public ObservableCollection<ViewCountPlanDetailExtend> Details
		{
			get { return _details; }
			set
			{
				_details = value;
				HeightList = (_details.Count * 45) + (_details.Count * 5);
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

		private string _planName;
		public string PlanName
		{
			get { return _planName; }
			set
			{
				_planName = value;
				OnPropertyChanged("PlanName");
			}
		}


		private Color _colorText;
		public Color ColorText
		{
			get { return _colorText; }
			set
			{
				_colorText = value;
				OnPropertyChanged("ColorText");
			}
		}

		private string _imageStatusPlan;
		public string ImageStatusPlan
		{
			get { return _imageStatusPlan; }
			set
			{
				_imageStatusPlan = value;
				OnPropertyChanged("ImageStatusPlan");
			}
		}

		private int _porcentaje;
		public int Porcentaje
		{
			get { return _porcentaje; }
			set
			{
				_porcentaje = value;
				OnPropertyChanged("Porcentaje");
			}
		}

		private int _estatusPlanConteo;
		public int EstatusPlanConteo
		{
			get { return _estatusPlanConteo; }
			set
			{
				_estatusPlanConteo = value;
				OnPropertyChanged("EstatusPlanConteo");
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


		#endregion

		public ICommand SavePlan
		{
			get { return new RelayCommand(Save); }
		}

		public ICommand PrevPage
		{
			get { return new RelayCommand(PrevPageList); }
		}


		public ICommand NextPage
		{
			get { return new RelayCommand(NextPageList); }
		}


		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		public async void Save()
		{
			var _plan = new CountPlan();
			bool result;
			// _plan.Value3 = Lote;   //value2 field used to Lote
			_plan.Id = Details.Select(a => a.IdCountPlan).FirstOrDefault();



			if (EstatusPlanConteo == (int)EnumTShirt.CountEstatus.SINDIFERENCIA)
			{
				_plan.Value2 = false.ToString();
				var answer = await App.Current.MainPage.DisplayAlert("TSHIRT", "Desea Guardar el Plan de Conteo", "SI", "NO");
				if (answer)
				{
					EnableButton();
					result = await countServices.SaveCountPlan(_plan);
					if (result)
					{
						await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PlanList(User));
					}
				}
			}
			else if (EstatusPlanConteo == (int)EnumTShirt.CountEstatus.DIFERENCIAMEDIA)
			{
				_plan.Value2 = false.ToString();
				var answer = await App.Current.MainPage.DisplayAlert("TSHIRT", "Diferencia MENOR al " + Porcentaje + "%, Desea Continuar?", "SI", "NO");
				if (answer)
				{
					EnableButton();
					result = await countServices.SaveCountPlan(_plan);
					if (result)
					{
						await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PlanList(User));
					}
				}
			}
			else
			{
				_plan.Value2 = true.ToString();
				var answer = await App.Current.MainPage.DisplayAlert("TSHIRT", "Diferencia MAYOR al " + Porcentaje + "%, Desea Continuar?", "SI", "NO");
				if (answer)
				{
					EnableButton();
					result = await countServices.SaveCountPlan(_plan);
					if (result)
					{
						await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PlanList(User));
					}

				}
			}
		}

		void EnableButton()
		{
			App.Current.MainPage.Navigation.NavigationStack.Last().FindByName<Button>("EntSave").Text = "Guardando...";
			App.Current.MainPage.Navigation.NavigationStack.Last().FindByName<Button>("EntSave").IsEnabled = false;
		}

	}
}
