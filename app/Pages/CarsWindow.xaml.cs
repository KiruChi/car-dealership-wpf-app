using System;
using System.Collections.Generic;
using System.Data;
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
using Kursova.Classes;
using Kursova.DA_Layer;
using Kursova.Helper;
using Kursova.View_Layer;
using System.Collections.ObjectModel;
using Kursova.Add_windows;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

namespace Kursova.Pages
{
    /// <summary>
    /// Interaction logic for CarsWindow.xaml
    /// </summary>
    public partial class CarsWindow : UserControl
    {
        private List<Cars> CR;
        private Users user;
        private ObservableCollection<Cars> DeletedCR;
        private List<Bodys> DR1;
        private List<Classes.Colors> DR2;
        private List<Brands_models> DR3;
        private List<Materials> DR4;
        private List<Transmissions> DR5;
        private List<Fuels> DR6;
        private List<Clients> DR7;
        private List<Branches> DR8;
        private List<Sales> DR9;
        private List<Checks> DR10;
        private Style originalButtonStyle;
        public string[] Names0 { get; set; }
        public string[] Names1 { get; set; }
        public string[] Names2 { get; set; }
        public string[] Names3 { get; set; }
        public string[] Names4 { get; set; }
        public string[] Names5 { get; set; }
        public string[] Names6 { get; set; }
        public string[] Names7 { get; set; }
        public string[] Names8 { get; set; }
        public string[] Names9 { get; set; }
        public string[] Names10 { get; set; }
        public string[] Names11 { get; set; }
        public string[] Names12 { get; set; }
        public string[] Names13 { get; set; }
        public string[] Names14 { get; set; }
        public string[] Names15 { get; set; }
        public string[] Names16 { get; set; }
        public string[] Names17 { get; set; }
        public CarsWindow(Users user)
        {
            InitializeComponent();
            CR = CarsDA.RetrieveCars();
            dtGrid.ItemsSource = CR;
            CB0.SelectionChanged += CB_Changed;
            CB3.SelectionChanged += CB1_Changed;
            DR9 = new List<Sales>();
            DR10 = new List<Checks>();
            originalButtonStyle = btnInfo.Style;
            DeletedCR = new ObservableCollection<Cars>();
            this.user = user;
            if (user.Role == "Salesperson")
            {
                CB0.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;
                btnInfo.Margin = new Thickness(0, 0, 350, 0);
                btnAdd.Margin = new Thickness(0, 0, 0,0);
                btnBuy.Margin = new Thickness(350, 0, 0, 0);
            }
            Bor1.Visibility = Visibility.Collapsed;
            filterColumn.Width = new GridLength(0);
            FilterUpdate();
        }
        private void FilterUpdate()
        {
            int i = 0;
            Names0 = new string[] { "Available cars", "Deleted cars" };
            CB0.SelectedIndex = 0;
            DR1 = BodysDA.RetrieveBodys();
            i = DR1.Count();
            Names1 = new string[i];
            i = 0;
            foreach (Bodys c in DR1)
            {
                Names1[i] = c.Name;
                i += 1;
            }

            DR2 = ColorsDA.RetrieveColors();
            i = DR2.Count();
            Names2 = new string[i];
            i = 0;
            foreach (Classes.Colors c in DR2)
            {
                Names2[i] = c.Name;
                i += 1;
            }

            DR3 = Brands_modelsDA.RetrieveBrands_models();
            i = DR3.Count();
            Names3 = new string[i];
            Names4 = new string[i];
            i = 0;
            int j;
            foreach (Brands_models c in DR3)
            {
                j = 0;
                foreach (var el in Names3)
                {
                    if (el == c.Brand)
                    {
                        j = 1;
                    }
                }
                if (j == 0)
                {
                    Names3[i] = c.Brand;
                    i += 1;
                }
                else
                {
                    Names3[i] = null;
                    i += 1;
                }
            }
            List<string> list = new List<string>(Names3);
            list.RemoveAll(item => item == null);
            Names3 = list.ToArray();


            DR4 = MaterialsDA.RetrieveMaterials();
            i = DR4.Count();
            Names5 = new string[i];
            i = 0;
            foreach (Materials c in DR4)
            {
                Names5[i] = c.Name;
                i += 1;
            }

            DR5 = TransmissionsDA.RetrieveTransmissions();
            i = DR5.Count();
            Names6 = new string[i];
            i = 0;
            foreach (Transmissions c in DR5)
            {
                Names6[i] = c.Name;
                i += 1;
            }

            DR6 = FuelsDA.RetrieveFuels();
            i = DR6.Count();
            Names7 = new string[i];
            i = 0;
            foreach (Fuels c in DR6)
            {
                Names7[i] = c.Name;
                i += 1;
            }

            CR = CarsDA.RetrieveCars();
            i = CR.Count();
            Names8 = new string[i];
            i = 0;
            j = 0;
            foreach (Cars c in CR)
            {
                j = 0;
                foreach (var el in Names8)
                {
                    if (el == c.Engine)
                    {
                        j = 1;
                    }
                }
                if (j == 0)
                {
                    Names8[i] = c.Engine;
                    i += 1;
                }
                else
                {
                    Names8[i] = null;
                    i += 1;
                }
            }
            list = new List<string>(Names8);
            list.RemoveAll(item => item == null);
            Names8 = list.ToArray();

            Names9 = new string[] { "AWD", "Front", "Rear", "Full" };

            Names10 = new string[] { "Yes", "No" };

            Names11 = new string[] { "Yes", "No" };

            Names12 = new string[] { "Factory", "Custom" };

            Names13 = new string[] { "Street", "Off-road", "Sport", "All-purpose" };

            DR7 = ClientsDA.RetrieveClients();
            i = DR7.Count();
            Names14 = new string[i];
            i = 0;
            foreach (Clients c in DR7)
            {
                Names14[i] = c.Full_name;
                i += 1;
            }

            DR8 = BranchesDA.RetrieveMotor_shows();
            i = DR8.Count();
            Names15 = new string[i];
            i = 0;
            foreach (Branches c in DR8)
            {
                Names15[i] = c.Name;
                i += 1;
            }

            Names16 = new string[] { "Available", "Coming Soon", "Sold" };
            Names17 = new string[] {"A", "B", "C", "D", "E", "F", "S", "M", "J" };

            DataContext = this;
        }
        private void btnAplly_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    int result;
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
                    if (Seats.Text != "Seats")
                    {
                        if (int.TryParse(Seats.Text, out result) == false)
                        {
                            MessageBox.Show("Please enter a number in Seats", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                        else if (Convert.ToInt32(Seats.Text) < 0)
                        {
                            MessageBox.Show("Please enter a positive number in Seats", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                    }
                    if (Cost1.Text != "from")
                    {
                        if (int.TryParse(Cost1.Text, out result) == false)
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
                        if (int.TryParse(Cost2.Text, out result) == false)
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
                        if (Convert.ToInt64(DateTime.ParseExact(Year2.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd")) < Convert.ToInt64(DateTime.ParseExact(Year1.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd")))
                        {
                            MessageBox.Show("The date \"from\" cannot be greater than the date \"to\" in Date", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                    }
                    if (Mileage1.Text != "from")
                    {
                        if (int.TryParse(Mileage1.Text, out result) == false)
                        {
                            MessageBox.Show("Please enter a number in Mileage", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                        else if (Convert.ToInt32(Mileage1.Text) < 0)
                        {
                            MessageBox.Show("Please enter a positive number in Mileage", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                    }
                    if (Mileage2.Text != "to")
                    {
                        if (int.TryParse(Mileage2.Text, out result) == false)
                        {
                            MessageBox.Show("Please enter a number in Mileage", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                        else if (Convert.ToInt32(Mileage2.Text) < 0)
                        {
                            MessageBox.Show("Please enter a positive number in Mileage", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }
                    }
                    if (Mileage1.Text != "from" && Mileage2.Text != "to")
                    {
                        if (Convert.ToInt32(Mileage1.Text) > Convert.ToInt32(Mileage2.Text))
                        {
                            MessageBox.Show("The number \"from\" cannot be greater than the number \"to\" in Mileage", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                            return;
                        }
                    }
                    dtGrid.ItemsSource = CarsDA.RetrieveCarsWithParameters(CB1.Text, CB2.Text, CB3.Text, CB4.Text, CB5.Text, CB6.Text, Cost1.Text, Cost2.Text, formattedDate1, formattedDate2, 
                    Mileage1.Text, Mileage2.Text, CB7.Text, CB8.Text, CB9.Text, Seats.Text, CB10.Text, CB11.Text, CB12.Text, CB13.Text, CB14.Text, CB15.Text, CB16.Text, CB17.Text);
                    DataContext = this;
                gt1:;
                }
                else
                {
                    MessageBox.Show("The filter when selecting Deleted cars is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                CB1.Text = "Body";
                CB2.Text = "Color";
                CB3.Text = "Brand";
                CB4.Text = "Model";
                CB17.Text = "Class";
                CB5.Text = "Interior material";
                CB6.Text = "Transmission";
                CB7.Text = "Fuel";
                CB8.Text = "Engine";
                CB9.Text = "Car drive";
                CB10.Text = "Сar heat";
                CB11.Text = "Air conditioner";
                CB12.Text = "Discs";
                CB13.Text = "Rubber";
                CB14.Text = "Owner";
                CB15.Text = "Motor show";
                CB16.Text = "Status";
                Seats.Text = "Seats";
                Year1.Text = "from";
                Year2.Text = "to";
                Year1.SelectedDate = null;
                Year2.SelectedDate = null;
                Cost1.Text = "from";
                Cost2.Text = "to";
                Mileage1.Text = "from";
                Mileage2.Text = "to";
                Names4 = new string[2];
                CB4.ItemsSource = Names4;
                dtGrid.ItemsSource = CarsDA.SearchCars(s);
            }
            else
            {
                MessageBox.Show("The Search when selecting Deleted cars is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                Cars car = (Cars)dtGrid.SelectedItem;
                AddCarWindow addCarWindow = new AddCarWindow(car, user);
                addCarWindow.Show();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    AddCarWindow addCarWindow = new AddCarWindow();
                    addCarWindow.Show();
                }
                if (CB0.SelectedIndex == 1)
                {
                    Cars car = (Cars)dtGrid.SelectedItem;
                    CarsDA.AddCar(car.Id.ToString(), car.Name, car.Brand, car.Model, car.Clas, car.Color, car.Body, car.Material, car.Kpp, car.Year, car.Mileage.ToString(), car.Fuel, car.Engine, car.Drive, car.Seats.ToString(), car.Heat,
                    car.Air_cond, car.Discs, car.Rubber, car.Owner, car.Vin_code, car.Type_sale, car.Motor_show, car.Status);
                    Sales a = new Sales(0, "", 0, "", 0, 0, "", 0, 0, "", 0);
                    Checks b = new Checks(0, 0, 0, "", 0, 0);
                    if (DR9 != null)
                    {
                        foreach (Sales i in DR9)
                        {
                            if (i.Carid == car.Id)
                            {
                                a = i;
                            }
                        }
                    }
                    if (DR10 != null)
                    {
                        foreach (Checks i in DR10)
                        {
                            if (i.Carid == car.Id)
                            {
                                b = i;
                            }
                        }
                    }
                    if (a != null)
                    {
                        SalesDA.AddSale(a.Id.ToString(), a.Carid.ToString(), a.Clientid.ToString(), a.Employeeid.ToString(), a.Date, a.Price.ToString());
                    }
                    if (b != null)
                    {
                        ChecksDA.AddCheck(b.Id.ToString(), b.Carid.ToString(), b.Service_centerid.ToString(), b.Date, b.Check_price.ToString(), b.Car_price.ToString());
                    }
                    DeletedCR.Remove(car);
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
                    Cars car = (Cars)dtGrid.SelectedItem;
                    DeletedCR.Add(car);
                    Sales a = SalesDA.RetrieveSalesWithCarId(car.Id.ToString());
                    if (a.Id != null && a.Id.ToString() != "")
                    {
                        DR9.Add(a);
                    }
                    Checks b = ChecksDA.RetrieveChecksWithCarId(car.Id.ToString());
                    if (b.Id != null && b.Id.ToString() != "")
                    {
                        DR10.Add(b);
                    }
                    CR = CarsDA.DeleteCar(car.Id.ToString());
                    dtGrid.ItemsSource = CR;
                }
                if (CB0.SelectedIndex == 1)
                {
                    Cars car = (Cars)dtGrid.SelectedItem;
                    if (DR9 != null)
                    {
                        foreach (Sales i in DR9)
                        {
                            if (i.Carid == car.Id)
                            {
                                DR9.Remove(i);
                            }
                        }
                    }
                    if (DR10 != null)
                    {
                        foreach (Checks i in DR10)
                        {
                            if (i.Carid == car.Id)
                            {
                                DR10.Remove(i);
                            }
                        }
                    }
                    DeletedCR.Remove(car);
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
                    CR = CarsDA.RetrieveCars();
                    dtGrid.ItemsSource = CR;
                    btnInfo.Visibility = Visibility.Visible;
                    btnBuy.Visibility = Visibility.Visible;
                    btnAdd.Content = "Add";
                }
                if (CB0.SelectedIndex == 1)
                {
                    dtGrid.ItemsSource = DeletedCR;
                    btnInfo.Visibility = Visibility.Hidden;
                    btnBuy.Visibility = Visibility.Hidden;
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
                    CB1.Text = "Body";
                    CB2.Text = "Color";
                    CB3.Text = "Brand";
                    CB4.Text = "Model";
                    CB17.Text = "Class";
                    CB5.Text = "Interior material";
                    CB6.Text = "Transmission";
                    CB7.Text = "Fuel";
                    CB8.Text = "Engine";
                    CB9.Text = "Car drive";
                    CB10.Text = "Сar heat";
                    CB11.Text = "Air conditioner";
                    CB12.Text = "Discs";
                    CB13.Text = "Rubber";
                    CB14.Text = "Owner";
                    CB15.Text = "Motor show";
                    CB16.Text = "Status";
                    Seats.Text = "Seats";
                    Year1.Text = "from";
                    Year2.Text = "to";
                    Year1.SelectedDate = null;
                    Year2.SelectedDate = null;
                    Cost1.Text = "from";
                    Cost2.Text = "to";
                    Mileage1.Text = "from";
                    Mileage2.Text = "to";
                    Names4 = new string[2];
                    CB4.ItemsSource = Names4;
                    CR = CarsDA.RetrieveCars();
                    dtGrid.ItemsSource = CR;
                }
                else
                {
                    MessageBox.Show("The filter when selecting Deleted cars is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void CB1_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (CB3.SelectedValue != null)
            {
                string selectedBrand = CB3.SelectedValue.ToString();
                DR3 = Brands_modelsDA.RetrieveModels(selectedBrand);
                int i = DR3.Count();
                Names4 = new string[i];
                i = 0;
                foreach (Brands_models c in DR3)
                {
                    Names4[i] = c.Model;
                    i += 1;
                }
                CB4.ItemsSource = Names4;
            }
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (dtGrid.SelectedItem != null)
            {
                Cars car = (Cars)dtGrid.SelectedItem;
                if (car.Status != "Sold")
                {
                    BuyCarWindow b = new BuyCarWindow(car);
                    b.Show();
                }
                else
                {
                    MessageBox.Show("This car has already been sold", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }
    }
}
