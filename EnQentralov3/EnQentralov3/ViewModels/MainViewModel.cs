using EnQentralov3.Services;
using EnQentralov3.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnQentralov3.ViewModels
{
    public class MainViewModel
    {
        public PublicacionesViewModel Publics { get; set; }
        public BuscaViewModel BuscPub { get; set; }

        public MainViewModel()
        {
            this.Publics = new PublicacionesViewModel();
            //LoadMenu();
        }

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        private void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel()
                {
                    Icon = "lupa.png",
                    Title = "Buscar",
                    PageName = "BuscaPage"
                },

                new MenuItemViewModel()
                {
                    Icon = "agregar.png",
                    Title = "Agregar",
                    PageName = "AgregaPage"
                }
            };

            /*Menu.Add(new MenuItemViewModel()
            {
                Icon = "publicaciones.png",
                Title = "Publicaciones",
                PageName = "Publicaciones"
            });

            Menu.Add(new MenuItemViewModel()
            {
                Icon = "ajustes.png",
                Title = "Ajustes",
                PageName = "Ajustes"
            });

            Menu.Add(new MenuItemViewModel()
            {
                Icon = "cerrars.png",
                Title = "Cerrar Sesión",
                PageName = "CerrarSesion"
            });*/
        }


        public ICommand AgregaCommand
        {
            get
            {
                return new RelayCommand(GoToBusca);
            }
        }

        private async void GoToBusca()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AgregarPage());
        }
    }
}
