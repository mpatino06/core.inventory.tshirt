﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.Models;
using Newtonsoft.Json;

namespace App3.core.tshirt.Services
{
	public class ProductChangeServices
	{
		private HttpClient client;
		private string PATHSERVER { get; set; }

		public ProductChangeServices()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 2560000;
			PATHSERVER = "10.1.92.207:83"; // Resources.PathServer;
		}

		public async Task<OrderReqExtend> GetDetailByCode(string code)
		{

			var items = new OrderReqExtend();
			string url = "http://" + PATHSERVER + "/tshirt/productchange/GetDetailByCode?code=";
			string uri = string.Concat(url, code);
			try
			{
				var result = await client.GetAsync(uri);
				if (result.IsSuccessStatusCode)
				{
					var content = await result.Content.ReadAsStringAsync();
					items = JsonConvert.DeserializeObject<OrderReqExtend>(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return items;
		}


		public async Task<OrderReqExtend> GetOrderByCode(string code)
		{
			var items = new OrderReqExtend();
			string url = "http://" + PATHSERVER + "/tshirt/productchange/GetOrderByCode?code=";
			string uri = string.Concat(url, code);
			try
			{
				var result = await client.GetAsync(uri);
				if (result.IsSuccessStatusCode)
				{
					var content = await result.Content.ReadAsStringAsync();
					items = JsonConvert.DeserializeObject<OrderReqExtend>(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return items;
		}


		public async Task<List<OrderReqDetailExtend>> GetAll()
		{
			var items = new List<OrderReqDetailExtend>();
			string url = "http://" + PATHSERVER + "/tshirt/productchange/GetAllDetail";
			int count = 0;
			try
			{
				var result = await client.GetAsync(url);
				if (result.IsSuccessStatusCode)
				{
					var content = await result.Content.ReadAsStringAsync();
					items = JsonConvert.DeserializeObject<List<OrderReqDetailExtend>>(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return items;
		}


		//Tuple permite devolver en un metodo dos resultados
		public async Task<Tuple<List<OrderReqExtend>, int>> GetAllWeb(int aPage, int aElementsPerPage, string aSearchString)
		{
			var items = new List<OrderReqExtend>();
			string url = "http://" + PATHSERVER + "/tshirt/productchange/GetAll";
			int count = 0;
			try
			{
				var result = await client.GetAsync(url);
				if (result.IsSuccessStatusCode)
				{
					var content = await result.Content.ReadAsStringAsync();
					items = JsonConvert.DeserializeObject<List<OrderReqExtend>>(content);
					count = items.Count;
					items.OrderByDescending(a => a.Id).Skip(aPage * aElementsPerPage).Take(aElementsPerPage).ToList();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return Tuple.Create(items, count);
		}

		public async Task<bool> UpdateOrder(OrderReqExtend codes)
		{
			bool result = false;
			string url = "http://" + PATHSERVER + "/tshirt/productchange/SaveOrder";

			try
			{
				var json = JsonConvert.SerializeObject(codes);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage request = null;

				request = await client.PostAsync(url, content);

				if (request.IsSuccessStatusCode)
				{
					var x = await request.Content.ReadAsStringAsync();
					result = JsonConvert.DeserializeObject<bool>(x);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return result;

		}

		public async Task<bool> UpdateDetail(OrderReqDetailExtend codes)
		{
			bool result = false;
			string url = "http://" + PATHSERVER + "/tshirt/productchange/SaveDetail";

			try
			{
				var json = JsonConvert.SerializeObject(codes);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				HttpResponseMessage request = null;

				request = await client.PostAsync(url, content);

				if (request.IsSuccessStatusCode)
				{
					var x = await request.Content.ReadAsStringAsync();
					result = JsonConvert.DeserializeObject<bool>(x);

				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return result;

		}
	}
}

