using System;
using System.Collections.Generic;
using FormsControls.Base;
using IRagazzi.Clases;
using IRagazzi.Servicios;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IRagazzi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CargaProducto : ContentPage, IAnimationPage
    {
        Plugin.Media.Abstractions.MediaFile file ;
        string Base64Imagen = "";
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
        public CargaProducto()
        {
            try
            {

              
                InitializeComponent();
                
                Todo.IsEnabled = true;
                List<string> tipos = new List<string>();
                List<string> unidad = new List<string>();

                tipos.Add("FRUTAS");
                tipos.Add("VERDURA");
                tipos.Add("CARNES");

                unidad.Add("KG");
                unidad.Add("C/U");
                unidad.Add("ATADO");

                cboTipoProducto.ItemsSource = tipos;
                cboUnidad.ItemsSource = unidad;

                cboTipoProducto.SelectedIndex = 0;
                cboUnidad.SelectedIndex = 0;

                }
            catch (Exception ex)
            {
                var a = ex.ToString();
            }
        }

        private async void btnCargarImagen(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No soporta", ":( No tiene los permisos necesarios ", "OK");
            }
             file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
             {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight                
                
                });
            if (file == null)
            {
                return;
            }
            MostrarImagen.Source = ImageSource.FromStream(() =>
              {
                  var stream = file.GetStream();
                  var stam = file.GetStream();
                  byte[] filebyte = new byte[stream.Length];
                  stream.Read(filebyte, 0, (int)stream.Length);
                  Base64Imagen = Convert.ToBase64String(filebyte);
                  file.Dispose();
                  return stam;
              });
            
            }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            aicargando.IsRunning = true;
            btncargar.IsVisible = false;

            if (txtNombre.Text != null && txtPrecio.Text != null && !Base64Imagen.Equals("") && txtDescripcion.Text!=null)
            {
                
                var respuesta = false;
                Productos1 productos = new Productos1();

                productos.Nombre = txtNombre.Text;

                productos.Precio = txtPrecio.Text;


                productos.Unidad = cboUnidad.SelectedItem.ToString();


                productos.Foto = Base64Imagen;

                productos.id_Categorias = cboTipoProducto.SelectedIndex+1;


                productos.Descripcion = txtDescripcion.Text;

                productos.Activo = 1;

                productos.ActvioCarrito = 0;

                Insert_Producto insert = new Insert_Producto();

                respuesta = await insert.ActivarProductos(productos);
            
            
                if (respuesta==true)
                {
                    btncargar.IsVisible = true;
                    aicargando.IsRunning = false;
                    GetProductos getProductos = new GetProductos();
                    App.listProductos = await getProductos.listaPersonas();
                    await Navigation.PopAsync();
                    MessagingCenter.Send((new Inicio()), "BtnCargarProductos");
                }

            }
            else
            {
                await DisplayAlert("Alerta", "Complete todo los campos", "OK");
                btncargar.IsVisible = true;
                aicargando.IsRunning = false;
            }

        }
    }
}
