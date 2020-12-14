
using System;
using System.Collections.Generic;
using System.Text;

    namespace IRagazzi.Clases
    {
        public partial class Compra
        {
            public int id { get; set; }

            public int id_Registros { get; set; }


            public string Productos { get; set; }


            public string PrecioTotal { get; set; }


            public string Pago { get; set; }

            public string Direccion { get; set; }

            public int Entregado { get; set; }


            public DateTime Fecha { get; set; }

            public int? Codigo { get; set; }

            public string Telefono { get; set; }

            public string FechaString { get; set; }
        }
    }