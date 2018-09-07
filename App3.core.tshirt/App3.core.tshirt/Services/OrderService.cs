﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.Models;
using Newtonsoft.Json;

namespace App3.core.tshirt.Services
{
	public class OrderService
	{
		HttpClient client;
		public List<OrderTShirt> Items { get; private set; }
		private string PATHSERVER { get; set; }

		public OrderService()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 25600000;

			PATHSERVER = "10.1.92.207:83";
		}

		public async Task<List<OrderTShirt>> GetOrderProviders(string codeProvider)
		{
			Items = new List<OrderTShirt>();
			string url = "http://" + PATHSERVER + "/tshirt/provider/GetProviderOrder?code=";
			string uri = string.Concat(url, codeProvider);
			try
			{
				var result = await client.GetAsync(uri);
				if (result.IsSuccessStatusCode)
				{
					var content = await result.Content.ReadAsStringAsync();
					Items = JsonConvert.DeserializeObject<List<OrderTShirt>>(content);
				}
			}
			catch (Exception ex)
			{
				//Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return Items;
		}


		public async Task<List<ViewOrder>> GetListOrder(List<OrderTShirt> codes)
		{
			var orders = new List<ViewOrder>();

			string url = "http://" + PATHSERVER + "/tshirt/order/GetOrdersDetails";

			try
			{
				var json = JsonConvert.SerializeObject(codes);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage result = null;

				result = await client.PostAsync(url, content);

				if (result.IsSuccessStatusCode)
				{
					var x = await result.Content.ReadAsStringAsync();
					orders = JsonConvert.DeserializeObject<List<ViewOrder>>(x);
					if (orders.Count == 0)
					{
						orders = null;
					}
				}
			}
			catch (Exception ex)
			{
				//Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return orders;
		}


		public async Task<OrderTShirt> GetOrder(string code)
		{
			var orders = new OrderTShirt();
			string url = "http://" + PATHSERVER + "/tshirt/order/GetOrder?code=";
			string uri = string.Concat(url, code);
			try
			{
				var result = await client.GetAsync(uri);
				if (result.IsSuccessStatusCode)
				{
					var content = await result.Content.ReadAsStringAsync();
					orders = JsonConvert.DeserializeObject<OrderTShirt>(content);
				}
			}
			catch (Exception ex)
			{
				//Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return orders;
		}


		public async Task<List<OrderDetailProduct>> GetOrderDetailProduct(List<OrderDetailProduct> codes)
		{
			var orders = new List<OrderDetailProduct>();
			string url = "http://" + PATHSERVER + "/tshirt/order/GetOrdersDetailsProduct";
			try
			{
				var json = JsonConvert.SerializeObject(codes);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage result = null;

				result = await client.PostAsync(url, content);

				if (result.IsSuccessStatusCode)
				{
					var x = await result.Content.ReadAsStringAsync();
					orders = JsonConvert.DeserializeObject<List<OrderDetailProduct>>(x);
					if (orders.Count == 0)
					{
						orders = null;
					}
				}
			}
			catch (Exception ex)
			{
				//Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return orders;

		}


		public async Task<OrderDetailProduct> AddOrdeDetailProduct(OrderDetailProduct detail)
		{
			var _detail = new OrderDetailProduct();
			string url = "http://" + PATHSERVER + "/tshirt/order/PostOrderDetailProduct";
			try
			{
				var json = JsonConvert.SerializeObject(detail);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage result = null;

				result = await client.PostAsync(url, content);

				if (result.IsSuccessStatusCode)
				{
					var x = await result.Content.ReadAsStringAsync();
					_detail = JsonConvert.DeserializeObject<OrderDetailProduct>(x);
				}
			}
			catch (Exception ex)
			{
				//Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return _detail;
		}

	}
}
