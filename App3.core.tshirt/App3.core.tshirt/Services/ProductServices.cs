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
    public class ProductServices
	{
        HttpClient client;
        private string PATHSERVER { get; set; }

        public ProductServices()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            PATHSERVER = "10.1.92.207:81"; //Resources.PathServer;
        }

        public async Task<List<Product>> Search(string code)
        {
            var items = new List<Product>();
            string url = "http://" + PATHSERVER + "/tshirt/product/search?code=";
            string uri = string.Concat(url, code);
            try
            {
                var result = await client.GetAsync(uri);
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<Product>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return items;
        }


    }
}
