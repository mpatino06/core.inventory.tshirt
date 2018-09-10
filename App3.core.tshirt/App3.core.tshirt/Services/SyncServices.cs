using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App3.core.tshirt.Services
{
	public class SyncServices
	{
		private string PATHSERVER { get; set; }
		HttpClient client;

		public SyncServices()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 2560000;
			PATHSERVER = "10.1.92.207:81";
		}

		public async Task<bool> Execute(string processName)
		{
			string url = "http://" + PATHSERVER + "/tshirt/sync/execute";
			string _processName = "?processName=" + processName;
			string uri = string.Concat(url, _processName);

			try
			{
				HttpResponseMessage response = null;
				response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
			}
			return false;
		}
	}
}
