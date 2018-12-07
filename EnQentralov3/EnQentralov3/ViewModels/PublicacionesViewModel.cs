using EnQentralov3.Common.Models;
using EnQentralov3.Helpers;
using EnQentralov3.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnQentralov3.ViewModels
{
    public class PublicacionesViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;
        public DataService dataService;

        public List<Publicacion> MyPubs { get; set; }

        public string Filter { get; set; }
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
            this.dataService = new DataService();
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


        #region Metodos
        private async void LoadPubs()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (connection.IsSuccess)
            {
                var answer = await this.LoadPubsFromAPI();
                if (answer)
                {
                    this.SaveProductsToDB();
                }
            }
            else
            {
                await this.LoadPubsFromDB();
            }

            if (this.MyPubs == null || this.MyPubs.Count == 0)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Intenta más tarde", "Aceptar");
                return;
            }
            
            
            this.IsRefreshing = false;
        }

        private async Task LoadPubsFromDB()
        {
            this.MyPubs = await this.dataService.GetAllProducts();
        }

        private async Task SaveProductsToDB()
        {
            await this.dataService.DeleteAllProducts();
            this.dataService.Insert(this.MyPubs);
        }


        private async Task<bool> LoadPubsFromAPI()
        {
            var response = await this.apiService.GetList<Publicacion>("https://enqentralov3api.azurewebsites.net", "/api", "/Publicacions", Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }
            else
            {
                await this.LoadPubsFromDB();
            }

            this.MyPubs = (List<Publicacion>)response.Result;
            this.Publics = new ObservableCollection<Publicacion>(MyPubs);
            return true;
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
