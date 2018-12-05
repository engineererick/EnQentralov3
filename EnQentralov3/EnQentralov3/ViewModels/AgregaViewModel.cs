using EnQentralov3.Common.Models;
using EnQentralov3.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnQentralov3.ViewModels
{
    public class AgregaViewModel : BaseViewModel
    {
        private ApiService apiService;
        private bool isRunning;
        private bool isEnable;


        public string Tipo { get; set; }
        public string Lugar { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        public bool IsEnable
        {
            get { return this.isEnable; }
            set { this.SetValue(ref this.isEnable, value); }
        }

        public AgregaViewModel()
        {
            this.IsEnable = true;
            this.apiService = new ApiService();
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            this.IsRunning = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                return;
            }

            var pub = new Publicacion
            {
                Titulo = this.Titulo,
                Fecha = this.Fecha,
                Lugar = this.Lugar,
                Descripcion = this.Descripcion,
                Tipo = this.Tipo
            };

            var response = await this.apiService.CreatePub("https://enqentralov3api.azurewebsites.net", "/api", "/Publicacions", pub);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            this.IsRunning = false;
            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}
