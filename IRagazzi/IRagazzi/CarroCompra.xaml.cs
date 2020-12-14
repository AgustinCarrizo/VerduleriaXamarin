using FormsControls.Base;
using IRagazzi.Clases;
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
    public partial class CarroCompra : ContentPage, IAnimationPage
    {
        List<Productos> lista = new List<Productos>();
        List<CarritoCompraFoto> listaCompra = new List<CarritoCompraFoto>();
        int cantidadtotal = 0;
       
        public IPageAnimation PageAnimation { get; } = new PushPageAnimation
        {
            Duration = AnimationDuration.Medium,
            Subtype = AnimationSubtype.FromBottom
        };

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public CarroCompra()
        {
            InitializeComponent();

            buscarPedidoCompra();

           // cargarListDescuento();

            //crearsqllite();

            MessagingCenter.Subscribe<CarroCompra>(this, "BtnEmisor_Clicked", (sender) => {

               
                buscarPedidoCompra();

                //cargarListDescuento();

               // lstUsuario.SelectedItem = 0;
            });


        }

        private void cargarListDescuento()
        {


            foreach (var item in App.listProductos)
            {
                Productos news = new Productos();

                news.Id = item.Id;
                news.Nombre = item.Nombre;
                news.Precio = "Precio: $" + item.Precio;
                news.PrecioCuenta = int.Parse(item.Precio);
                news.Foto = item.Foto.Replace("http", "https");
                news.Fotouri = new Uri(item.Foto.Replace("http", "https"));
                news.Id_Categoria = item.Id_Categoria;
                news.Descripcion = item.Descripcion;
                lista.Add(news);
            }

            if (lista != null)
            {
               // lstUsuario.ItemsSource = lista;
            }



        }

        

        private void buscarPedidoCompra()
        {
            listaCompra = new List<CarritoCompraFoto>();
            List<Productosdb> productosdb = new List<Productosdb>();

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {

                productosdb = conn.Table<Productosdb>().ToList();
            }

            if (productosdb.Count>0)
            {

                var todoslosProductos = App.listProductos;

                CarritoCompraFoto nuevo = new CarritoCompraFoto();

                foreach (var item1 in productosdb)
                {
                    nuevo = new CarritoCompraFoto();
                   var foto = todoslosProductos.Where(d =>  d.Nombre.ToUpper().Equals(item1.nombre.ToUpper())).First().Foto;
                    nuevo.id = item1.id;
                    nuevo.nombre = item1.nombre;
                    nuevo.precionUnitarioString = item1.precionUnitarioString;
                    nuevo.precioUnitario = item1.precioUnitario;
                    nuevo.precioTotal = item1.precioTotal;
                    nuevo.cantidad = item1.cantidad;
                    nuevo.Foto = foto.Replace("http", "https"); ;

                    listaCompra.Add(nuevo);
                }
            
                lstCarrito.ItemsSource = listaCompra;
            
                foreach (var item in productosdb)
                {
                    cantidadtotal = cantidadtotal + item.precioTotal;
                }
                btnComprar.Text= "Comprar "+"($"+ cantidadtotal.ToString()+")" ;
            }
            else
            {
                MessagingCenter.Send((new MainPage()), "CarroSinProudtos_Clicked");
                Navigation.RemovePage(this);
            }
        }

       
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            //await Navigation.PushAsync(new MainPage(), false);

        }
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
             await Navigation.PushAsync(new popUpCompra(cantidadtotal.ToString(), "soy"));
            Navigation.RemovePage(this);



        }

        

        private void crearsqllite()
        {
            var cantidad = 0;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                
                var listProductos = conn.Table<Productosdb>().ToList();
                cantidad = listProductos.Count();
               // lblCantidadCarrizto.Text = cantidad.ToString();
            }

        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Pedidos());
        }

        private async void lstUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (sender != null)
            {
                var producto = (CarritoCompraFoto)e.CurrentSelection[0];

                // await PopupNavigation.Instance.PushAsync(new popUpProductos(producto, 0));
                //string ans = await DisplayActionSheet("Quiero desea realizar con el producto "+producto.nombre+"???", "Cancel", null, "Editar", "Eliminar");
               
              
                
                //if (ans == "Editar")
                //{
                    await PopupNavigation.Instance.PushAsync(new popUpProductos(producto, 1));
                //}
                //else
                //{
                //    if (ans == "Eliminar")
                //    {

                    
                //        using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                //        {

                //            var listProductos = conn.Table<Productosdb>().ToList();
                //            conn.DeleteAll<Productosdb>();
                //            listProductos = listProductos.Where(d => d.id != producto.id).ToList();
                //            Productosdb nuevo = new Productosdb();

                //            foreach (var item1 in listProductos)
                //            {
                //                nuevo = new Productosdb();
                //                 nuevo.id = item1.id;
                //                nuevo.nombre = item1.nombre;
                //                nuevo.precionUnitarioString = item1.precionUnitarioString;
                //                nuevo.precioUnitario = item1.precioUnitario;
                //                nuevo.precioTotal = item1.precioTotal;
                //                nuevo.cantidad = item1.cantidad;
                            

                //                conn.Insert(nuevo);
                //            }

                //            buscarPedidoCompra();
                //        }
                //    }
                //    else
                //    {

                //   }


                //}


            }
            
        }
    }
}