using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DienChanCRM.DAL;
using DienChanCRM.Main;
using DienChanCRM.Models;

namespace DienChanCRM.Login
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        private readonly LoginViewModel _vm;
        private readonly GetAuthenticationQuery _getAuthenticationQuery;
        public LoginView()
        {
            InitializeComponent();

            _getAuthenticationQuery = new GetAuthenticationQuery();

            _vm = new LoginViewModel
            {
                Version = ConfigurationManager.AppSettings["Version"]
            };

            DataContext = _vm;
        }

        private void LoginView_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((Window) Parent).WindowState = WindowState.Maximized;
        }

        private void BttLogin_OnClick(object sender, RoutedEventArgs e)
        {
            Process();
        }

        private void BttClose_OnClick(object sender, RoutedEventArgs e)
        {
            if (
                MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            Application.Current.Shutdown();
        }

        private void TxtUsername_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;

            Process();
        }

        private void TxtPassword_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;

            Process();
        }

        private void Process()
        {
            _vm.Password = TxtPassword.Password;

            var user = _vm.IsPasswordValid(_vm);

            if (user != null)
            {
                var dashboardView = new MainView(user);

                NavigationService.Navigate(dashboardView);

                return;
            }

            Task.Factory.StartNew(() =>
            {
                _vm.ErrorMessage = "Invalid username/password!!!";

                Thread.Sleep(5000);

                _vm.ErrorMessage = "";
            });

            TxtUsername.Focus();

            TxtUsername.SelectAll();
        }

        public User IsPasswordValid(LoginViewModel vm)
        {
            var hashPassword = ComputeHash(vm.Password);

            return _getAuthenticationQuery.GetAuthentication(vm.UserName, hashPassword);
        }

        private string ComputeHash(string password)
        {
            using (var md5Hash = MD5.Create())
            {
                var bytes = Encoding.ASCII.GetBytes(password);

                var data = md5Hash.ComputeHash(bytes);

                var sb = new StringBuilder();

                foreach (var d in data)
                {
                    sb.Append(d.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
