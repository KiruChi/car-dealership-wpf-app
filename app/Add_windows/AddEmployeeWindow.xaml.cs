using Kursova.Classes;
using Kursova.DA_Layer;
using MySqlX.XDevAPI.Common;
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
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private Employees CR;
        private List<Branches> DR1;
        private int k = 0;
        public string[] Names1 { get; set; }
        public string[] Names2 { get; set; }
        public AddEmployeeWindow()
        {
            InitializeComponent();
            Initialize();
            Inf.Text = "Add";
            k = 0;
        }
        public AddEmployeeWindow(Employees cr)
        {
            InitializeComponent();
            Initialize();
            CR = cr;
            TB1.Text = cr.Full_name;
            TB2.Text = cr.Phone.ToString();
            TB3.Text = cr.Email;
            if (cr.Sex == "Male")
            {
                CB1.SelectedIndex = 0;
            }
            else if (cr.Sex == "Female")
            {
                CB1.SelectedIndex = 1;
            }
            TB4.Text = cr.Birthday;
            TB5.Text = cr.Post;
            TB6.Text = cr.Address;
            TB7.Text = cr.Passport.ToString();
            TB8.Text = cr.RNOCPP1.ToString();
            foreach(var i in Names2)
            { 
                if (cr.Motor_show == i)
                {
                    CB2.SelectedItem = i;
                }
            }
            k = 1;
        }
        private void Initialize()
        {
            int i = 0;

            Names1 = new String[] { "Male", "Female" };

            DR1 = BranchesDA.RetrieveMotor_shows();
            i = DR1.Count();
            Names2 = new String[i];
            i = 0;
            foreach (Branches c in DR1)
            {
                Names2[i] = c.Name;
                i += 1;
            }

            DataContext = this;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                string formattedDateNow = now.ToString("yyyyMMdd");
                long result;
                string formattedDate1 = TB4.Text;
                if (Regex.IsMatch(formattedDate1, @"^\d{2}\.\d{2}\.\d{4}$"))
                {
                    DateTime date = DateTime.ParseExact(TB4.Text, "dd.MM.yyyy", null);
                    formattedDate1 = date.ToString("yyyy-MM-dd");
                }
                if (TB1.Text.Count(c => c == ' ') != 2)
                {
                    MessageBox.Show("Incorrectly entered Name", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (long.TryParse(TB2.Text, out result) == false)
                {
                    MessageBox.Show("Please enter a number in Phone number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt64(TB2.Text) < 0)
                {
                    MessageBox.Show("Please enter a positive number in Phone number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (TB2.Text.Length != 10)
                {
                    MessageBox.Show("Incorrectly entered Phone number", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Regex.IsMatch(TB3.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") == false)
                {
                    MessageBox.Show("Incorrectly entered Email", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB1.Text == "Sex")
                {
                    MessageBox.Show("Incorrectly entered Gender", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Regex.IsMatch(formattedDate1, @"^\d{4}-\d{2}-\d{2}$") == false)
                {
                    MessageBox.Show("Incorrectly entered Birthday", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Math.Abs(Convert.ToInt64(formattedDateNow) - Convert.ToInt64(DateTime.ParseExact(TB4.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd"))) < 00180000)
                {
                    MessageBox.Show("It is not possible to register someone under the age of 18", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (TB7.Text.Length != 9)
                {
                    MessageBox.Show("Incorrectly entered Passport", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (TB8.Text.Length != 10)
                {
                    MessageBox.Show("Incorrectly entered RNOCPP", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB2.Text == "Motor show")
                {
                    MessageBox.Show("Incorrectly entered Motor show", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (k == 0)
                    {
                        int n = 0;
                        List<Employees> ts = EmployeesDA.RetrieveEmployees();
                        foreach (Employees t in ts)
                        {
                            if (t.RNOCPP1.ToString() == TB8.Text)
                            {
                                n = 1;
                            }
                            else if (t.Passport.ToString() == TB7.Text)
                            {
                                n = 2;
                            }
                        }
                        if (n == 1)
                        {
                            MessageBox.Show("An employee with such RNOCPP already exists", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                        if (n == 2)
                        {
                            MessageBox.Show("An employee with such Passport already exists", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }

                        int i = 0;
                        int j = 0;
                        List<Employees> sa = EmployeesDA.RetrieveEmployees();
                        foreach (Employees u in sa)
                        {
                            i += 1;
                        }
                        EmployeesDA.AddEmployee("false", TB1.Text, TB2.Text, TB3.Text, CB1.Text, formattedDate1, TB5.Text, TB6.Text, TB7.Text, TB8.Text, CB2.Text);
                        sa = EmployeesDA.RetrieveEmployees();
                        foreach (Employees u in sa)
                        {
                            j += 1;
                        }
                        if (j <= i)
                        {
                            MessageBox.Show("Error adding a new employee", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("A new employee has been added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            AddEWind.Close();
                        }
                    gt1:;
                    }
                    else if (k == 1)
                    {
                        EmployeesDA.UpdateEmployee(CR.Id.ToString(), TB1.Text, TB2.Text, TB3.Text, CB1.Text, formattedDate1, TB5.Text, TB6.Text, TB7.Text, TB8.Text, CB2.Text);
                        MessageBox.Show("The employee has been changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
            AddEWind.Close();
        }
    }
}