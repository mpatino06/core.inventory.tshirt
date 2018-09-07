using System;
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
    public class RctServices
    {
        HttpClient client;
        private string PATHSERVER { get; set; }

        public RctServices()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 2560000;
            PATHSERVER = "10.1.92.207:83";
        }


        public async Task<RctExtendModel> Add(RctExtendModel model)
        {
            var _rct = new RctExtendModel();
            string url = "http://" + PATHSERVER + "/tshirt/Order/PostRct";

            try
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage result = null;

                result = await client.PostAsync(url, content);

                if (result.IsSuccessStatusCode)
                {
                    var x = await result.Content.ReadAsStringAsync();
                    _rct = JsonConvert.DeserializeObject<RctExtendModel>(x);
                }
            }
            catch (Exception ex)
            {
                _rct = null;
                //Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return _rct;

        }


    }
}
