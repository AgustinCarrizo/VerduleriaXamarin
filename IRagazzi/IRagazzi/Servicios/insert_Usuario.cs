using IRagazzi.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRagazzi.Servicios
{
    class insert_Usuario
    {
        const String URL = "https://todoreparacion.com/IRagazzi/insert_UsuarioNuevo.php";
        public async Task<List<UsuarioCompleto>> insertUsuario(UsuarioCompleto nuevo)
        {


            var client = new HttpClient();

            var model = nuevo;

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var res = await client.PostAsync(URL, content);

            if (res.IsSuccessStatusCode)
            {
                string content1 = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UsuarioCompleto>>(content1);
            }
            return null;

        }
    }
}
