using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRagazzi.Clases
{
    [Table("Usuario")]
    class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int id_Usuario { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Contrasenia { get; set; }

        public string Calle { get; set; }

        public string Altura { get; set; }

        public string Telefono { get; set; }
    }
}
