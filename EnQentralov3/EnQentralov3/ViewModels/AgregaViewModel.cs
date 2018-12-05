using EnQentralov3.Common.Models;
using EnQentralov3.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnQentralov3.ViewModels
{
    public class AgregaViewModel : BaseViewModel
    {
        ApiService apiService;

        public string Id { get; set; }
        public string Tipo { get; set; }
        public string Lugar { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }

        public AgregaViewModel()
        {
            apiService = new ApiService();
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
            try
            {
                await apiService.CreatePub(new Publicacion()
                {
                    PubId = 2,
                    Titulo = this.Titulo,
                    UsuPub = "Erick Galindo",
                    Fecha = this.Fecha,
                    Lugar = this.Lugar,
                    Descripcion = this.Descripcion,
                    Tipo = this.Tipo
                });
                
                await Application.Current.MainPage.DisplayAlert("Publicación", "Se ha creado la publicación.", "Información");
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Publicación", "Ha ocurrido un error inesperado", "Error");
            }
        }
    }
}
