using OtpMainGui.viewmodels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OtpMainGui
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM _viewModel = null;


        public MainWindow(MainWindowVM viewModel)
        {
            DataContext = viewModel;
            _viewModel = viewModel;
            InitializeComponent();

        }

        private void Button_Click_Logon(object sender, RoutedEventArgs e)
        {
            _viewModel.logon();
        }

        private void Button_Click_Register(object sender, RoutedEventArgs e)
        {
            _viewModel.register();
        }

        private void Button_Click_Show(object sender, RoutedEventArgs e)
        {
            _viewModel.show();
        }
        private void Button_Click_ClearRegister(object sender, RoutedEventArgs e)
        {
            _viewModel.clearRegister();
        }
        private void Button_Click_ClearShow(object sender, RoutedEventArgs e)
        {
            _viewModel.clearShow();
        }
    }
}