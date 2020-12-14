using IRagazzi.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRagazzi.Servicios
{
    class Insert_influencer
    {

        const String URL = "https://todoreparacion.com/IRagazzi/insert_influencer.php";
        public async Task<bool> nuevoInfluencer(influencer nuevo)
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
