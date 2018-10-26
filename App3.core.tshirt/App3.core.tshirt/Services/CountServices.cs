using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.Models;
using Newtonsoft.Json;

namespace App3.core.tshirt.Services
{
	public class CountServices
	{

		private HttpClient client;
		private string PATHSERVER { get; set; }

		public List<CountPlan> Items { get; private set; }


		public CountServices()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 2560000;
			//client.Timeout = TimeSpan.FromSeconds(180);
			PATHSERVER = "10.1.92.207:81"; // "PRUEBA";
		}


		public async Task<List<CountPlan>> GetAll()
		{
			//Items = new List<CountPlan>();
			//string uri = "http://" + PATHSERVER + "/tshirt/Count/GetAll";
			try
			{
				//var result = await client.GetAsync(uri);

				//if (result.IsSuccessStatusCode)
				//{
				var content = "[{'Id': 1,'Name': 'PLAN CONTEO 1','Description': 'DESC PLAN CONTEO 1','Status': '2','DateCreated': '','Warehouse': '','Value2': '','Value3': '','Value4': '','Value5': '','UserUpdated': '','DateUpdated': ''},{'Id': 2,'Name': 'PLAN CONTEO 2','Description': 'DESC PLAN CONTEO 2','Status': '2','DateCreated': '','Warehouse': '','Value2': '','Value3': '','Value4': '','Value5': '','UserUpdated': '','DateUpdated': ''},{'Id': 3,'Name': 'PLAN CONTEO 3','Description': 'DESC PLAN CONTEO 3','Status': '2','DateCreated': '','Warehouse': '','Value2': '','Value3': '','Value4': '','Value5': '','UserUpdated': '','DateUpdated': ''},{'Id': 4,'Name': 'PLAN CONTEO 4','Description': 'DESC PLAN CONTEO 4','Status': '2','DateCreated': '','Warehouse': '','Value2': '','Value3': '','Value4': '','Value5': '','UserUpdated': '','DateUpdated': ''},{'Id': 5,'Name': 'PLAN CONTEO 5','Description': 'DESC PLAN CONTEO 5','Status': '2','DateCreated': '','Warehouse': '','Value2': '','Value3': '','Value4': '','Value5': '','UserUpdated': '','DateUpdated': ''},{'Id': 6,'Name': 'PLAN CONTEO 6','Description': 'DESC PLAN CONTEO 6','Status': '2','DateCreated': '','Warehouse': '','Value2': '','Value3': '','Value4': '','Value5': '','UserUpdated': '','DateUpdated': ''},{'Id': 7,'Name': 'PLAN CONTEO 7','Description': 'DESC PLAN CONTEO 7','Status': '2','DateCreated': '','Warehouse': '','Value2': '','Value3': '','Value4': '','Value5': '','UserUpdated': '','DateUpdated': ''},{'Id': 8,'Name': 'PLAN CONTEO 8','Description': 'DESC PLAN CONTEO 8','Status': '2','DateCreated': '','Warehouse': '','Value2': '','Value3': '','Value4': '','Value5': '','UserUpdated': '','DateUpdated': ''},{'Id': 9,'Name': 'PLAN CONTEO 9','Description': 'DESC PLAN CONTEO 9','Status': '2','DateCreated': '','Warehouse': '','Value2': '','Value3': '','Value4': '','Value5': '','UserUpdated': '','DateUpdated': ''},{'Id': 10,'Name': 'PLAN CONTEO 10','Description': 'DESC PLAN CONTEO 10','Status': '2','DateCreated': '','Warehouse': '','Value2': '','Value3': '','Value4': '','Value5': '','UserUpdated': '','DateUpdated': ''}]";
				//var content = await result.Content.ReadAsStringAsync();
				Items = JsonConvert.DeserializeObject<List<CountPlan>>(content);
				//}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}

			return Items;
		}

		public async Task<ViewCountPlanDetailPage> GetById(int id)
		{
			var result = new ViewCountPlanDetailPage();

			//var plan = new ViewCountPlanDetailPage();
			//string url = "http://" + PATHSERVER + "/tshirt/Count/GetCountByIdPage?id=";
			//string uri = string.Concat(url, id);
			try
			{
				//	var result = await client.GetAsync(uri);
				//	if (result.IsSuccessStatusCode)
				//	{				
				//var content = await result.Content.ReadAsStringAsync();
				//plan = JsonConvert.DeserializeObject<ViewCountPlanDetailPage>(content);
				//}

				
				List<ListItems> rows = new List<ListItems>();

				var _list1 = new ListItems
				{
					Id = 1,
					IdCountPlan = 1,
					Name = "PC1",
					Description = "PLAN CONTEO 1",
					ProductCode = "PROD00001",
					Quantity = 39,
					BarCode = "PROD00001",
					ProductDescription = "PRODUCTO 1",
					TotalProduct = 0,
					Warehouse = "ALMACEN1"
				};
				rows.Add(_list1);
				var _list2 = new ListItems
				{
					Id = 2,
					IdCountPlan = 1,
					Name = "PC1",
					Description = "PLAN CONTEO 1",
					ProductCode = "PROD00002",
					Quantity = 2,
					BarCode = "PROD00002",
					ProductDescription = "PRODUCTO 2",
					TotalProduct = 0,
					Warehouse = "ALMACEN1"
				};
				rows.Add(_list2);
				var _list3 = new ListItems
				{
					Id = 3,
					IdCountPlan = 1,
					Name = "PC1",
					Description = "PLAN CONTEO 1",
					ProductCode = "PROD00003",
					Quantity = 2,
					BarCode = "PROD00003",
					ProductDescription = "PRODUCTO 3",
					TotalProduct = 0,
					Warehouse = "ALMACEN1"
				};
				rows.Add(_list3);
				var _list4 = new ListItems
				{
					Id = 4,
					IdCountPlan = 1,
					Name = "PC1",
					Description = "PC1",
					ProductCode = "PROD00004",
					Quantity = 4,
					BarCode = "PROD00004",
					ProductDescription = "PRODUCTO 4",
					TotalProduct = 0,
					Warehouse = "ALMACEN1"
				};
				rows.Add(_list4);
				var _list5 = new ListItems
				{
					Id = 5,
					IdCountPlan = 1,
					Name = "PC1",
					Description = "PLAN CONTEO 1",
					ProductCode = "PROD 00005",
					Quantity = 2,
					BarCode = "PROD00005",
					ProductDescription = "PRODUCTO 5",
					TotalProduct = 0,
					Warehouse = "ALMACEN1"
				};
				rows.Add(_list5);
				var _list6 = new ListItems
				{
					Id = 6,
					IdCountPlan = 1,
					Name = "PC1",
					Description = "PLAN CONTEO 1",
					ProductCode = "PROD00006",
					Quantity = 98,
					BarCode = "PROD00006",
					ProductDescription = "PRODUCTO 6",
					TotalProduct = 0,
					Warehouse = "ALMACEN1"
				};
				rows.Add(_list6);
				var _list7 = new ListItems
				{
					Id = 7,
					IdCountPlan = 1,
					Name = "PC1",
					Description = "PLAN CONTEO 1",
					ProductCode = "PROD00007",
					Quantity = 6,
					BarCode = "PROD00007",
					ProductDescription = "PRODUCTO 7",
					TotalProduct = 0,
					Warehouse = "ALMACEN1"
				};
				rows.Add(_list7);

				result.Listrows = rows;
				result.Count = 7;				
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return result;
			//return plan;
		}

		public async Task<ViewCountPlanDetailPage> GetByIdSkipTake(int id, int skip, int take)
		{
			var plan = new ViewCountPlanDetailPage();
			string url = "http://" + PATHSERVER + "/tshirt/Count/GetCountByIdPageSkipTake?";
			string parameter1 = "id=" + id;
			string parameter2 = "&skip=" + skip;
			string parameter3 = "&take=" + take;
			string uri = string.Concat(url, parameter1, parameter2, parameter3);
			try
			{
				var result = await client.GetAsync(uri);
				if (result.IsSuccessStatusCode)
				{
					var content = await result.Content.ReadAsStringAsync();
					plan = JsonConvert.DeserializeObject<ViewCountPlanDetailPage>(content);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return plan;
		}



		public async Task<string> SaveDetail(List<CountPlanDetailItem> items)
		{
			string plan = string.Empty;
			string url = "http://" + PATHSERVER + "/tshirt/Count/SaveDetails";

			try
			{
				var json = JsonConvert.SerializeObject(items);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage result = null;

				result = await client.PostAsync(url, content);

				if (result.IsSuccessStatusCode)
				{
					var x = await result.Content.ReadAsStringAsync();
					plan = JsonConvert.DeserializeObject<string>(x);

				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return plan;
		}


		public async Task<List<ViewCountPlanDetailItem>> GetByPlanAndProduct(int id, string product)
		{
			var plan = new List<ViewCountPlanDetailItem>();
			string url = "http://" + PATHSERVER + "/tshirt/Count/GetCountByPlanAndProduct?";
			string parameter1 = "id=" + id;
			string parameter2 = "&product=" + product;
			string uri = string.Concat(url, parameter1, parameter2);
			try
			{
				var result = await client.GetAsync(uri);
				if (result.IsSuccessStatusCode)
				{
					var content = await result.Content.ReadAsStringAsync();
					plan = JsonConvert.DeserializeObject<List<ViewCountPlanDetailItem>>(content);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return plan;
		}


		public async Task<bool> SaveCountPlan(CountPlan item)
		{
			bool plan = true;
			string url = "http://" + PATHSERVER + "/tshirt/Count/SavePlan";

			try
			{
				var json = JsonConvert.SerializeObject(item);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage result = null;

				result = await client.PostAsync(url, content);

				if (result.IsSuccessStatusCode)
				{
					var x = await result.Content.ReadAsStringAsync();
					plan = JsonConvert.DeserializeObject<bool>(x);

				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return plan;
		}

	}
}
