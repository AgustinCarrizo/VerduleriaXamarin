using IRagazzi.Clases;
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
    public partial class Pedidos : ContentPage
    {
        public Pedidos()
        {
            InitializeComponent();
            buscarpedidos(0);
            crearsqllite();

            
        }

        public Pedidos(int pedido)
        {
            
            InitializeComponent();
           
            var existingPages = Navigation.NavigationStack.ToList();
            buscarpedidos(0);
            crearsqllite();
            mensaje();
        }

        public Pedidos(int pedido , string n)
        {

            InitializeComponent();

            buscarpedidos(pedido);
            crearsqllite();
            
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void mensaje()
        {
            await DisplayAlert("Informacion", "Su pedido se cargo con exito muchas gracias por usar nuestro servicio", "OK");
        }

        private async void buscarpedidos(int entrega)
        {
            int id = 0;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {

                 id = conn.Table<Usuario>().FirstOrDefault().id_Usuario;
            }

            if (id!=0)
            {
                GetCompras_ID getCompras_ID = new GetCompras_ID();

                var listaCompra = await getCompras_ID.listaCompra(id);

                var mostar = new List<Compra>();

                foreach (var item in listaCompra.Where(d=>d.Entregado == entrega))
                {
                    Compra nuevo = new Compra();

                    nuevo.id = item.id;

                    nuevo.id_Registros = item.id_Registros;


                    nuevo.Productos = item.Productos.Substring(0, item.Productos.IndexOf("\n")) + "....."; ;


                    nuevo.PrecioTotal = item.PrecioTotal;


                    nuevo.Pago = item.Pago;

                    nuevo.Direccion = item.Direccion;

                    nuevo.Entregado = item.Entregado;


                    nuevo.Fecha = item.Fecha;

                    nuevo.FechaString = item.Fecha.ToString().Substring(0, item.Fecha.ToString().IndexOf(" "));

                    nuevo.Codigo = item.Codigo;

                    mostar.Add(nuevo);
                  }

                CollectionView.ItemsSource = mostar;
            }

        }
        private void crearsqllite()
        {
            var cantidad = 0;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {

                var listProductos = conn.Table<Productosdb>().ToList();
                cantidad = listProductos.Count();
                lblCantidadCarrizto.Text = cantidad.ToString();
            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarroCompra());
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var producto = (Compra)e.CurrentSelection[0];
        }
    }
}