using IRagazzi.Clases;
using IRagazzi.Servicios;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using FormsControls.Base;

namespace IRagazzi
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : ContentPage
    {
        List<Productos> lista = new List<Productos>();
        int cantidad = 0;
        public MainPage()
        {

            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);
            buscarProductos();

            MessagingCenter.Subscribe<MainPage>(this, "CarroSinProudtos_Clicked", async (sender) => {

                await DisplayAlert("Alerta", "Seleccione un producto realizar la compra", "OK");
                buscarProductos();
                lblCantidadCarrizto.Text = "0";


            });


        }
        public MainPage(int? cantidades)
        {

            InitializeComponent();
            if (cantidades != null)
            {
                cantidad = int.Parse(cantidades.ToString());
                NavigationPage.SetHasBackButton(this, false);
                buscarProductos();
                lblCantidadCarrizto.Text = cantidad.ToString();
            }


        }

        public async void buscarProductos()
        {
            if (App.listProductos.Count > 0)
            {


                foreach (var item in App.listProductos)
                {
                    Productos news = new Productos();

                    news.Id = item.Id;
                    news.Nombre = item.Nombre.ToUpper();
                    news.Precio = "Precio: $" + item.Precio + " " + item.Unidad;
                    news.PrecioCuenta = int.Parse(item.Precio);
                    news.Foto = item.Foto.Replace("http", "https");
                    news.Fotouri = new Uri(item.Foto.Replace("http", "https"));
                    news.Id_Categoria = item.Id_Categoria;
                    news.Descripcion = item.Descripcion;
                    if (!String.IsNullOrWhiteSpace(item.Descripcion) && item.Descripcion.Length >= 50)
                        news.DescripcionCorta = item.Descripcion.Substring(0, 50)+".....";
                    else
                        news.DescripcionCorta = news.Descripcion;
                    lista.Add(news);
                }

                if (lista != null)
                {
                    lstUsuario.ItemsSource = lista.Where(d=>d.Id_Categoria==1).ToList();
                }

            }
            else
            {
                try
                {

                    GetProductos manager = new GetProductos();
                    App.listProductos = await manager.listaPersonas();
                    buscarProductos();

                }
                catch (Exception ex)
                {

                    var d = ex.Message;


                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lstUsuario_ItemSelected(null, new SelectedItemChangedEventArgs(null));

            crearsqllite();
        }



        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            lstUsuario_ItemSelected(null, new SelectedItemChangedEventArgs(null));
            crearsqllite();
        }

        private void crearsqllite()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Usuario>();
                conn.CreateTable<Productosdb>();
                var listProductos = conn.Table<Productosdb>().ToList();
                cantidad = listProductos.Count();
                lblCantidadCarrizto.Text = cantidad.ToString();
            }
            
        }

        private void btnFrutas(object sender, EventArgs e)
        {

            lstUsuario.ItemsSource = lista.Where(d => d.Id_Categoria == 1).ToList();
        }

        private void btnVerduras(object sender, EventArgs e)
        {

            lstUsuario.ItemsSource = lista.Where(d => d.Id_Categoria == 2).ToList();
        }

        private void btnCarnes(object sender, EventArgs e)
        {

            lstUsuario.ItemsSource = lista.Where(d => d.Id_Categoria == 3).ToList();
        }

        private async void lstUsuario_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (sender != null)
            {
                var producto = (Productos)e.SelectedItem;

                await PopupNavigation.Instance.PushAsync(new popUpProductos(producto,0));
            }



        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (lblCantidadCarrizto.Text.Equals("0"))
            {
                await DisplayAlert("Alerta", "Agrege un producto para entrar al carrito", "OK");
            }
            else
            {
                await Navigation.PushAsync(new CarroCompra());
            }
            
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pedidos());
        }

        private async void btnLogin(object sender, EventArgs e)
        {
            
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                 var usuario = conn.Table<Usuario>().FirstOrDefault();
            


                Get_Login login = new Get_Login();

                var respuesta = await login.logiarse(usuario.Contrasenia, usuario.Email);

                if (respuesta != null)
                {
                    await Navigation.PushAsync(new Inicio());
                    
                }
                else
                {
                    await DisplayAlert("Alerta", "Usuario o Contraseñia incorrecta", "OK");
                    
                }
            

           
            }
        }

        

        private async void lstUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                var producto = (Productos)e.CurrentSelection[0];

                await PopupNavigation.Instance.PushAsync(new popUpProductos(producto, 0));
            }
        }
    }
}
