using FormsControls.Base;
using IRagazzi.Clases;
using IRagazzi.Servicios;
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
    public partial class Login : ContentPage, IAnimationPage
    {

        public IPageAnimation PageAnimation { get; } = new PushPageAnimation
        {
            Duration = AnimationDuration.Medium,
            Subtype = AnimationSubtype.FromLeft
        };

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public Login()
        {
            InitializeComponent();

            txtContrasenia.Text = "";
            txtUsuario.Text = "";


        }

        private async void btnLogin(object sender, EventArgs e)
        {
            aicargando.IsRunning = true;
            btnlogin.IsVisible = false;

            if (!txtContrasenia.Text.Trim().Equals("")&&!txtUsuario.Text.Trim().Equals(""))
            {
                Get_Login login = new Get_Login();

              var respuesta = await login.logiarsePrincipal(txtContrasenia.Text, txtUsuario.Text);

                if (respuesta != null)
                {
                   

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                         conn.DeleteAll<Usuario>();
                        Usuario nuevo = new Usuario();

                        nuevo.Nombre = respuesta[0].Nombre;

                        nuevo.Apellido = respuesta[0].Apellido;

                        nuevo.Email = respuesta[0].Email;

                        nuevo.Contrasenia = respuesta[0].Contrasenia;

                        nuevo.Calle = respuesta[0].Calle;

                        nuevo.Altura = respuesta[0].Altura;

                        nuevo.Telefono = respuesta[0].Telefono;

                        nuevo.id_Usuario = respuesta[0].id;

                        conn.Insert(nuevo);
                    }


                    Application.Current.MainPage = new ContenedorImagen();

                    aicargando.IsRunning = false;
                    btnlogin.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("Alerta", "Usuario o Contraseñia incorrecta", "OK");
                    aicargando.IsRunning = false;
                    btnlogin.IsVisible = true;
                }
            }
            else
            {
                await DisplayAlert("Alerta", "Complete todo los campos", "OK");
                aicargando.IsRunning = false;
                btnlogin.IsVisible = true;
            }

            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(  new Registro());
        }


    }
}