using EnQentralov3.Services;
using EnQentralov3.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnQentralov3.ViewModels
{
    public class MainViewModel
    {
        public PublicacionesViewModel Publics { get; set; }

        public MainViewModel()
        {
            this.Publics = new PublicacionesViewModel();
        }

        public ICommand BuscaCommand
        {
            get
            {
                return new RelayCommand(GoToBusca);
            }
        }

        private async void GoToBusca()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new BuscaPage());
        }
    }
}
