using Kursova.Classes;
using Kursova.DA_Layer;
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
    /// Interaction logic for AddSaleWindow.xaml
    /// </summary>
    public partial class AddSaleWindow : Window
    {
        private Sales CR;
        private List<Cars> DR1;
        private List<Clients> DR2;
        private List<Employees> DR3;
        private int k = 0;
        public string[] Names1 { get; set; }
        public string[] Names2 { get; set; }
        public AddSaleWindow()
        {
            InitializeComponent();
            Initialize();
            Inf.Text = "Add";
            k = 0;
        }
        public AddSaleWindow(Sales cr)
        {
            InitializeComponent();
            Initialize();
            CR = cr;
            TB1.Text = cr.Carvin;
            int j = 0;
            foreach (Cars c in DR1)
            {
                if (TB1.Text == c.Vin_code.ToString())
                {
                    TB2.Text = c.Name;
                    j = 1;
                }
            }
            if (j == 0)
            {
                TB2.Text = "";
            }
            foreach (var i in Names1)
            {
                if (cr.Clientname == i)
                {
                    CB1.SelectedItem = i;
                }
            }
            TB3.Text = cr.ClientRNOCPP.ToString();
            foreach (var i in Names2)
            {
                if (cr.Employeename == i)
                {
                    CB2.SelectedItem = i;
                }
            }
            TB4.Text = cr.EmployeeRNOCPP.ToString(); 
            TB5.Text = cr.Date;
            TB6.Text = cr.Price.ToString();
            k = 1;
        }
        private void Initialize()
        {
            int i = 0;
            TB1.TextChanged += TB1Changed;
            CB1.SelectionChanged += CB1_Changed;
            CB2.SelectionChanged += CB2_Changed;
            DR2 = ClientsDA.RetrieveClients();
            i = DR2.Count();
            Names1 = new String[i];
            i = 0;
            foreach (Clients c in DR2)
            {
                Names1[i] = c.Full_name;
                i += 1;
            }

            DR3 = EmployeesDA.RetrieveEmployees();
            i = DR3.Count();
            Names2 = new String[i];
            i = 0;
            foreach (Employees c in DR3)
            {
                Names2[i] = c.Full_name;
                i += 1;
            }

            DR1 = CarsDA.RetrieveCars();

            DataContext = this;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long result;
                string formattedDate1 = TB5.Text;
                if (Regex.IsMatch(formattedDate1, @"^\d{2}\.\d{2}\.\d{4}$"))
                {
                    DateTime date = DateTime.ParseExact(TB5.Text, "dd.MM.yyyy", null);
                    formattedDate1 = date.ToString("yyyy-MM-dd");
                }
                if (TB2.Text == "")
                {
                    MessageBox.Show("Incorrectly entered Car vin code", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB1.Text == "Client")
                {
                    MessageBox.Show("Incorrectly entered Client", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB2.Text == "Employee")
                {
                    MessageBox.Show("Incorrectly entered Employee", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Regex.IsMatch(formattedDate1, @"^\d{4}-\d{2}-\d{2}$") == false)
                {
                    MessageBox.Show("Incorrectly entered Birthday", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else  if(long.TryParse(TB6.Text, out result) == false)
                {
                    MessageBox.Show("Please enter a number in Price", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt64(TB6.Text) < 0)
                {
                    MessageBox.Show("Please enter a positive number in Price", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (k == 0)
                    {
                        int n = 0;
                        List<Sales> ts = SalesDA.RetrieveSales();
                        foreach (Sales t in ts)
                        {
                            if (t.Carvin.ToString() == TB1.Text)
                            {
                                n = 1;
                            }
                        }
                        if (n == 1)
                        {
                            MessageBox.Show("A sale with such car vin code already exists", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }

                        int i = 0;
                        int j = 0;
                        List<Sales> sa = SalesDA.RetrieveSales();
                        foreach (Sales u in sa)
                        {
                            i += 1;
                        }
                        SalesDA.AddSale("false", TB1.Text, (CB1.SelectedIndex + 1).ToString(), (CB2.SelectedIndex + 1).ToString(), formattedDate1, TB6.Text);
                        sa = SalesDA.RetrieveSales();
                        foreach (Sales u in sa)
                        {
                            j += 1;
                        }
                        if (j <= i)
                        {
                            MessageBox.Show("Error adding a new sale", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("A new sale has been added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                    gt1:;
                    }
                    else if (k == 1)
                    {
                        SalesDA.UpdateSale(CR.Id.ToString(), TB1.Text, (CB1.SelectedIndex + 1).ToString(), (CB2.SelectedIndex + 1).ToString(), formattedDate1, TB6.Text);
                        MessageBox.Show("The sale has been changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
            this.Close();
        }

        private void CB1_Changed(object sender, SelectionChangedEventArgs e)
        {
            string selected = CB1.SelectedValue.ToString();
            int a = CB1.SelectedIndex + 1;
            List<Clients> saleslist = ClientsDA.RetrieveClients();
            foreach (Clients i in saleslist)
            {
                if (i.Id == a)
                {
                    TB3.Text = i.RNOCPP1.ToString();
                }
            }
        }

        private void CB2_Changed(object sender, SelectionChangedEventArgs e)
        {
            string selected = CB2.SelectedValue.ToString();
            int a = CB2.SelectedIndex + 1;
            List<Employees> saleslist = EmployeesDA.RetrieveEmployees();
            foreach (Employees i in saleslist)
            {
                if (i.Id == a)
                {
                    TB4.Text = i.RNOCPP1.ToString();
                }
            }
        }

        private void TB1Changed(object sender, TextChangedEventArgs e)
        {
            int k = 0;
            foreach (Cars c in DR1)
            {
                if (TB1.Text == c.Vin_code.ToString())
                {
                    TB2.Text = c.Name;
                    k = 1;
                }
            }
            if (k == 0)
            {
                TB2.Text = "";
            }
        }
    }
}