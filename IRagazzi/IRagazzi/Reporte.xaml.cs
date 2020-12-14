using IRagazzi.Clases;
using IRagazzi.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IRagazzi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reporte : ContentPage
    {
        public Reporte()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            ReporteInfluencer reporteInfluencer = new ReporteInfluencer();

            reporteInfluencer.FechaDesde = fechadesde.Date;
            reporteInfluencer.FechaHasta = fechahasta.Date;

            ReporteInfluencerDB reporte = new ReporteInfluencerDB();
            List<ReporteInfluencer> resultado = await reporte.ReporteInfluencer(reporteInfluencer);

            foreach (var item in resultado)
            {
                item.FechaDesdes = item.FechaDesde.ToString("yyyy-MM-dd");
                item.FechaHastas = item.FechaHasta.ToString("yyyy-MM-dd");
            }

            lstinfluencer.ItemsSource = resultado;
        }
    }
}