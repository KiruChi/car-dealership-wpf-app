using Kursova.Classes;
using Kursova.DA_Layer;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Utilities.Bzip2;
using Org.BouncyCastle.Utilities.Encoders;
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

namespace Kursova.Add_windows
{
    /// <summary>
    /// Interaction logic for AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        private Clients CR;
        private Users user;
        private int k = 0;
        public string[] Names1 { get; set; }
        public AddClientWindow()
        {
            InitializeComponent();
            Initialize();
            Inf.Text = "Add";
            k = 0;
        }
        public AddClientWindow(Clients cr, Users user)
        {
            InitializeComponent();
            Initialize();
            CR = cr;
            TB4.Text = cr.Full_name;
            TB5.Text = cr.Phone.ToString();
            if (cr.Sex == "Male")
            {
                CB1.SelectedIndex = 0;
            }
            else if (cr.Sex == "Female")
            {
                CB1.SelectedIndex = 1;
            }
            TB6.Text = cr.Birthday;
            TB7.Text = cr.RNOCPP1.ToString();
            TB8.Text = cr.Address;
            k = 1;
            this.user = user;
            if (user.Role == "Salesperson")
            {
                btnAdd.Background = Brushes.White;
                btnAdd.IsEnabled = false;
            }
        }
        private void Initialize()
        {
            int i = 0;

            Names1 = new String[] { "Male", "Female" };

            DataContext = this;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                string formattedDateNow = now.ToString("yyyyMMdd");
                long result;
                string formattedDate = TB6.Text;
                if (Regex.IsMatch(formattedDate, @"^\d{2}\.\d{2}\.\d{4}$"))
                {
                    DateTime date = DateTime.ParseExact(TB6.Text, "dd.MM.yyyy", null);
                    formattedDate = date.ToString("yyyy-MM-dd");
                }
                if (TB4.Text.Count(c => c == ' ') != 2)
                {
                    MessageBox.Show("Incorrectly entered Name", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if ( TB5.Text.Length != 10)
                {
                    MessageBox.Show("Incorrectly entered Phone number", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (long.TryParse(TB5.Text, out result) == false)
                {
                    MessageBox.Show("Please enter a number in Phone number", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt64(TB5.Text) < 0)
                {
                    MessageBox.Show("Please enter a positive number in Phone number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB1.Text == "Sex")
                {
                    MessageBox.Show("Incorrectly entered Gender", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Regex.IsMatch(formattedDate, @"^\d{4}-\d{2}-\d{2}$") == false)
                {
                    MessageBox.Show("Incorrectly entered Birthday", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Math.Abs(Convert.ToInt64(formattedDateNow) - Convert.ToInt64(DateTime.ParseExact(TB6.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd"))) < 00180000)
                {
                    MessageBox.Show("It is not possible to register someone under the age of 18", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (TB8.Text == "")
                {
                    MessageBox.Show("Incorrectly entered Address", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (TB7.Text.Length != 10)
                {
                    MessageBox.Show("Incorrectly entered RNOCPP", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (k == 0)
                    {
                        int n = 0;
                        List<Clients> ts = ClientsDA.RetrieveClients();
                        foreach (Clients t in ts)
                        {
                            if (t.RNOCPP1.ToString() == TB7.Text)
                            {
                                n = 1;
                            }
                        }
                        if (n == 1)
                        {
                            MessageBox.Show("A client with such RNOCPP already exists", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }

                        int i = 0;
                        int j = 0;
                        List<Clients> sa = ClientsDA.RetrieveClients();
                        foreach (Clients u in sa)
                        {
                            i += 1;
                        }
                        ClientsDA.AddClient("false", TB4.Text, TB5.Text, CB1.Text, formattedDate, TB8.Text, TB7.Text);
                        sa = ClientsDA.RetrieveClients();
                        foreach (Clients u in sa)
                        {
                            j += 1;
                        }
                        if (j <= i)
                        {
                            MessageBox.Show("Error adding a new client", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("A new client has been added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            AddClWind.Close();
                        }
                    gt1:;
                    }
                    else if (k == 1)
                    {
                        ClientsDA.UpdateClient(CR.Id.ToString(), TB4.Text, TB5.Text, CB1.Text, formattedDate, TB8.Text, TB7.Text);
                        MessageBox.Show("The client has been changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            AddClWind.Close();
        }
    }
}
