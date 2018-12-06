using EnQentralov3.Common.Models;
using EnQentralov3.Services;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnQentralov3.ViewModels
{
    public class PublicacionesViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;

        private bool isRefreshing;

        private ObservableCollection<Publicacion> publics;

        public ObservableCollection<Publicacion> Publics
        {
            get { return this.publics; }
            set { this.SetValue(ref this.publics, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        #endregion


        #region Constructor
        public PublicacionesViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadPubs();
        }
        #endregion


        #region Singleton
        private static PublicacionesViewModel instance;

        public static PublicacionesViewModel GetInstance()
        {
            if (instance==null)
            {
                return new PublicacionesViewModel();
            }

            return instance;
        }
        #endregion


        #region Metodoss
        private async void LoadPubs()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                return;
            }
            //var url = Application.Current.Resources["UrlAPI"].ToString();
            //var urlPrefix = Application.Current.Resources["UrlPrefix"].ToString();
            //var urlController = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.GetList<Publicacion>("https://enqentralov3api.azurewebsites.net", "/api", "/Publicacions");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            var list = (List<Publicacion>)response.Result;
            this.Publics = new ObservableCollection<Publicacion>(list);
            this.IsRefreshing = false;
        }
        #endregion


        #region Comandos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadPubs);
            }
        }
        #endregion
    }
}
