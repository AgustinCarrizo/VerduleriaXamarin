using IRagazzi.Clases;
using IRagazzi.Servicios;
using Rg.Plugins.Popup.Pages;
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
    public partial class popUpCompra : ContentPage
    {
        public  List<influencer> listaCodigos;
        influencer resultadoCodigo = new influencer();
        string precioTotal="";
        string Pago = "";
        string formaPago = "";

        public popUpCompra(string total , string formapago)
        {
            InitializeComponent();
            precioTotal = total;
            Pago = formapago;
            lblPrecio.Text =   total;
            lblPrecio8.Text = total;
            // lblForma.Text = "Foma De Pago: " + formapago;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {

                var completado = conn.Table<Usuario>().FirstOrDefault();
            

                txtDireccion.Text = completado.Calle + completado.Altura ;
                txtTelefono.Text = completado.Telefono;
            }
            buscarCodigos();
        }

        private async void buscarCodigos()
        {
            Get_influencer get_Influencer = new Get_influencer();

            listaCodigos = await get_Influencer.listaInfluencer();

            listaCodigos = listaCodigos.Where(d => d.Activo == 1).ToList();

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var codigo = txtCodigo.Text.Trim();
            if (!codigo.Equals(""))
            {
                resultadoCodigo = listaCodigos.Where(d => d.Codigo.ToUpper().Equals(codigo.ToUpper())).FirstOrDefault();
                if (resultadoCodigo != null)
                {
                     var descuento = resultadoCodigo.Descuento;

                    divDescuento.IsVisible = true;

                    var suma = double.Parse(precioTotal);
                    suma = suma - ( descuento/suma) * 100;

                    lblSuma.Text = suma.ToString();
                    
                }
                else
                {
                    await DisplayAlert("Alert", "El Codigo no existe", "OK");
                    return;
                }
            }
            else
            {
                await DisplayAlert("Alert", "Cargue un Codigo", "OK");
                return;
            }
            
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (!txtDireccion.Text.Trim().Equals("") && !formaPago.Equals("") && !txtTelefono.Text.Trim().Equals("") &&
                txtDireccion.Text !=null && txtTelefono.Text !=null)
            {
                int Usuario = 0;
                List<Productosdb> productosdb = new List<Productosdb>();
                Compra nuevaCompra = new Compra();
                List<Registros> resultado = new List<Registros>();

                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {

                    productosdb = conn.Table<Productosdb>().ToList();
                    Usuario = conn.Table<Usuario>().FirstOrDefault().id_Usuario;
                }

                if (Usuario==0)
                {
                    Registros nuevoUsuario = new Registros();
                    nuevoUsuario.Usuario = txtDireccion.Text;


                    insert_Usuario usuario = new insert_Usuario();
                     //resultado = await usuario.insertUsuario(nuevoUsuario);

                    nuevaCompra.id_Registros = resultado[0].id;

                }
                else
                {
                    nuevaCompra.id_Registros = Usuario;
                }

                nuevaCompra.Direccion = txtDireccion.Text;



                foreach (var item in productosdb)
                {
                    nuevaCompra.Productos = nuevaCompra.Productos + item.cantidad+ "x"+item.nombre+ "\n";
                }

                

                if (!lblSuma.Text.Equals("0"))
                {
                    nuevaCompra.PrecioTotal = lblSuma.Text;
                }
                else
                {
                    nuevaCompra.PrecioTotal = precioTotal;
                }
                nuevaCompra.Pago = formaPago;
                nuevaCompra.Entregado = 0;
                var fecha =  DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
                var conventir = DateTime.Parse(fecha);
                nuevaCompra.Fecha = conventir ;

                if (resultado!=null)
                {
                    nuevaCompra.Codigo = resultadoCodigo.id;
                }

                nuevaCompra.Telefono = txtTelefono.Text;

                Insert_Compra compra = new Insert_Compra();
                 var resultadoCompra = await compra.nuevoCompra(nuevaCompra);

                if (resultadoCompra==true)
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {

                        conn.DeleteAll<Productosdb>();
                        conn.DeleteAll<Usuario>();

                        Usuario nuevo = new Usuario();

                        nuevo.id_Usuario =  nuevaCompra.id_Registros;

                        conn.Insert(nuevo);
                    }

                    await Navigation.PushAsync(new Pedidos(1));
                    Navigation.RemovePage(this);
                }

                

            }
            else
            {
                await DisplayAlert("Informacion", "Por favor complete todos los campos", "OK");
            }

            
            


        }
        


        private async void chkEfectivo_BindingContextChanged(object sender, EventArgs e)
        {
            
            if (chkEfectivo.IsChecked == true )
            {
                chkEfectivo.IsChecked = true;
                chktarjeta.IsChecked = false;
                formaPago = "EFECTIVO";
            }
            
                if ( chktarjeta.IsChecked == true)
                {
                    chkEfectivo.IsChecked = false;
                    chktarjeta.IsChecked = true;
                    formaPago = "TARJETA";

                   await DisplayAlert("Informacion", "El pago de tarjeta tambien se hace cuando llega el pidio a su casa", "OK");
                }
               
            
            
        }

        
    }
}