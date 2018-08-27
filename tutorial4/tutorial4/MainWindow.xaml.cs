using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Nec.Nebula;
using tutorial4;
using System.Diagnostics;

namespace tutorial4
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private TodoViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize Nebula
            var service = NbService.GetInstance();
            service.TenantId = "";
            service.AppId = "";
            service.AppKey = "";
            service.EndpointUrl = "https://baas.example.com/api";

            // Enable offline
            NbOfflineService.EnableOfflineService(service, "1234567890");

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

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Sync();
        }
    }
}
