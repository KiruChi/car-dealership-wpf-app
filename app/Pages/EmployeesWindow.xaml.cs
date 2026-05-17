using Kursova.Add_windows;
using Kursova.Classes;
using Kursova.DA_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kursova.Pages
{
    /// <summary>
    /// Interaction logic for EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : UserControl
    {
        private List<Employees> CR;
        private ObservableCollection<Employees> DeletedCR;
        private List<Branches> DR1;
        private List<Sales> DR2;
        private Style originalButtonStyle;
        public string[] Names0 { get; set; }
        public string[] Names1 { get; set; }
        public string[] Names2 { get; set; }
        public string[] Names3 { get; set; }
        public EmployeesWindow()
        {
            InitializeComponent();
            CR = EmployeesDA.RetrieveEmployees();
            dtGrid.ItemsSource = CR;
            CB0.SelectionChanged += CB_Changed;
            DR2 = new List<Sales>();
            originalButtonStyle = btnInfo.Style;
            DeletedCR = new ObservableCollection<Employees>();
            Bor1.Visibility = Visibility.Collapsed;
            filterColumn.Width = new GridLength(0);
            FilterUpdate();
        }
        private void FilterUpdate()
        {
            int i = 0;
            Names0 = new string[] { "Available employees", "Deleted employees" };
            CB0.SelectedIndex = 0;

            DR1 = BranchesDA.RetrieveMotor_shows();
            i = DR1.Count();
            Names1 = new string[i];
            i = 0;
            foreach (Branches c in DR1)
            {
                Names1[i] = c.Name;
                i += 1;
            }

            CR = EmployeesDA.RetrieveEmployees();
            i = CR.Count();
            Names2 = new string[i];
            i = 0;
            int j;
            foreach (Employees c in CR)
            {
                j = 0;
                foreach (var el in Names2)
                {
                    if (el == c.Post)
                    {
                        j = 1;
                    }
                }
                if (j == 0)
                {
                    Names2[i] = c.Post;
                    i += 1;
                }
                else
                {
                    Names2[i] = null;
                    i += 1;
                }
            }
            List<string> list = new List<string>(Names2);
            list.RemoveAll(item => item == null);
            Names2 = list.ToArray();

            Names3 = new string[] { "Male", "Female" };

            DataContext = this;
        }
        private void btnAplly_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    string formattedDate1 = Year1.Text;
                    if (Regex.IsMatch(formattedDate1, @"^\d{2}\.\d{2}\.\d{4}$"))
                    {
                        DateTime date = DateTime.ParseExact(Year1.Text, "dd.MM.yyyy", null);
                        formattedDate1 = date.ToString("yyyy-MM-dd");
                    }
                    string formattedDate2 = Year2.Text;
                    if (Regex.IsMatch(formattedDate2, @"^\d{2}\.\d{2}\.\d{4}$"))
                    {
                        DateTime date = DateTime.ParseExact(Year2.Text, "dd.MM.yyyy", null);
                        formattedDate2 = date.ToString("yyyy-MM-dd");
                    }
                    if (Regex.IsMatch(formattedDate1, @"^\d{4}-\d{2}-\d{2}$") == false && formattedDate1 != "")
                    {
                        MessageBox.Show("Incorrectly entered Date", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                        goto gt1;
                    }
                    if (Regex.IsMatch(formattedDate2, @"^\d{4}-\d{2}-\d{2}$") == false && formattedDate2 != "")
                    {
                        MessageBox.Show("Incorrectly entered Date", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                        goto gt1;
                    }
                    if ((Regex.IsMatch(formattedDate2, @"^\d{4}-\d{2}-\d{2}$") && formattedDate2 != "") && (Regex.IsMatch(formattedDate1, @"^\d{4}-\d{2}-\d{2}$") && formattedDate1 != ""))
                    {
                        if (Convert.ToInt64(DateTime.ParseExact(Year2.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd")) < Convert.ToInt64(DateTime.ParseExact(Year1.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd")))
                        {
                            MessageBox.Show("The date \"from\" cannot be greater than the date \"to\" in Date", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                    }
                    dtGrid.ItemsSource = EmployeesDA.RetrieveEmployeesWithParameters(CB1.Text, CB2.Text, CB3.Text, formattedDate1, formattedDate2);
                    DataContext = this;
                gt1:;
                }
                else
                {
                    MessageBox.Show("The filter when selecting Deleted employees is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void SearchC(string s)
        {
            if (CB0.SelectedIndex == 0)
            {
                DataContext = null;
                FilterUpdate();
                CB1.SelectedItem = null;
                CB2.SelectedItem = null;
                CB3.SelectedItem = null;
                Year1.Text = "from";
                Year2.Text = "to";
                Year1.SelectedDate = null;
                Year2.SelectedDate = null;
                dtGrid.ItemsSource = EmployeesDA.SearchEmployees(s);
            }
            else
            {
                MessageBox.Show("The Search when selecting Deleted employees is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void FilterC(string s)
        {
            if (Bor1.Visibility == Visibility.Collapsed)
            {
                Bor1.Visibility = Visibility.Visible;
                filterColumn.Width = new GridLength(300);
            }
            else if (Bor1.Visibility == Visibility.Visible)
            {
                Bor1.Visibility = Visibility.Collapsed;
                filterColumn.Width = new GridLength(0);
            }
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            if (dtGrid.SelectedItem != null)
            {
                Employees c = (Employees)dtGrid.SelectedItem;
                AddEmployeeWindow win = new AddEmployeeWindow(c);
                win.Show();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    AddEmployeeWindow win = new AddEmployeeWindow();
                    win.Show();
                }
                if (CB0.SelectedIndex == 1)
                {
                    Employees c = (Employees)dtGrid.SelectedItem;
                    EmployeesDA.AddEmployee(c.Id.ToString(), c.Full_name, c.Phone.ToString(), c.Email, c.Sex, c.Birthday, c.Post, c.Address, c.Passport.ToString(), c.RNOCPP1.ToString(), c.Motor_show);
                    Sales b = new Sales(0, "", 0, "", 0, 0, "", 0, 0, "", 0);
                    if (DR2 != null)
                    {
                        foreach (Sales i in DR2)
                        {
                            if (i.Employeeid == c.Id)
                            {
                                b = i;
                                SalesDA.AddSale(b.Id.ToString(), b.Carid.ToString(), b.Clientid.ToString(), b.Employeeid.ToString(), b.Date, b.Price.ToString());
                            }
                        }
                    }
                    DeletedCR.Remove(c);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    Employees c = (Employees)dtGrid.SelectedItem;
                    DeletedCR.Add(c);
                    List<Sales> b = SalesDA.RetrieveSalesWithEmployeeId(c.Id.ToString());
                    if (b != null)
                    {
                        foreach (Sales s in b)
                        {
                            DR2.Add(s);
                        }
                    }
                    CR = EmployeesDA.DeleteEmployee(c.Id.ToString());
                    dtGrid.ItemsSource = CR;
                }
                if (CB0.SelectedIndex == 1)
                {
                    Employees c = (Employees)dtGrid.SelectedItem;
                    if (DR2 != null)
                    {
                        foreach (Sales i in DR2)
                        {
                            if (i.Employeeid == c.Id)
                            {
                                DR2.Remove(i);
                            }
                        }
                    }
                    DeletedCR.Remove(c);
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void CB_Changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    CR = EmployeesDA.RetrieveEmployees();
                    dtGrid.ItemsSource = CR;
                    btnInfo.Visibility = Visibility.Visible;
                    btnDelete.Margin = new Thickness(0, 0, 0, 0);
                    btnAdd.Margin = new Thickness(350, 0, 0, 0);
                    btnAdd.Content = "Add";
                }
                if (CB0.SelectedIndex == 1)
                {
                    dtGrid.ItemsSource = DeletedCR;
                    btnInfo.Visibility = Visibility.Hidden;
                    btnDelete.Margin = new Thickness(0, 0, 200, 0);
                    btnAdd.Margin = new Thickness(200, 0, 0, 0);
                    btnAdd.Content = "Return";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnResetF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    CB1.SelectedItem = null;
                    CB2.SelectedItem = null;
                    CB3.SelectedItem = null;
                    Year1.Text = "from";
                    Year2.Text = "to";
                    Year1.SelectedDate = null;
                    Year2.SelectedDate = null;
                    CR = EmployeesDA.RetrieveEmployees();
                    dtGrid.ItemsSource = CR;
                }
                else
                {
                    MessageBox.Show("The filter when selecting Deleted employees is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}