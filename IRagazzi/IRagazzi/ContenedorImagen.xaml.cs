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
    public partial class ContenedorImagen : MasterDetailPage
    {
        public ContenedorImagen()
        {
            InitializeComponent();
            this.Master = new MenuLateral();
            this.Detail = new NavigationPage(new MainPage());

            App.MasterD = this;
        }
    }
}