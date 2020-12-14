using System;
using System.Collections.Generic;
using System.Text;

namespace IRagazzi.Clases
{
    public class Productos
    {

        bool success4;
        int id;
        string nombre;
        string precio;
        int precioCuenta;
        string foto;
        Uri fotouri;
        int id_Categoria;
        string id_categoria;
        string descripcion;
        string descripcionCorta;
        string unidad;
        int activo;

        public bool Success4 { get => success4; set => success4 = value; }
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Precio { get => precio; set => precio = value; }
        public string Foto { get => foto; set => foto = value; }
        public int Id_Categoria { get => id_Categoria; set => id_Categoria = value; }
        public string Id_categoria { get => id_categoria; set => id_categoria = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int PrecioCuenta { get => precioCuenta; set => precioCuenta = value; }
        public Uri Fotouri { get => fotouri; set => fotouri = value; }
        public int Activo { get => activo; set => activo = value; }
        public string Unidad { get => unidad; set => unidad = value; }
        public string DescripcionCorta { get => descripcionCorta; set => descripcionCorta = value; }
    }
}
