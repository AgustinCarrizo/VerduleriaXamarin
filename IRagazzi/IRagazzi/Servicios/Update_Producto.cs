using IRagazzi.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRagazzi.Servicios
{
    class Update_Producto
    {
        const String URL = "https://todoreparacion.com/IRagazzi/Update_Productos.php";
        public async Task<bool> UpdateProductos(Productos nuevo)
        {


            var client = new HttpClient();

            var model = nuevo;

            var json = JsonConvert.SerializeObject(new { 
            Id=nuevo.Id,
            Activo=nuevo.Activo
            });

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(URL, content);


            var data = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }
    }
}
