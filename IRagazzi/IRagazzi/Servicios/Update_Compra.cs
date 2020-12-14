﻿using IRagazzi.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRagazzi.Servicios
{
   public class Update_Compra
    {
        const String URL = "https://todoreparacion.com/IRagazzi/Update_Compra.php";
        public async void UpdateCompra(Compra nuevo)
        {


            var client = new HttpClient();

            var model = nuevo;

            var json = JsonConvert.SerializeObject(new
            {
                Id = nuevo.id,
                Entregado = 1
            });

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(URL, content);


            var data = await response.Content.ReadAsStringAsync();

            
        }
    }
}
