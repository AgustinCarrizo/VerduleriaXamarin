using IRagazzi.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRagazzi.Servicios
{
    class Get_Login
    {
        const String URL = "https://todoreparacion.com/IRagazzi/Get_Login.php";
        const String URL1 = "https://todoreparacion.com/IRagazzi/Get_LoginPrincipal.php";
        public async Task<List<clsLogin>> logiarse(string usuario , string contrasenia)
        {
            var client = new HttpClient();

            //var model = nuevo;

            var json = JsonConvert.SerializeObject(new
            {
                Email = contrasenia,
                Contrasenia = usuario
            });

            HttpContent content1 = new StringContent(json);

            content1.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var res = await client.PostAsync(URL, content1);



            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<clsLogin>>(content);
            }
            return null;
        }

        public async Task<List<UsuarioCompleto>> logiarsePrincipal(string usuario, string contrasenia)
        {
            var client = new HttpClient();

            //var model = nuevo;

            var json = JsonConvert.SerializeObject(new
            {
                Email = contrasenia,
                Contrasenia = usuario
            });

            HttpContent content1 = new StringContent(json);

            content1.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var res = await client.PostAsync(URL1, content1);



            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UsuarioCompleto>>(content);
            }
            return null;
        }
    }
}
