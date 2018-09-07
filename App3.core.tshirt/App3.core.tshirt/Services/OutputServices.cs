using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App3.core.tshirt.Models;
using Newtonsoft.Json;

namespace App3.core.tshirt.Services
{
	public class OutputServices
	{
		private string PATHSERVER { get; set; }
		HttpClient client;

		public OutputServices()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
			PATHSERVER = "10.1.92.207:83";
		}

		public async Task<WarehouseProduct> GetWarehouseProduct(string warehouseCode, string productCode)
		{
			WarehouseProduct warehouseProduct = null;
			string url = "http://" + PATHSERVER + "/tshirt/warehouseproduct/GetWarehouseProductByCodes";
			string _warehouseCode = "?warehouseCode=" + warehouseCode;
			string _productCode = "&productCode=" + productCode;
			string uri = string.Concat(url, _warehouseCode, _productCode);

			try
			{
				HttpResponseMessage result = null;

				result = await client.GetAsync(uri);

				if (result.IsSuccessStatusCode)
				{
					var x = await result.Content.ReadAsStringAsync();
					warehouseProduct = JsonConvert.DeserializeObject<WarehouseProduct>(x);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return warehouseProduct;
		}

		public async Task<Models.Output> SaveOutput(Models.Output output)
		{
			var _output = output;
			string url = "http://" + PATHSERVER + "/tshirt/output/save";
			try
			{
				string json = JsonConvert.SerializeObject(output);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				Debug.WriteLine(json);

				HttpResponseMessage result = null;

				result = await client.PostAsync(url, content);

				if (result.IsSuccessStatusCode)
				{
					var x = await result.Content.ReadAsStringAsync();
					_output.Id = JsonConvert.DeserializeObject<int>(x);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return _output;
		}

		public async Task<List<Models.Output>> GetList(int quantity, int? code)
		{

			List<Models.Output> list = null;
			string url = "http://" + PATHSERVER + "/tshirt/output/GetList";
			string _quantity = "?quantity=" + quantity;
			string _code = "&id=" + code;
			string uri = string.Concat(url, _quantity, _code);

			Debug.WriteLine("uri " + uri);

			try
			{
				HttpResponseMessage result = null;

				result = await client.GetAsync(uri);

				if (result.IsSuccessStatusCode)
				{
					var x = await result.Content.ReadAsStringAsync();
					list = JsonConvert.DeserializeObject<List<Models.Output>>(x);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return list;
		}

		public async Task<Models.Output> Get(int id)
		{
			Models.Output output = null;
			string url = "http://" + PATHSERVER + "/tshirt/output/Get";
			string _id = "?id=" + id;
			string uri = string.Concat(url, _id);

			try
			{
				HttpResponseMessage result = null;

				result = await client.GetAsync(uri);

				if (result.IsSuccessStatusCode)
				{
					var x = await result.Content.ReadAsStringAsync();
					output = JsonConvert.DeserializeObject<Models.Output>(x);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return output;
		}
	}
}
