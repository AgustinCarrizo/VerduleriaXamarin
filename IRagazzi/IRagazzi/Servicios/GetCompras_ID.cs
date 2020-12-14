using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRagazzi.Clases
{
    class GetCompras_ID
    {
        const String URL = "https://todoreparacion.com/IRagazzi/Get_ProductosID.php";

        public async Task<List<Compra>> listaCompra(int nuevo)
        {
            var client = new HttpClient();

            var model = nuevo;

            var json = JsonConvert.SerializeObject(new
            {
                id_Registros = nuevo 
            });

            HttpContent content1 = new StringContent(json);

            content1.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var res = await client.PostAsync(URL, content1);

           

            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Compra>>(content);
            }
            return null;
        }
    }
}
