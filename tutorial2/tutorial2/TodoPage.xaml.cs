using Nec.Nebula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace tutorial2
{
    /// <summary>
    /// TodoPage.xaml の相互作用ロジック
    /// </summary>
    public partial class TodoPage : Page
    {
        private TodoViewModel _viewModel;

        public TodoPage()
        {
            InitializeComponent();

            var service = NbService.GetInstance();
            _viewModel = new TodoViewModel();
            DataContext = _viewModel;

            _viewModel.Reload();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Add(TodoTextBox.Text);
            TodoTextBox.Clear();
        }

        private async void TodoListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete)
            {
                return;
            }

            var idx = TodoListBox.SelectedIndex;
            if (idx < 0) return; // not selected

            _viewModel.RemoveAt(idx);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var page = new LoginPage();
            NavigationService.Navigate(page);
        }
    }
}
