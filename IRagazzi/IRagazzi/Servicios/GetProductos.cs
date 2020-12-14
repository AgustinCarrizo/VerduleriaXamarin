using IRagazzi.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRagazzi.Servicios
{
   public class GetProductos
    {

        const String URL = "https://todoreparacion.com/IRagazzi/Get_Productos.php";


        private HttpClient getCliente()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Connection", "close");

            return client;

        }

        public async Task<List<Productos>> listaPersonas()
        {
            HttpClient client = getCliente();

            var res = await client.GetAsync(URL);

            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Productos>>(content);
            }
            return null;
        }

    }
}
