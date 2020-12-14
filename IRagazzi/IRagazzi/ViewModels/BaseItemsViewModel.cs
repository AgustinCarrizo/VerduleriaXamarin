
using IRagazzi;
using IRagazzi.Clases;
using IRagazzi.Servicios;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContextMenuSample.ViewModels
{
    public class BaseItemsViewModel : BaseViewModel
    {
        public List<Compra> listaCompraInicio = new List<Compra>();
        private ICommand _deleteCommand;
        private ICommand _detalleTodos;
        public  ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(async item =>
        {
            var model = item as Compra;
            //model?.ForceCloseCommand?.Execute(false); // OneWayToSource binding. So we call Close method without animation
            if (App.NoEliminar == model.id)
            {
                Update_Compra update_Compra = new Update_Compra();
                update_Compra.UpdateCompra(model);
                listaCompraInicio = listaCompraInicio.Where(d => d.id != model.id).ToList();
                Items.Remove(model);
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new detallePedido(model.Productos));
                App.NoEliminar = model.id;
            }
            

        }));
        public ICommand DetalleCommand => _deleteCommand ?? (_deleteCommand = new Command(async item =>
        {
            var model = item as Compra;
            //model?.ForceCloseCommand?.Execute(false); // OneWayToSource binding. So we call Close method without animation
            //Update_Compra update_Compra = new Update_Compra();
            //update_Compra.UpdateCompra(model);
            //listaCompraInicio = listaCompraInicio.Where(d => d.id != model.id).ToList();
            //Items.Remove(model);
            await PopupNavigation.Instance.PushAsync(new detallePedido(model.Productos));

        }));

        public ICommand DetalleTodos => _detalleTodos ?? (_detalleTodos = new Command(async item =>
        {
            var model = item as Compra;
            //model?.ForceCloseCommand?.Execute(false); // OneWayToSource binding. So we call Close method without animation
            //Update_Compra update_Compra = new Update_Compra();
            //update_Compra.UpdateCompra(model);
            //listaCompraInicio = listaCompraInicio.Where(d => d.id != model.id).ToList();
            //Items.Remove(model);
            await PopupNavigation.Instance.PushAsync(new detallePedido(model.Productos));

        }));


        //private ICommand _muteCommand;
        //public ICommand MuteCommand => _muteCommand ?? (_muteCommand = new Command(item =>
        //{
        //    var model = item as Item;
        //    model.IsMuted = !model.IsMuted;
        //}));

        public ObservableCollection<Compra> Items { get; } = new ObservableCollection<Compra>
        {
            
        };


        public BaseItemsViewModel()
        {
            buscarCompra();
        }

        private async void buscarCompra()
        {
            listaCompraInicio = new List<Compra>();
            Get_Compra compra = new Get_Compra();
            listaCompraInicio = await compra.listaCompra();
            listaCompraInicio = listaCompraInicio.Where(d => d.Entregado == 0).OrderByDescending(d=>d.Fecha).ToList();
            



            foreach (var item in listaCompraInicio)
            {

                Items.Add(new Compra{

                    id = item.id,
                    id_Registros = item.id_Registros,
                    Productos = item.Productos.Substring(0, item.Productos.IndexOf("\n")) + ".....",
                    PrecioTotal = item.PrecioTotal
                    , Pago = item.Pago,
                    Direccion = item.Direccion,
                    Entregado = item.Entregado,
                    Fecha = item.Fecha,
                    FechaString = item.Fecha.ToString().Substring(0, item.Fecha.ToString().IndexOf(" ")),
                Telefono = item.Telefono,
                    Codigo = item.Codigo
                }
                    
                    );

            }
            
        }

        public sealed class Item : Compra
        {
           

            public ICommand ForceCloseCommand { get; set; }

           
        }
    }
}