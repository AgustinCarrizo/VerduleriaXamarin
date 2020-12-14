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
    public partial class MenuLateral : ContentPage
    {
        public MenuLateral()
        {
            InitializeComponent();
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {

                var completado = conn.Table<Usuario>().FirstOrDefault();
                lblNombre.Text = completado.Apellido + " " + completado.Nombre;

            }
           
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await (App.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new CarroCompra());
           
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await(App.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new Pedidos(0 ,"n"));
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await(App.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new Pedidos(1, "n"));
        }
    }
}