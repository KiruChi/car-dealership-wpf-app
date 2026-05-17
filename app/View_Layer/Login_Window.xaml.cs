using Kursova.Classes;
using Kursova.DA_Layer;
using Kursova.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace Kursova.View_Layer
{
    /// <summary>
    /// Interaction logic for Login_Window.xaml
    /// </summary>
    public partial class Login_Window : Window
    {
        public Login_Window()
        {
            InitializeComponent();
            DBHelper.EstablishConnection();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string log = User.Text;
            string password = Passw.Password;

            Users aUser = UsersDA.RetrieveUser(log);

            if (aUser != null && aUser.Password.Equals(password))
            {
                if (string.IsNullOrWhiteSpace(log) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("The field for email or password cannot be empty");
                    return;
                }
                MessageBox.Show("You are successfully logged in", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow Wind = new MainWindow(aUser);
                Wind.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect username or password", "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
                User.Text = "";
                Passw.Password = "";
            }
        }
    }
}
