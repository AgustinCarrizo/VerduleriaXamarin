using IRagazzi.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRagazzi.Servicios
{
    class Get_influencer
    {

        const String URL = "https://todoreparacion.com/IRagazzi/Get_Influencer.php";


        private HttpClient getCliente()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Connection", "close");

            return client;

        }

        public async Task<List<influencer>> listaInfluencer()
        {
            
            HttpClient client = getCliente();

            var res = await client.GetAsync(URL);
            string content = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                
               
                return JsonConvert.DeserializeObject<List<influencer>>(content);
            }
            return JsonConvert.DeserializeObject<List<influencer>>(content);
        }

    }
}
