using Kursova.Classes;
using Kursova.DA_Layer;
using Kursova.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Interop;
using Kursova.Pages;
using MySqlX.XDevAPI;

namespace Kursova.View_Layer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Users user;
        private string s;
        public CarsWindow carW;
        public ClientsWindow clientsW;
        public EmployeesWindow employeesW = new EmployeesWindow();
        public BranchesWindow branchesW = new BranchesWindow();
        public SalesWindow salesW = new SalesWindow();
        public StatisticsWindow statisticsW = new StatisticsWindow();
        public string S { get => s; set => s = value; }

        public MainWindow(Users user)
        {
            InitializeComponent();
            DBHelper.EstablishConnection();
            this.user = user;
            if (user.Role == "Salesperson")
            {
                TB1.Foreground = Brushes.White;
                B1.Background = Brushes.White;
                btnEmployees.Visibility = Visibility.Hidden;
                btnBranches.Visibility = Visibility.Hidden;
                btnSales.Visibility = Visibility.Hidden;
                btnStatistics.Visibility = Visibility.Hidden;
            }
            carW = new CarsWindow(user);
            clientsW = new ClientsWindow(user);
            btnCars.IsChecked = true;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnFullsize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CarsCheck(object sender, RoutedEventArgs e)
        {
            ViewBox.Content = carW;
            btnEmployees.IsChecked = false;
            btnBranches.IsChecked = false;
            btnSales.IsChecked = false;
            btnStatistics.IsChecked = false;
        }

        private void EmployeesCheck(object sender, RoutedEventArgs e)
        {
            ViewBox.Content = employeesW;
            btnCars.IsChecked = false;
            btnClients.IsChecked = false;
        }

        private void BranchesCheck(object sender, RoutedEventArgs e)
        {
            ViewBox.Content = branchesW;
            btnCars.IsChecked = false;
            btnClients.IsChecked = false;
        }

        private void ClientsCheck(object sender, RoutedEventArgs e)
        {
            ViewBox.Content = clientsW;
            btnEmployees.IsChecked = false;
            btnBranches.IsChecked = false;
            btnSales.IsChecked = false;
            btnStatistics.IsChecked = false;
        }

        private void SalesCheck(object sender, RoutedEventArgs e)
        {
            ViewBox.Content = salesW;
            btnCars.IsChecked = false;
            btnClients.IsChecked = false;
        }

        private void StatisticsCheck(object sender, RoutedEventArgs e)
        {
            ViewBox.Content = statisticsW;
            btnCars.IsChecked = false;
            btnClients.IsChecked = false;
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            S = Search.Text;
            if (btnCars.IsChecked == true)
            {
                carW.SearchC(S);
            }
            if (btnClients.IsChecked == true)
            {
                clientsW.SearchC(S);
            }
            if (btnEmployees.IsChecked == true)
            {
                employeesW.SearchC(S);
            }
            if (btnBranches.IsChecked == true)
            {
                branchesW.SearchC(S);
            }
            if (btnSales.IsChecked == true)
            {
                salesW.SearchC(S);
            }
            if (btnStatistics.IsChecked == true)
            {
                statisticsW.SearchC(S);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login_Window Window = new Login_Window();
            Window.Show();
            this.Close();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (btnCars.IsChecked == true)
            {
                carW.FilterC(S);
            }
            if (btnClients.IsChecked == true)
            {
                clientsW.FilterC(S);
            }
            if (btnEmployees.IsChecked == true)
            {
                employeesW.FilterC(S);
            }
            if (btnBranches.IsChecked == true)
            {
                branchesW.FilterC(S);
            }
            if (btnSales.IsChecked == true)
            {
                salesW.FilterC(S);
            }
            if (btnStatistics.IsChecked == true)
            {
                statisticsW.FilterC(S);
            }
        }
    }
}
