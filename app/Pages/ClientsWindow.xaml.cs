using Kursova.Add_windows;
using Kursova.Classes;
using Kursova.DA_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MySql.Data.MySqlClient;
using Kursova.Helper;
using Kursova.View_Layer;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kursova.Pages
{
    /// <summary>
    /// Interaction logic for ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : UserControl
    {
        private List<Clients> CR;
        private Users user;
        private ObservableCollection<Clients> DeletedCR;
        private List<Cars> DR1;
        private List<Sales> DR2;
        private Style originalButtonStyle;
        public string[] Names0 { get; set; }
        public string[] Names1 { get; set; }
        public string[] Names2 { get; set; }
        public ClientsWindow(Users user)
        {
            InitializeComponent();
            CR = ClientsDA.RetrieveClients();
            dtGrid.ItemsSource = CR;
            CB0.SelectionChanged += CB_Changed;
            DR1 = new List<Cars>();
            DR2 = new List<Sales>();
            originalButtonStyle = btnInfo.Style;
            DeletedCR = new ObservableCollection<Clients>();
            this.user = user;
            if (user.Role == "Salesperson")
            {
                CB0.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;
            }
            Bor1.Visibility = Visibility.Collapsed;
            filterColumn.Width = new GridLength(0);
            FilterUpdate();
        }
        private void FilterUpdate()
        {
            int i = 0;
            Names0 = new string[] { "Available clients", "Deleted clients" };
            CB0.SelectedIndex = 0;

            Names1 = new string[] { "Owner", "Buyer" };

            Names2 = new string[] { "Male", "Female" };

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
                    dtGrid.ItemsSource = ClientsDA.RetrieveClientsWithParameters(CB1.Text, CB2.Text, formattedDate1, formattedDate2);
                    DataContext = this;
                gt1:;
                }
                else
                {
                    MessageBox.Show("The filter when selecting Deleted clients is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                Year1.Text = "from";
                Year2.Text = "to";
                Year1.SelectedDate = null;
                Year2.SelectedDate = null;
                dtGrid.ItemsSource = ClientsDA.SearchClients(s);
            }
            else
            {
                MessageBox.Show("The Search when selecting Deleted clients is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                Clients c = (Clients)dtGrid.SelectedItem;
                AddClientWindow win = new AddClientWindow(c, user);
                win.Show();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    AddClientWindow win = new AddClientWindow();
                    win.Show();
                }
                if (CB0.SelectedIndex == 1)
                {
                    Clients c = (Clients)dtGrid.SelectedItem;
                    ClientsDA.AddClient(c.Id.ToString(), c.Full_name, c.Phone.ToString(), c.Sex, c.Birthday, c.Address, c.RNOCPP1.ToString());
                    Sales b = new Sales(0, "", 0, "", 0, 0, "", 0, 0,"", 0);
                    if (DR2 != null)
                    {
                        foreach (Sales i in DR2)
                        {
                            if (i.Clientid == c.Id)
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
                    Clients c = (Clients)dtGrid.SelectedItem;
                    int i = 0;
                    int j = 0;
                    List<Clients> y = ClientsDA.RetrieveClients();
                    foreach (Clients s in y)
                    {
                        i += 1;
                    }
                    List <Sales> b = SalesDA.RetrieveSalesWithClientId(c.Id.ToString());
                    if (b != null)
                    {
                        foreach (Sales s in b)
                        {
                            DR2.Add(s);
                        }
                    }
                    CR = ClientsDA.DeleteClients(c.Id.ToString());
                    y = ClientsDA.RetrieveClients();
                    foreach (Clients s in y)
                    {
                        j += 1;
                    }
                    if (j >= i)
                    {
                        MessageBox.Show("If the customer has a car, remove them first", "Hint", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        DeletedCR.Add(c);
                        dtGrid.ItemsSource = CR;
                    }
                    
                }
                if (CB0.SelectedIndex == 1)
                {
                    Clients c = (Clients)dtGrid.SelectedItem;
                    if (DR1 != null)
                    {
                        foreach (Cars i in DR1)
                        {
                            if (i.Owner == c.Full_name)
                            {
                                DR1.Remove(i);
                            }
                        }
                    }
                    if (DR2 != null)
                    {
                        foreach (Sales i in DR2)
                        {
                            if (i.Clientid == c.Id)
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
                    CR = ClientsDA.RetrieveClients();
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
                    Year1.Text = "from";
                    Year2.Text = "to";
                    Year1.SelectedDate = null;
                    Year2.SelectedDate = null;
                    CR = ClientsDA.RetrieveClients();
                    dtGrid.ItemsSource = CR;
                }
                else
                {
                    MessageBox.Show("The filter when selecting Deleted clients is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}