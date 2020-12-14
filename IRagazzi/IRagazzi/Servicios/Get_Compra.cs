using IRagazzi.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRagazzi.Servicios
{
    class Get_Compra
    {
        const String URL = "https://todoreparacion.com/IRagazzi/Get_Compra.php";


        private HttpClient getCliente()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Connection", "close");

            return client;

        }

        public async Task<List<Compra>> listaCompra()
        {
            HttpClient client = getCliente();

            var res = await client.GetAsync(URL);

            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Compra>>(content);
            }
            return null;
        }
    }
}
