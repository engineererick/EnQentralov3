using EnQentralov3.Common.Models;
using EnQentralov3.Helpers;
using EnQentralov3.Services;
using EnQentralov3.ViewModels;
using EnQentralov3.Views;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EnQentralov3
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        //public static MasterPage Master { get; internal set; }

        public App()
        {
            InitializeComponent();

            MainViewModel.GetInstance().Login = new LoginViewModel();

            MainPage = new NavigationPage(new MainLoginPage()); //PublicacionesPage MainLoginPage
        }

        public static Action HideLoginView
        {
            get
            {
                return new Action(() => Current.MainPage = new NavigationPage(new MainLoginPage()));
            }
        }

        public static async Task NavigateToProfile(TokenResponse token)
        {
            if (token == null)
            {
                Current.MainPage = new NavigationPage(new MainLoginPage());
                return;
            }
            
            Settings.AccessToken = token.AccessToken;
            Settings.TokenType = token.TokenType;

            var apiService = new ApiService();
            var url = Current.Resources["UrlAPI"].ToString();
            var prefix = "\api";
            var controller = Current.Resources["UrlUsersController"].ToString();
            var response = await apiService.GetUser(url, prefix, $"{controller}/GetUser", token.UserName, token.TokenType, token.AccessToken);
            if (response.IsSuccess)
            {
                var userASP = (MyUserASP)response.Result;
                MainViewModel.GetInstance().UserASP = userASP;
                Settings.UserASP = JsonConvert.SerializeObject(userASP);
            }

            MainViewModel.GetInstance().Publics = new PublicacionesViewModel();
            Current.MainPage = new PublicacionesPage();
            /*MainViewModel.GetInstance().Agrega = new AgregaViewModel();
            Application.Current.MainPage = new AgregarPage();*/

        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
