using EnQentralov3.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EnQentralov3.Services
{
    public class NavigationService : ContentView
    {
        public NavigationService()
        {

        }

        public async void Navigate(string PageName)
        {
            App.Master.IsPresented = false;
            switch (PageName)
            {
                case "Buscar":
                    await App.Navigator.PushAsync(new BuscaPage());
                    break;
                case "Agregar":
                    await App.Navigator.PushAsync(new AgregarPage());
                    break;
                case "Publicaciones":
                    //await App.Navigator.PushAsync(new Publicaciones());
                    break;
                case "Ajustes":
                    //await App.Navigator.PushAsync(new UserPage());
                    break;
                case "CerrarSesion":
                    //await App.Navigator.PushAsync(new CerrarSesion());
                    break;
                default:
                    break;
            }

        }

        internal void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "MasterPage":
                    App.Current.MainPage = new MasterPage();
                    break;
                default:
                    break;
            }
        }
    }
}
