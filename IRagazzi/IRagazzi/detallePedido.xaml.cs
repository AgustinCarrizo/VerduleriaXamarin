using IRagazzi.Clases;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IRagazzi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class detallePedido : PopupPage
    {
        int cantidad = 0;
        int precioUnidad;
        int suma = 0;
        int carritos = 0; 
        public detallePedido( string carrito)
        {
            InitializeComponent();
            lblDescripcion.Text = carrito;
        }

        public detallePedido(CarritoCompraFoto productos, int carrito)
        {
            //InitializeComponent();
            //var todoslosProductos = App.listProductos;
            //foto.Source = productos.Foto;
            //nombre.Text =  todoslosProductos.Where(d => d.Nombre.ToUpper().Equals(productos.nombre.ToUpper())).First().Nombre;
            //precio.Text = "Precio: $" + todoslosProductos.Where(d => d.Nombre.ToUpper().Equals(productos.nombre.ToUpper())).First().Precio+ todoslosProductos.Where(d => d.Nombre.ToUpper().Equals(productos.nombre.ToUpper())).First().Unidad;
            //precioUnidad = int.Parse(todoslosProductos.Where(d => d.Nombre.ToUpper().Equals(productos.nombre.ToUpper())).First().Precio);
            //lblDescripcion.Text = todoslosProductos.Where(d => d.Nombre.ToUpper().Equals(productos.nombre.ToUpper())).First().Descripcion;
            //lblSuma.Text = productos.precioTotal.ToString();
            //lblCantidad.Text = productos.cantidad.ToString();
            //carritos = carrito;

        }

        private void btnMenos(object sender, EventArgs e)
        {
            //cantidad = int.Parse(lblCantidad.Text);
            
            //if (cantidad>0)
            //{
            //    cantidad = cantidad - 1;
            //    lblCantidad.Text = cantidad.ToString();
            //    suma = precioUnidad * cantidad;
            //    lblSuma.Text = suma.ToString();
            //}
            

        }

        private void btnMas(object sender, EventArgs e)
        {
            //cantidad = int.Parse(lblCantidad.Text);
            
            //    cantidad = cantidad + 1;
            //    lblCantidad.Text = cantidad.ToString();
            //    suma = precioUnidad * cantidad;
            //    lblSuma.Text = suma.ToString();
            
        }

        private async void btnConfirmar(object sender, EventArgs e)
        {
            //int listProductos = 0;
            //Productosdb newProducto = new Productosdb();
            //newProducto.nombre = nombre.Text;
            //newProducto.precionUnitarioString = precio.Text;
            //newProducto.precioUnitario = precioUnidad;
            //newProducto.precioTotal = suma;
            //newProducto.cantidad = cantidad;

            

            //if (carritos==1)
            //{
            //    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            //    {
            //        //conn.CreateTable<Productosdb>();
                    
            //        //conn.DropTable<Productosdb>();
            //        var resulltado = conn.Table<Productosdb>().ToList();
            //        conn.DeleteAll<Productosdb>();
            //        resulltado = resulltado.Where(d => !d.nombre.ToUpper().Equals(newProducto.nombre.ToUpper())).ToList();

            //        foreach (var item in resulltado)
            //        {
            //            Productosdb newProducto1 = new Productosdb();
            //            newProducto1.nombre = item.nombre;
            //            newProducto1.precionUnitarioString = item.precionUnitarioString;
            //            newProducto1.precioUnitario = item.precioUnitario;
            //            newProducto1.precioTotal = item.precioTotal;
            //            newProducto1.cantidad = item.cantidad;
            //            conn.Insert(newProducto1);
            //        }
            //        conn.Insert(newProducto);
            //    }
                


                
            //    MessagingCenter.Send((new CarroCompra()), "BtnEmisor_Clicked");
              
            //    await PopupNavigation.PopAsync();
            //}
            //else
            //{
            //    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            //    {
            //        //conn.CreateTable<Productosdb>();
            //        conn.Insert(newProducto);
            //        //conn.DropTable<Productosdb>();
            //        listProductos = conn.Table<Productosdb>().ToList().Count();
            //    }
            //    await Navigation.PushAsync(new MainPage(listProductos), false);
            //    await PopupNavigation.PopAsync();
            //}
            
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            //await Navigation.PushAsync(new MainPage(), false);
           
        }
    }
}