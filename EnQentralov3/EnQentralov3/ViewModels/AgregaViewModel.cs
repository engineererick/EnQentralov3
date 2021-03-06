﻿using EnQentralov3.Common.Models;
using EnQentralov3.Helpers;
using EnQentralov3.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnQentralov3.ViewModels
{
    public class AgregaViewModel : BaseViewModel
    {

        #region Atributos
        private ImageSource imageSource;
        private ApiService apiService;
        private bool isRunning;
        private bool isEnable;

        private MediaFile file;

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

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }
        #endregion


        #region Constructor
        public AgregaViewModel()
        {
            this.IsEnable = true;
            this.apiService = new ApiService();
            ImageSource = "noimg";
        }
        #endregion


        #region Comandos
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
            this.IsEnable = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = true;
                this.IsEnable = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                return;
            }

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

            var pub = new Publicacion
            {
                Titulo = this.Titulo,
                Fecha = this.Fecha,
                Lugar = this.Lugar,
                Descripcion = this.Descripcion,
                Tipo = this.Tipo,
                ImageArray = imageArray,

            };

            var response = await this.apiService.CreatePub("https://enqentralov3api.azurewebsites.net", "/api", "/Publicacions", pub, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                this.IsEnable = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            var newPublicn = (Publicacion)response.Result;
            var publicsVM = PublicacionesViewModel.GetInstance();
            publicsVM.Publics.Add(newPublicn);

            this.IsEnable = true;
            this.IsRunning = false;
            await App.Navigator.PopAsync();

        }

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }

        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet("ImageSource", "Cancel", null, "FromGallery", "NewPicture");

            if (source == "Cancel")
            {
                this.file = null;
                return;
            }
            
            if (source == "NewPicture")
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }

        }

        #endregion
    }
}
