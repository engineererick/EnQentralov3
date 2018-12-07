using EnQentralov3.Common.Models;
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
        public LoginViewModel Login { get; set; }
        public AgregaViewModel Agrega { get; set; }
        public PublicacionesViewModel Publics { get; set; }
        public BuscaViewModel BuscPub { get; set; }
        public RegisterViewModel Register { get; set; }

        public MyUserASP UserASP { get; set; }

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public MainViewModel()
        {
            instance = this;
            Agrega = new AgregaViewModel();
            LoadMenu();
        }

        public string UserFullName
        {
            get
            {
                if (this.UserASP != null && this.UserASP.Claims != null && this.UserASP.Claims.Count > 1)
                {
                    return $"{this.UserASP.Claims[0].ClaimValue} {this.UserASP.Claims[1].ClaimValue}";
                }

                return null;
            }
        }

        public string UserImageFullPath
        {
            get
            {
                if (this.UserASP != null && this.UserASP.Claims != null && this.UserASP.Claims.Count > 1)
                {
                    return $"https://enqentralov3api.azurewebsites.net{this.UserASP.Claims[3].ClaimValue.Substring(1)}";
                }

                return null;
            }
        }


        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
                return new MainViewModel();
            return instance;
        }


        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "lupa",
                PageName = "AboutPage",
                Title = "Acerca",
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ajustes",
                PageName = "SetupPage",
                Title = "Configuración",
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "cerrars",
                PageName = "LoginPage",
                Title = "Cerrar Sesión",
            });
        }



        public ICommand AgregaCommand
        {
            get
            {
                return new RelayCommand(GoToAgrega);
            }
        }

        private async void GoToAgrega()
        {
            await App.Navigator.PushAsync(new AgregarPage());
        }
    }
}
