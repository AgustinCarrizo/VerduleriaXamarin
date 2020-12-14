using System;
using System.Collections.Generic;
using IRagazzi.Clases;
using IRagazzi.Servicios;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace IRagazzi
{
    public partial class popUpInfluencer : PopupPage
    {
        public popUpInfluencer()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            aicargando.IsRunning = true;
            btncargar.IsVisible = false;
            var respuesta = false;
            influencer productos = new influencer();




            productos.Nombre = txtNombre.Text;

            productos.Codigo = txtcodigo.Text;


            productos.Descuento = int.Parse(txtdescuento.Text);


            Insert_influencer insert = new Insert_influencer();

            respuesta = await insert.nuevoInfluencer(productos);


            if (respuesta == true)
            {
                aicargando.IsRunning = false; 
                MessagingCenter.Send((new Inicio()), "BtnEmisor");
                await PopupNavigation.PopAsync();
                btncargar.IsVisible = true;
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
            await PopupNavigation.RemovePageAsync(this);
        }
    }
}
