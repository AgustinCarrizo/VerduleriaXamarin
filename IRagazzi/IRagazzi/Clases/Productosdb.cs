using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace IRagazzi.Clases
{
    [Table("Productos")]
    class Productosdb
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        public string nombre { get; set; }
        public string precionUnitarioString { get; set; }
        public int precioUnitario { get; set; }
        public int precioTotal { get; set; }
        public int cantidad { get; set; }


    }
}
