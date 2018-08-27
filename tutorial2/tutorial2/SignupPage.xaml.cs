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
    /// SignUp.xaml の相互作用ロジック
    /// </summary>
    public partial class SignUp : Page
    {
        private UserViewModel _user;

        public SignUp()
        {
            InitializeComponent();

            _user = new UserViewModel();
        }

        private async void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            var ret = await _user.Signup(mailBox.Text, passwordBox.Password);
            if (!ret)
            {
                System.Windows.Forms.MessageBox.Show("Failed to signup", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mailBox.Clear();
                passwordBox.Clear();
                return;
            }

            var page = new LoginPage();
            NavigationService.Navigate(page);
        }
    }
}
