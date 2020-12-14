using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using IRagazzi.Clases;
using IRagazzi.Servicios;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace IRagazzi
{
    public partial class Inicio : TabbedPage
    {

        List<Productos> lista = new List<Productos>();
       public List<Compra> listaCompraInicio = new List<Compra>();
        public static List<influencer> listaMostar;
        public influencer selecionado = new influencer();
        private ICommand _deleteCommand;
        public Inicio()
        {
            Xamarin.Forms.DataGrid.DataGridComponent.Init();
            InitializeComponent();

            buscarInfluencer();

            cargargrilla();
            MessagingCenter.Subscribe<Inicio>(this, "BtnEmisor", (sender) => {

               
                buscarInfluencer();
            });

            MessagingCenter.Subscribe<Inicio>(this, "BtnCargarProductos", (sender) => {

                cargargrilla();

            });


            cargillatodoslospedidosAsync();


            lstinfluencer.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    var producto = (influencer)e.SelectedItem;
                    string selectedItem = e.SelectedItem.ToString();

                    // clear selected item
                    lstinfluencer.SelectedItem = null;

                    recargarInfluencer(producto);
                }
            };
        }

        private async System.Threading.Tasks.Task cargillatodoslospedidosAsync()
        {
            Get_Compra getCompras_ID = new Get_Compra();

            var listaCompra = await getCompras_ID.listaCompra();

            var mostar = new List<Compra>();

            foreach (var item in listaCompra)
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

                nuevo.Telefono = item.Telefono;

                mostar.Add(nuevo);
            }

            CollectionView1.ItemsSource = mostar.OrderByDescending(d=>d.Fecha);
        }

        public Inicio(int a)
        {
            Xamarin.Forms.DataGrid.DataGridComponent.Init();
            InitializeComponent();

            buscarInfluencer();

            cargargrilla();

            buscarCompra();
            
        }



       

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(item =>
        {
            var model = item as Compra;
            //model?.ForceCloseCommand?.Execute(false); // OneWayToSource binding. So we call Close method without animation
            
        }));

        private async void buscarCompra()
        {
            await PopupNavigation.PopAsync();
            //listaCompraInicio = new List<Compra>();
            //Get_Compra compra = new Get_Compra();
            //listaCompraInicio = await compra.listaCompra();
            //listaCompraInicio = listaCompraInicio.Where(d => d.Entregado == 0).ToList();
            //CollectionView.ItemsSource = listaCompraInicio;
        }

        private void cargargrilla()
        {
            lista = new List<Productos>();
            foreach (var item in App.listProductos)
            {
                Productos news = new Productos();

                news.Id = item.Id;
                news.Nombre = item.Nombre.ToUpper();
                news.Precio = "Precio: $" + item.Precio +"  " + item.Unidad;
                news.PrecioCuenta = int.Parse(item.Precio);
                news.Foto = item.Foto.Replace("http", "https");
                news.Fotouri = new Uri(item.Foto.Replace("http", "https"));
                news.Id_Categoria = item.Id_Categoria;
                switch (news.Id_Categoria)
                {
                    case 1:
                        news.Id_categoria= "FRUTAS";
                        break;
                    case 2:
                        news.Id_categoria = "VERDURA";
                        break;
                    case 3:
                        news.Id_categoria = "CARNES";
                        break;
                    
                }
                news.Descripcion = item.Descripcion;
                news.Activo = item.Activo;
                lista.Add(news);
            }

            if (lista != null)
            {
                lstActivo.ItemsSource = new List<influencer>();
                lstInactivos.ItemsSource = new List<influencer>();
                lstActivo.ItemsSource = lista.Where(d=>d.Activo==1);
                lstInactivos.ItemsSource = lista.Where(d => d.Activo == 0);
            }
        }

        private async void buscarInfluencer()
        {
            Get_influencer get_Influencer = new Get_influencer();

            listaMostar = await get_Influencer.listaInfluencer();

            lstinfluencer.ItemsSource = listaMostar.Where(d=>d.Activo==1);


        }

       

        private async void lstInactivos_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (sender != null)
            {
                var producto = (Productos)e.SelectedItem;
                var ans = await DisplayAlert(producto.Nombre, "Quiero volver a activar el producto???", "SI", "NO");
                if (ans == true)
                {
                    producto.Activo = 1;
                    Update_Producto update = new Update_Producto();
                    await update.UpdateProductos(producto);
                    GetProductos getProductos = new GetProductos();
                    App.listProductos = new List<Productos>(); 
                    App.listProductos=  await getProductos.listaPersonas();
                    cargargrilla();
                }
                else
                {
                    //false conditon
                }
            }
        }

        private async void lstActivos_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (sender != null)
            {
                var producto = (Productos)e.SelectedItem;
                var ans = await DisplayAlert(producto.Nombre, "Quiero desactivar el producto???", "SI", "NO");
                if (ans == true)
                {
                    producto.Activo = 0;
                    Update_Producto update = new Update_Producto();
                    await update.UpdateProductos(producto);
                    GetProductos getProductos = new GetProductos();
                    App.listProductos = new List<Productos>();
                    App.listProductos = await getProductos.listaPersonas();
                    cargargrilla();
                }
                else
                {
                    //false conditon
                }
            }
        }

      private async void  Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (sender != null)
            {
                
                await PopupNavigation.Instance.PushAsync(new popUpInfluencer());
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CargaProducto());
        }

        

        private async void recargarInfluencer(influencer producto)
        {
            
                var ans = await DisplayAlert(producto.Nombre, "Quiero desactivar el influencer???", "SI", "NO");
                if (ans == true)
                {
                    var mostrar = listaMostar.Where(d => d.Activo == 1 && d.id != producto.id).ToList();
                    lstinfluencer.ItemsSource = mostrar;
                    producto.Activo = 0;
                    Update_Influencer update = new Update_Influencer();
                    await update.UpdateProductos(producto);
                }
                else
                {
                    //false conditon
                }
            

            
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reporte());
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            lstActivo.IsVisible = false;
            lstInactivos.IsVisible = true;
            btnActivo.IsVisible = true;
            btnInactivo.IsVisible = false;
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            lstActivo.IsVisible = true;
            lstInactivos.IsVisible = false;

            btnActivo.IsVisible = false;
            btnInactivo.IsVisible = true;
        }

        private async void lstActivo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                var producto = (Productos)e.CurrentSelection[0];
                var ans = await DisplayAlert(producto.Nombre, "Quiero desactivar el producto???", "SI", "NO");
                if (ans == true)
                {
                    producto.Activo = 0;
                    Update_Producto update = new Update_Producto();
                    await update.UpdateProductos(producto);
                    GetProductos getProductos = new GetProductos();
                    App.listProductos = new List<Productos>();
                    App.listProductos = await getProductos.listaPersonas();
                    cargargrilla();
                }
                else
                {
                    //false conditon
                }
            }
        }

        private void StackLayout_MeasureInvalidated(object sender, EventArgs e)
        {

        }

        private async void lstInactivos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                var producto = (Productos)e.CurrentSelection[0];
                var ans = await DisplayAlert(producto.Nombre, "Quiero volver a activar el producto???", "SI", "NO");
                if (ans == true)
                {
                    producto.Activo = 1;
                    Update_Producto update = new Update_Producto();
                    await update.UpdateProductos(producto);
                    GetProductos getProductos = new GetProductos();
                    App.listProductos = new List<Productos>();
                    App.listProductos = await getProductos.listaPersonas();
                    cargargrilla();
                }
                else
                {
                    //false conditon
                }
            }
        }
    }
}
