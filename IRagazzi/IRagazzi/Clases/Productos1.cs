
using System;
using System.Collections.Generic;
using System.Text;

    namespace IRagazzi.Clases
    {
        public partial class Productos1
        {

            public int id_Productos { get; set; }


            public string Nombre { get; set; }

            public string Precio { get; set; }


            public string Unidad { get; set; }


            public string Foto { get; set; }

            public int id_Categorias { get; set; }


            public string Descripcion { get; set; }

            public int Activo { get; set; }

            public int ActvioCarrito { get; set; }
        }
    }

