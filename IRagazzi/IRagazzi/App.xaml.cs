using FormsControls.Base;
using IRagazzi.Clases;
using IRagazzi.Servicios;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IRagazzi
{
    public partial class App : Application
    {
        public static string FilePath;
        public static List<Productos> listProductos;
        public static int NoEliminar;
        public static ContenedorImagen MasterD { get; set; }
        public App()
        {
            listProductos = new List<Productos>();
            InitializeComponent();
            
            MainPage = new NavigationPage( new Login());
            
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("4CAF50");

        }
        public App(string filePath)
        {
            listProductos = new List<Productos>();
            InitializeComponent();
            
            MainPage = new NavigationPage( new Login());
            
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("4CAF50");


            FilePath = filePath;
        }



        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }


    }
}
