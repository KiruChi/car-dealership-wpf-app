using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
using Kursova.Classes;
using Kursova.DA_Layer;
using Kursova.Helper;

namespace Kursova.Add_windows
{
    /// <summary>
    /// Interaction logic for AddCarWindow.xaml
    /// </summary>
    public partial class AddCarWindow : Window
    {
        private Cars CR;
        private Users user;
        private List<Bodys> DR1;
        private List<Classes.Colors> DR2;
        private List<Brands_models> DR3;
        private List<Materials> DR4;
        private List<Transmissions> DR5;
        private List<Fuels> DR6;
        private List<Clients> DR7;
        private List<Branches> DR8;
        private List<Branches> DR9;
        private int k = 0;
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
        public string[] Names18 { get; set; }
        public string[] Names19 { get; set; }
        public AddCarWindow()
        {
            InitializeComponent();
            Initialize();
            Inf.Text = "Add";
            CB3.SelectionChanged += CB1_Changed;
            k = 0;
        }
        public AddCarWindow(Cars car, Users user)
        {
            try
            {
                InitializeComponent();
                CR = car;
                TB1.Text = car.Name;
                CB1.Text = car.Body;
                CB2.Text = car.Color;
                CB3.Text = car.Brand;
                CB4.Text = car.Model;
                CB19.Text = car.Clas;
                CB5.Text = car.Material;
                CB6.Text = car.Kpp;
                TB3.Text = car.Year;
                TB4.Text = car.Mileage.ToString();
                CB7.Text = car.Fuel;
                TB5.Text = car.Engine;
                CB9.Text = car.Drive;
                TB6.Text = car.Seats.ToString();
                CB10.Text = car.Heat;
                CB11.Text = car.Air_cond;
                CB12.Text = car.Discs;
                CB13.Text = car.Rubber;
                CB14.Text = car.Owner;
                TB7.Text = car.Vin_code;
                CB17.Text = car.Type_sale;
                CB15.Text = car.Motor_show;
                CB16.Text = car.Status;
                TB2.Text = car.Price.ToString();
                CB18.Text = BranchesDA.RetrieveService_centersWithCarId(car.Id.ToString()).Name;
                Checks a = ChecksDA.RetrieveChecksWithCarId(car.Id.ToString());
                TB9.Text = a.Date;
                TB10.Text = a.Check_price.ToString();
                Initialize();
                CB3.SelectionChanged += CB1_Changed;
                DR3 = Brands_modelsDA.RetrieveModels(CB3.Text);
                int i = DR3.Count();
                Names4 = new string[i];
                i = 0;
                foreach (Brands_models c in DR3)
                {
                    Names4[i] = c.Model;
                    i += 1;
                }
                CB4.ItemsSource = Names4;
                k = 1;
            }
            catch
            {
                MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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

            DR1 = BodysDA.RetrieveBodys();
            i = DR1.Count();
            Names1 = new String[i];
            i = 0;
            foreach (Bodys c in DR1)
            {
                Names1[i] = c.Name;
                i += 1;
            }

            DR2 = ColorsDA.RetrieveColors();
            i = DR2.Count();
            Names2 = new String[i];
            i = 0;
            foreach (Classes.Colors c in DR2)
            {
                Names2[i] = c.Name;
                i += 1;
            }

            DR3 = Brands_modelsDA.RetrieveBrands_models();
            i = DR3.Count();
            Names3 = new String[i];
            Names4 = new String[i];
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
            Names5 = new String[i];
            i = 0;
            foreach (Materials c in DR4)
            {
                Names5[i] = c.Name;
                i += 1;
            }

            DR5 = TransmissionsDA.RetrieveTransmissions();
            i = DR5.Count();
            Names6 = new String[i];
            i = 0;
            foreach (Transmissions c in DR5)
            {
                Names6[i] = c.Name;
                i += 1;
            }

            DR6 = FuelsDA.RetrieveFuels();
            i = DR6.Count();
            Names7 = new String[i];
            i = 0;
            foreach (Fuels c in DR6)
            {
                Names7[i] = c.Name;
                i += 1;
            }


            Names9 = new String[] { "AWD", "Front", "Rear", "Full" };

            Names10 = new String[] { "Yes", "No" };

            Names11 = new String[] { "Yes", "No" };

            Names12 = new String[] { "Factory", "Custom" };

            Names13 = new String[] { "Street", "Off-road", "Sport", "All-purpose" };

            DR7 = ClientsDA.RetrieveClients();
            i = DR7.Count();
            Names14 = new String[i];
            i = 0;
            foreach (Clients c in DR7)
            {
                Names14[i] = c.Full_name;
                i += 1;
            }

            DR8 = BranchesDA.RetrieveMotor_shows();
            i = DR8.Count();
            Names15 = new String[i];
            i = 0;
            foreach (Branches c in DR8)
            {
                Names15[i] = c.Name;
                i += 1;
            }

            Names16 = new String[] { "Available", "Coming Soon", "Sold" };

            Names17 = new String[] { "Purchase", "Regular"};

            DR9 = BranchesDA.RetrieveService_centers(); 
            i = DR9.Count();
            Names18 = new String[i];
            i = 0;
            foreach (Branches c in DR9)
            {
                Names18[i] = c.Name;
                i += 1;
            }
            Names19 = new String[] { "A", "B", "C", "D", "E", "F", "S", "M", "J" };
            DataContext = this;
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
            AddCWin.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                string formattedDateNow = now.ToString("yyyyMMdd");
                long result;
                int result1;
                string formattedDate1 = TB3.Text;
                if (Regex.IsMatch(formattedDate1, @"^\d{2}\.\d{2}\.\d{4}$"))
                {
                    DateTime date = DateTime.ParseExact(TB3.Text, "dd.MM.yyyy", null);
                    formattedDate1 = date.ToString("yyyy-MM-dd");
                }
                string formattedDate3 = TB9.Text;
                if (Regex.IsMatch(formattedDate3, @"^\d{2}\.\d{2}\.\d{4}$"))
                {
                    DateTime date = DateTime.ParseExact(TB9.Text, "dd.MM.yyyy", null);
                    formattedDate3 = date.ToString("yyyy-MM-dd");
                }
                if (CB3.Text == "Brand")
                {
                    MessageBox.Show("Incorrectly entered Brand", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB4.Text == "Model")
                {
                    MessageBox.Show("Incorrectly entered Model", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB19.Text == "Class")
                {
                    MessageBox.Show("Incorrectly entered Class", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB1.Text == "Body")
                {
                    MessageBox.Show("Incorrectly entered Body", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB2.Text == "Color")
                {
                    MessageBox.Show("Incorrectly entered Color", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB5.Text == "Interior material")
                {
                    MessageBox.Show("Incorrectly entered Interior material", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB6.Text == "Transmission")
                {
                    MessageBox.Show("Incorrectly entered Transmission", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Regex.IsMatch(formattedDate1, @"^\d{4}-\d{2}-\d{2}$") == false)
                {
                    MessageBox.Show("Incorrectly entered Date", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt64(formattedDateNow) < Convert.ToInt64(DateTime.ParseExact(TB3.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd")))
                {
                    MessageBox.Show("The date cannot be greater than today's date in Date", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (long.TryParse(TB4.Text, out result) == false)
                {
                    MessageBox.Show("Please enter a number in Mileage", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt64(TB4.Text) < 0)
                {
                    MessageBox.Show("Please enter a positive number in Mileage", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB7.Text == "Fuel")
                {
                    MessageBox.Show("Incorrectly entered Fuel", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB9.Text == "Car drive")
                {
                    MessageBox.Show("Incorrectly entered Car drive", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (int.TryParse(TB6.Text, out result1) == false || TB6.Text == "")
                {
                    MessageBox.Show("Please enter a number in Num of seats", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt32(TB6.Text) < 0)
                {
                    MessageBox.Show("Please enter a positive number in Num of seats", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB10.Text == "Сar heat")
                {
                    MessageBox.Show("Incorrectly entered Сar heat", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB11.Text == "Air conditioner")
                {
                    MessageBox.Show("Incorrectly entered Air conditioner", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB12.Text == "Discs")
                {
                    MessageBox.Show("Incorrectly entered Discs", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB13.Text == "Rubber")
                {
                    MessageBox.Show("Incorrectly entered Rubber", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB14.Text == "Owner")
                {
                    MessageBox.Show("Incorrectly entered Owner", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (TB7.Text.Length != 17)
                {
                    MessageBox.Show("Incorrectly entered Vin code", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB17.Text == "Type of sale")
                {
                    MessageBox.Show("Incorrectly entered Type of sale", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB15.Text == "Motor show")
                {
                    MessageBox.Show("Incorrectly entered Motor show", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB16.Text == "Status")
                {
                    MessageBox.Show("Incorrectly entered Status", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (long.TryParse(TB2.Text, out result) == false || TB2.Text == "")
                {
                    MessageBox.Show("Incorrectly entered Generated price", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt64(TB2.Text) < 0)
                {
                    MessageBox.Show("Please enter a positive number in Generated price", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB18.Text == "Service center")
                {
                    MessageBox.Show("Incorrectly entered Service center", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Regex.IsMatch(formattedDate3, @"^\d{4}-\d{2}-\d{2}$") == false)
                {
                    MessageBox.Show("Incorrectly entered Check date", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt64(DateTime.ParseExact(TB9.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd")) < Convert.ToInt64(DateTime.ParseExact(TB3.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd")))
                {
                    MessageBox.Show("The date of Check cannot be less than the date of creation of the car", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (long.TryParse(TB10.Text, out result) == false || TB10.Text == "")
                {
                    MessageBox.Show("Incorrectly entered Check price", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt64(TB10.Text) < 0)
                {
                    MessageBox.Show("Please enter a positive number in Check price", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (k == 0)
                    {
                        int n = 0;
                        List<Cars> ts = CarsDA.RetrieveCars();
                        foreach (Cars t in ts)
                        {
                            if (t.Vin_code == TB7.Text)
                            {
                                n = 1;
                            }
                        }
                        if (n == 1)
                        {
                            MessageBox.Show("A car with such vin code already exists", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }

                        int i = 0;
                        int j = 0;
                        int x = 0;
                        int y = 0;
                        List<Cars> a = CarsDA.RetrieveCars();
                        foreach (Cars car in a)
                        {
                            i += 1;
                        }
                        List<Checks> b = ChecksDA.RetrieveChecks();
                        foreach (Checks checks in b)
                        {
                            j += 1;
                        }
                        CarsDA.AddCar("false", TB1.Text, CB3.Text, CB4.Text, CB19.Text, CB2.Text, CB1.Text, CB5.Text, CB6.Text, formattedDate1, TB4.Text, CB7.Text, TB5.Text, CB9.Text, TB6.Text, CB10.Text, CB11.Text,
                        CB12.Text, CB13.Text, CB14.Text, TB7.Text, CB17.Text, CB15.Text, CB16.Text);
                        string S = "";
                        a = CarsDA.RetrieveCarsAll();
                        foreach (Cars car in a)
                        {
                            S = car.Id.ToString();
                        }
                        ChecksDA.AddCheck("false", S, CB18.Text, formattedDate3, TB10.Text, TB2.Text);
                        b = ChecksDA.RetrieveChecks();
                        foreach (Checks checks in b)
                        {
                            y += 1;
                        }
                        a = CarsDA.RetrieveCars();
                        foreach (Cars car in a)
                        {
                            x += 1;
                        }
                        if (x <= i)
                        {
                            MessageBox.Show("Error adding a new vehicle", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (y <= j)
                        {
                            MessageBox.Show("Error adding a vehicle check", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("A new car has been added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            AddCWin.Close();
                        }
                    gt1:;
                    }
                    else if (k == 1)
                    {
                        CarsDA.UpdateCar(CR.Id.ToString(), TB1.Text, CB3.Text, CB4.Text, CB19.Text, CB2.Text, CB1.Text, CB5.Text, CB6.Text, formattedDate1, TB4.Text, CB7.Text, TB5.Text, CB9.Text, TB6.Text, CB10.Text, CB11.Text,
                        CB12.Text, CB13.Text, CB14.Text, TB7.Text, CB17.Text, CB15.Text, CB16.Text);
                        Checks a = ChecksDA.RetrieveChecksWithCarId(CR.Id.ToString());
                        ChecksDA.UpdateCheck(a.Id.ToString(), CR.Id.ToString(), CB18.Text, formattedDate3, TB10.Text, TB2.Text);
                        MessageBox.Show("The car has been changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CB1_Changed(object sender, SelectionChangedEventArgs e)
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
}
