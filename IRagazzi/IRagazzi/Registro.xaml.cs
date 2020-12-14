using IRagazzi.Clases;
using IRagazzi.Servicios;
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
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtContrasenia.Text = "";
            txtContraseniaRepetir.Text = "";
            txttelefono.Text = "";
            txtAltura.Text = "";
            txtCalle.Text = "";
            txtPiso.Text = "";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            btnregistrar.IsVisible = false;
            aicargando.IsRunning = true;
            if ( !txtNombre.Text.Trim().Equals( "") &&
                 !txtApellido.Text.Trim().Equals("") &&
                 !txtEmail.Text.Trim().Equals("") &&
                 !txtContrasenia.Text.Trim().Equals("") &&
                 !txtContraseniaRepetir.Text.Trim().Equals("") &&
                 !txttelefono.Text.Trim().Equals("") &&
                 !txtCalle.Text.Trim().Equals("") &&
                 !txtAltura.Text.Trim().Equals("") )
            {

                if (txtContrasenia.Text.Trim().Equals(
                 txtContraseniaRepetir.Text.Trim()) )
                {
                    insert_Usuario insert_Usuario = new insert_Usuario();
                    UsuarioCompleto usuarioCompleto = new UsuarioCompleto();
                    usuarioCompleto.Usuario = txtNombre.Text.Trim() + txtApellido.Text.Trim();

                    usuarioCompleto.Nombre = txtNombre.Text.Trim();

                    usuarioCompleto.Apellido = txtApellido.Text.Trim();

                    usuarioCompleto.Email = txtEmail.Text.Trim();

                    usuarioCompleto.Contrasenia = txtContrasenia.Text.Trim();

                    usuarioCompleto.Calle = txtCalle.Text.Trim();

                    usuarioCompleto.Altura = txtAltura.Text.Trim() + txtPiso.Text.Trim();

                    usuarioCompleto.Telefono = txttelefono.Text.Trim();

                    insert_Usuario.insertUsuario(usuarioCompleto);

                }
                else
                {
                    await DisplayAlert("Alerta", "contraseñia no coincide", "OK");
                    aicargando.IsRunning = false;
                    btnregistrar.IsVisible = true;
                }



            }
            else
            {
                await DisplayAlert("Alerta", "Complete todo los campos", "OK");
                aicargando.IsRunning = false;
                btnregistrar.IsVisible = true;
            }
        }
    }
}