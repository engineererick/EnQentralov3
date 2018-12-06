using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnQentralov3.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainLoginPage : ContentPage
	{
		public MainLoginPage ()
		{
			InitializeComponent ();
            //btn_Entrar.Clicked += btnEntrar_Clicked;
            //labelTapped();
        }

        private void btnEntrar_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new PublicacionesPage());
        }

        void OnFbButton(object sender, EventArgs e)
        {
            try
            {
                ((NavigationPage)this.Parent).PushAsync(new PublicacionesPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void OnGgButton(object sender, EventArgs e)
        {
            try
            {
                ((NavigationPage)this.Parent).PushAsync(new PublicacionesPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*private void labelTapped()
        {
            lblTapped.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    ((NavigationPage)this.Parent).PushAsync(new RegistroPage());
                })
            });
        }*/
    }
}