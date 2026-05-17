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
using System;
using System.Diagnostics;
using System.Windows;
using Xceed.Words.NET;
using System.IO;
using Spire.Doc;
using System.Runtime.ConstrainedExecution;

namespace Kursova.Add_windows
{
    /// <summary>
    /// Interaction logic for BuyCarWindow.xaml
    /// </summary>
    public partial class BuyCarWindow : System.Windows.Window
    {
        private Cars CR;
        private List<Employees> DR2;
        private List<Clients> DR7;
        private Clients cl;
        private int k = 0;
        public string[] Names1 { get; set; }
        public string[] Names2 { get; set; }    
        public string[] Names14 { get; set; }
        public BuyCarWindow(Cars cr)
        {
            InitializeComponent();
            CR = cr;
            TB1.Text = cr.Vin_code;
            TB2.Text = cr.Name;
            TB3.Text = cr.Owner;
            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("yyyy-MM-dd");
            TB8.Text = formattedDate;
            Initialize();
            CB14.SelectionChanged += CB1_Changed;
        }
        private void Initialize()
        {
            int i = 0;

            Names1 = new String[] {"Male", "Female"};
            
            DR2 = EmployeesDA.RetrieveEmployeesWithMotorShow(CR.Motor_show);
            i = DR2.Count();
            Names2 = new String[i];
            i = 0;
            foreach (Employees c in DR2)
            {
                Names2[i] = c.Full_name;
                i += 1;
            }

            DR7 = ClientsDA.RetrieveClients();
            i = DR7.Count();
            Names14 = new String[i+1];
            i = 0;
            Names14[0] = "New";
            i = 1;
            foreach (Clients c in DR7)
            {
                Names14[i] = c.Full_name;
                i += 1;
            }
            CB14.SelectedIndex = 0;

            DataContext = this;
        }
        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Checks aaa = ChecksDA.RetrieveChecksWithCarId(CR.Id.ToString());
                DateTime date1 = DateTime.Parse(aaa.Date);
                string formattedDateCheck = date1.ToString("yyyyMMdd");
                DateTime now = DateTime.Now;
                string formattedDateNow = now.ToString("yyyyMMdd");
                long result;
                string formattedDate1 = TB6.Text;
                if (Regex.IsMatch(formattedDate1, @"^\d{2}\.\d{2}\.\d{4}$"))
                {
                    DateTime date = DateTime.ParseExact(TB6.Text, "dd.MM.yyyy", null);
                    formattedDate1 = date.ToString("yyyy-MM-dd");
                }
                string formattedDate2 = TB8.Text;
                if (Regex.IsMatch(formattedDate2, @"^\d{2}\.\d{2}\.\d{4}$"))
                {
                    DateTime date = DateTime.ParseExact(TB8.Text, "dd.MM.yyyy", null);
                    formattedDate2 = date.ToString("yyyy-MM-dd");
                }
                if ( TB5.Text.Length != 10)
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
                else if (Regex.IsMatch(formattedDate1, @"^\d{4}-\d{2}-\d{2}$") == false)
                {
                    MessageBox.Show("Incorrectly entered Birthday", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Math.Abs(Convert.ToInt64(formattedDateNow) - Convert.ToInt64(DateTime.ParseExact(TB6.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd"))) < 00180000)
                {
                    MessageBox.Show("It is not possible to register someone under the age of 18", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (TB7.Text.Length != 10)
                {
                    MessageBox.Show("Incorrectly entered RNOCPP", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB2.Text == "Employee")
                {
                    MessageBox.Show("Incorrectly entered Employee", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Regex.IsMatch(formattedDate2, @"^\d{4}-\d{2}-\d{2}$") == false)
                {
                    MessageBox.Show("Incorrectly entered Sale date", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt64(formattedDateCheck) > Convert.ToInt64(DateTime.ParseExact(TB8.Text, "dd.MM.yyyy", null).ToString("yyyyMMdd")))
                {
                    MessageBox.Show("Sale date cannot be less than the date of check of the car", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (long.TryParse(TB9.Text, out result) == false)
                {
                    MessageBox.Show("Please enter a number in Price", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt64(TB9.Text) < 0)
                {
                    MessageBox.Show("Please enter a positive number in Price", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToDouble(TB9.Text) < (CR.Price - (CR.Price * 0.07)) || Convert.ToDouble(TB9.Text) > (CR.Price + (CR.Price * 0.07)))
                {
                    MessageBox.Show("The price can't be that low or that hight from the price of the car(max 7% of the car's price)", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    int n = 0;
                    List<Clients> ts = ClientsDA.RetrieveClients();
                    foreach (Clients t in ts)
                    {
                        if (t.RNOCPP1.ToString() == TB7.Text && CB14.Text == "New")
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
                    List<Sales> sa = SalesDA.RetrieveSales();
                    foreach (Sales car in sa)
                    {
                        i += 1;
                    }
                    if (CB14.SelectedIndex == 0)
                    {
                        ClientsDA.AddClient("false", TB4.Text, TB5.Text, CB1.Text, formattedDate1, TB10.Text, TB7.Text);
                        string s = "";
                        List<Clients> clients = ClientsDA.RetrieveClients();
                        foreach (Clients c in clients)
                        {
                            s = c.Id.ToString();
                        }
                        Employees a = EmployeesDA.RetrieveEmployeesWithName(CB2.Text);
                        SalesDA.AddSale("false", CR.Id.ToString(), s, a.Id.ToString(), formattedDate2, TB9.Text);
                    }
                    else
                    {
                        Clients b = ClientsDA.RetrieveClientsWithName(CB14.Text);
                        Employees a = EmployeesDA.RetrieveEmployeesWithName(CB2.Text);
                        SalesDA.AddSale("false", CR.Id.ToString(), b.Id.ToString(), a.Id.ToString(), formattedDate2, TB9.Text);
                    }
                    sa = SalesDA.RetrieveSales();
                    foreach (Sales car in sa)
                    {
                        j += 1;
                    }
                    if (j <= i)
                    {
                        MessageBox.Show("Error adding a new sale", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Order processing is successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        DocumentForm();
                        BuyCWind.Close();
                        CarsDA.UpdateCarStatus(CR.Id.ToString(), "Sold");
                    }
                gt1:;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void DocumentForm()
        {
            string folderPath = @"C:\Documents";
            Directory.CreateDirectory(folderPath);

            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            string relativePath = @"Add_windows\Vehicle-Purchase-Agreement-Template.docx";
            string filePath = System.IO.Path.Combine(projectDirectory, relativePath);

            string filledFilePath = $"C:\\Documents\\Filled-{CR.Vin_code}-Purchase-Agreement-Template.docx";

            using (var document = DocX.Load(filePath))
            {
                document.ReplaceText("[SellerName]", TB3.Text);
                Clients a = ClientsDA.RetrieveClientsWithName(TB3.Text);
                document.ReplaceText("[SellerAddress]", a.Address);
                document.ReplaceText("[BuyerName]", TB4.Text);
                document.ReplaceText("[BuyerAddress]", TB10.Text);
                document.ReplaceText("[CarBrand]", CR.Brand);
                document.ReplaceText("[CarModel]", CR.Model);
                document.ReplaceText("[CarBody]", CR.Body);
                document.ReplaceText("[CarColor]", CR.Color);
                document.ReplaceText("[CarYear]", CR.Year);
                document.ReplaceText("[CarMileage]", CR.Mileage.ToString());
                document.ReplaceText("[CarVinCode]", CR.Vin_code);
                document.ReplaceText("[PurchasePrice]", TB9.Text);
                document.SaveAs(filledFilePath);
            }

            Document doc = new Document();
            doc.LoadFromFile(filledFilePath);
            Process.Start(new ProcessStartInfo(filledFilePath) { UseShellExecute = true });
            MessageBox.Show($"The document has been successfully completed and saved as a \"Filled-{CR.Vin_code}-Purchase-Agreement.pdf\" in C:\\Documents\\");
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
            BuyCWind.Close();
        }

        private void CB1_Changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selected = CB14.SelectedValue.ToString();
                if (selected != "New" || selected != "")
                {
                    cl = ClientsDA.RetrieveClientsWithName(selected);
                    TB4.Text = cl.Full_name;
                    TB5.Text = cl.Phone.ToString();
                    CB1.Text = cl.Sex;
                    TB6.Text = cl.Birthday;
                    TB10.Text = cl.Address;
                    TB7.Text = cl.RNOCPP1.ToString();
                }
                else
                {
                    TB4.Text = "";
                    TB5.Text = "";
                    CB1.Text = "Gender";
                    TB6.Text = "";
                    TB7.Text = "";
                }
            }
            catch (Exception exception)
            {
            }
        }
    }
}
