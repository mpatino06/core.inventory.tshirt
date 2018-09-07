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
			PATHSERVER = "10.1.92.207:83"; // "PRUEBA";
		}


		public async Task<List<CountPlan>> GetAll()
		{
			Items = new List<CountPlan>();
			string uri = "http://" + PATHSERVER + "/tshirt/Count/GetAll";

			try
			{
				var result = await client.GetAsync(uri);

				if (result.IsSuccessStatusCode)
				{

					var content = await result.Content.ReadAsStringAsync();
					Items = JsonConvert.DeserializeObject<List<CountPlan>>(content);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}

			return Items;
		}

		public async Task<ViewCountPlanDetailPage> GetById(int id)
		{
			var plan = new ViewCountPlanDetailPage();
			string url = "http://" + PATHSERVER + "/tshirt/Count/GetCountByIdPage?id=";
			string uri = string.Concat(url, id);
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
