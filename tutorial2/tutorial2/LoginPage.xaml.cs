using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tutorial2
{
    /// <summary>
    /// LoginPage.xaml の相互作用ロジック
    /// </summary>
    public partial class LoginPage : Page
    {
        private UserViewModel _user;

        public LoginPage()
        {
            InitializeComponent();

            _user = new UserViewModel();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var ret = await _user.Login(mailBox.Text, passwordBox.Password);
            if (!ret)
            {
                System.Windows.Forms.MessageBox.Show("Failed to login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mailBox.Clear();
                passwordBox.Clear();
                return;
            }

            var page = new TodoPage();
            NavigationService.Navigate(page);
        }
    }
}
