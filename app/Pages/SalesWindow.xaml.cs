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

namespace Kursova.Pages
{
    /// <summary>
    /// Interaction logic for SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : UserControl
    {
        private List<Sales> CR;
        private ObservableCollection<Sales> DeletedCR;
        private List<Clients> DR1;
        private List<Employees> DR2;
        private Style originalButtonStyle;
        public string[] Names0 { get; set; }
        public string[] Names1 { get; set; }
        public string[] Names2 { get; set; }
        public SalesWindow()
        {
            InitializeComponent();
            CR = SalesDA.RetrieveSales();
            int S1 = 0;
            long S2 = 0;
            foreach (Sales x in CR)
            {
                S1 += 1;
                S2 += x.Price;

            }
            TB1.Text = S1.ToString();
            TB2.Text = S2.ToString();
            dtGrid.ItemsSource = CR;
            CB0.SelectionChanged += CB_Changed;
            originalButtonStyle = btnInfo.Style;
            DeletedCR = new ObservableCollection<Sales>();
            Bor1.Visibility = Visibility.Collapsed;
            filterColumn.Width = new GridLength(0);
            FilterUpdate();
        }
        private void FilterUpdate()
        {
            int i = 0;
            Names0 = new string[] { "Available sales", "Deleted sales" };
            CB0.SelectedIndex = 0;

            DR1 = ClientsDA.RetrieveClients();
            i = DR1.Count();
            Names1 = new string[i];
            i = 0;
            foreach (Clients c in DR1)
            {
                Names1[i] = c.Full_name;
                i += 1;
            }

            DR2 = EmployeesDA.RetrieveEmployees();
            i = DR2.Count();
            Names2 = new string[i];
            i = 0;
            foreach (Employees c in DR2)
            {
                Names2[i] = c.Full_name;
                i += 1;
            }

            DataContext = this;
        }
        private void btnAplly_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    string formattedDate1 = Date1.Text;
                    if (Regex.IsMatch(formattedDate1, @"^\d{2}\.\d{2}\.\d{4}$"))
                    {
                        DateTime date = DateTime.ParseExact(Date1.Text, "dd.MM.yyyy", null);
                        formattedDate1 = date.ToString("yyyy-MM-dd");
                    }
                    string formattedDate2 = Date2.Text;
                    if (Regex.IsMatch(formattedDate2, @"^\d{2}\.\d{2}\.\d{4}$"))
                    {
                        DateTime date = DateTime.ParseExact(Date2.Text, "dd.MM.yyyy", null);
                        formattedDate2 = date.ToString("yyyy-MM-dd");
                    }
                    if (Cost1.Text != "from")
                    {
                        if (int.TryParse(Cost1.Text, out int result) == false)
                        {
                            MessageBox.Show("Please enter a number in cost", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                        else if (Convert.ToInt32(Cost1.Text) < 0)
                        {
                            MessageBox.Show("Please enter a positive number in cost", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                    }
                    if (Cost2.Text != "to")
                    {
                        if (int.TryParse(Cost2.Text, out int result) == false)
                        {
                            MessageBox.Show("Please enter a number in cost", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                        else if (Convert.ToInt32(Cost2.Text) < 0)
                        {
                            MessageBox.Show("Please enter a positive number in cost", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                    }
                    if (Cost1.Text != "from" && Cost2.Text != "to")
                    {
                        if (Convert.ToInt32(Cost1.Text) > Convert.ToInt32(Cost2.Text))
                        {
                            MessageBox.Show("The number \"from\" cannot be greater than the number \"to\" in cost", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
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
                        if (Convert.ToInt64(DateTime.ParseExact(Date2.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd")) < Convert.ToInt64(DateTime.ParseExact(Date1.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd")))
                        {
                            MessageBox.Show("The date \"from\" cannot be greater than the date \"to\" in Date", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                    }
                    CR = SalesDA.RetrieveSalesWithParameters(CB1.Text, CB2.Text, formattedDate1, formattedDate2, Cost1.Text, Cost2.Text);
                    dtGrid.ItemsSource = CR;
                    int S1 = 0;
                    long S2 = 0;
                    foreach (Sales x in CR)
                    {
                        S1 += 1;
                        S2 += x.Price;

                    }
                    TB1.Text = S1.ToString();
                    TB2.Text = S2.ToString();
                    DataContext = this;
                gt1:;
                }
                else
                {
                    MessageBox.Show("The filter when selecting Deleted sales is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                CR = SalesDA.SearchSales(s);
                int S1 = 0;
                long S2 = 0;
                foreach (Sales x in CR)
                {
                    S1 += 1;
                    S2 += x.Price;

                }
                TB1.Text = S1.ToString();
                TB2.Text = S2.ToString();
                dtGrid.ItemsSource = SalesDA.SearchSales(s);
            }
            else
            {
                MessageBox.Show("The Search when selecting Deleted sales is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                Sales c = (Sales)dtGrid.SelectedItem;
                AddSaleWindow win = new AddSaleWindow(c);
                win.Show();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    AddSaleWindow win = new AddSaleWindow();
                    win.Show();
                }
                if (CB0.SelectedIndex == 1)
                {
                    Sales c = (Sales)dtGrid.SelectedItem;
                    SalesDA.AddSale(c.Id.ToString(), c.Carid.ToString(), c.Clientid.ToString(), c.Employeeid.ToString(), c.Date, c.Price.ToString());
                    CarsDA.UpdateCarStatus(c.Carid.ToString(), "Sold");
                    DeletedCR.Remove(c);
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    Sales c = (Sales)dtGrid.SelectedItem;
                    DeletedCR.Add(c);
                    CR = SalesDA.DeleteSale(c.Id.ToString(), c.Carid.ToString());
                    int S1 = 0;
                    long S2 = 0;
                    foreach (Sales x in CR)
                    {
                        S1 += 1;
                        S2 += x.Price;

                    }
                    TB1.Text = S1.ToString();
                    TB2.Text = S2.ToString();
                    dtGrid.ItemsSource = CR;
                }
                if (CB0.SelectedIndex == 1)
                {
                    Sales c = (Sales)dtGrid.SelectedItem;
                    DeletedCR.Remove(c);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void CB_Changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    CR = SalesDA.RetrieveSales();
                    dtGrid.ItemsSource = CR;
                    int S1 = 0;
                    long S2 = 0;
                    foreach (Sales x in CR)
                    {
                        S1 += 1;
                        S2 += x.Price;

                    }
                    TB1.Text = S1.ToString();
                    TB2.Text = S2.ToString();
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
                    CB1.Text = "Client name";
                    CB2.SelectedItem = null;
                    CB2.Text = "Employee name";
                    Date1.Text = "from";
                    Date2.Text = "to";
                    Date1.SelectedDate = null;
                    Date2.SelectedDate = null;
                    Cost1.Text = "from";
                    Cost2.Text = "to";
                    CR = SalesDA.RetrieveSales();
                    int S1 = 0;
                    long S2 = 0;
                    foreach (Sales x in CR)
                    {
                        S1 += 1;
                        S2 += x.Price;

                    }
                    TB1.Text = S1.ToString();
                    TB2.Text = S2.ToString();
                    dtGrid.ItemsSource = CR;
                }
                else
                {
                    MessageBox.Show("The filter when selecting Deleted sales is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}