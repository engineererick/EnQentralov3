using EnQentralov3.Helpers;
using EnQentralov3.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnQentralov3.ViewModels
{
    public class MenuItemViewModel
    {
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }


        public ICommand GotoCommand
        {
            get
            {
                return new RelayCommand(GoTo);
            }
        }

        private void GoTo()
        {
            if (this.PageName == "LoginPage")
            {
                Settings.AccessToken = string.Empty;
                Settings.TokenType = string.Empty;

                MainViewModel.GetInstance().Login = new LoginViewModel();
                Application.Current.MainPage = new NavigationPage(new MainLoginPage());
            }
        }
    }
}
