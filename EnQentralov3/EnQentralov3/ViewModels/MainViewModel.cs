using EnQentralov3.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnQentralov3.ViewModels
{
    public class MainViewModel
    {
        public PublicacionesViewModel Publics { get; set; }

        public MainViewModel()
        {
            this.Publics = new PublicacionesViewModel();
        }
    }
}
