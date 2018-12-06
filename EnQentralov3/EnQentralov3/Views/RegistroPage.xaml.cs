﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnQentralov3.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistroPage : ContentPage
	{
		public RegistroPage ()
		{
			InitializeComponent ();
            btn_Registro.Clicked += btnRegistrarse_Clicked;
        }
        private void btnRegistrarse_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new MainLoginPage());
        }

    }
}