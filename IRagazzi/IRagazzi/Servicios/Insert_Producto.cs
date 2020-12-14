using IRagazzi.Clases;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRagazzi.Servicios
{
   public class Insert_Producto
    {

        const String URL = "https://todoreparacion.com/IRagazzi/Productos.php";
        public async Task<bool> ActivarProductos(Productos1 nuevo)
            {


            var client = new HttpClient();

            var model = nuevo;

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(URL, content);

            
            var data = await response.Content.ReadAsStringAsync();

             return response.IsSuccessStatusCode;
           }

    }

}
