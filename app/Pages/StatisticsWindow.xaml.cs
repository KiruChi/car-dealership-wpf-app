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
using LiveCharts;
using LiveCharts.Wpf;
using System.Runtime.Serialization;
using LiveCharts.Configurations;
using MySqlX.XDevAPI.Common;
using System.Collections;
using LiveCharts.Helpers;
using System.Text.RegularExpressions;

namespace Kursova.Pages
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : UserControl
    {
        private List<Branches> CR;
        private List<Statistic> DR1;
        private List<Branches> DR2;
        private Style originalButtonStyle;
        public string[] Names1 { get; set; }
        public string[] Names2 { get; set; }
        public StatisticsWindow() 
        {
            InitializeComponent();
            CR = BranchesDA.RetrieveBranches();
            Mapper = Mappers.Xy<Statistic>()
                .X((car, index) => index)
                .Y(car => car.Numofsales);

            DR1 = StatisticDA.RetrieveStatisticCars("", "", "", "", "");
            var records = DR1.OrderByDescending(x => x.Numofsales).Take(15).ToArray();

            Results = records.AsChartValues();
            Labels = new ObservableCollection<string>(records.Select(x => x.Name));

            MillionFormatter = value => (value).ToString("N1") + " cars";
            DataContext = this;
            Bor1.Visibility = Visibility.Collapsed;
            filterColumn.Width = new GridLength(0);
            FilterUpdate();
        }
        public ChartValues<Statistic> Results { get; set; }
        public ObservableCollection<string> Labels { get; set; }
        public Func<double, string> MillionFormatter { get; set; }

        public object Mapper { get; set; }
        private void FilterUpdate()
        {
            int i = 0;
            Names1 = new string[] { "Cars", "Motor_shows", "Employees", "Clients"};
            DR2 = BranchesDA.RetrieveMotor_shows();
            i = DR2.Count();
            Names2 = new string[i+1];
            Names2[0] = "All";
            i = 1;
            foreach (Branches c in DR2)
            {
                Names2[i] = c.Name;
                i += 1;
            }
            CB1.SelectedIndex = 0;
            CB2.SelectedIndex = 0;
            DataContext = this;
        }
        public void SearchC(string s)
        {
            DataContext = null;
            FilterUpdate();
            var q = (s ?? string.Empty).ToUpper();
            DR1 = StatisticDA.RetrieveStatisticCars("", "", "", "", "");
            var records = DR1
                .Where(x => x.Name.ToUpper().Contains(q))
                .OrderByDescending(x => x.Numofsales)
                .Take(15)
                .ToArray();

            Results.Clear();
            Results.AddRange(records);

            Labels.Clear();
            foreach (var record in records) Labels.Add(record.Name);
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
        private void btnAplly_Click(object sender, RoutedEventArgs e)
        {
            try
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
                if (int.TryParse(Quantity.Text, out int res) == false || int.Parse(Quantity.Text) < 0)
                {
                    MessageBox.Show("Please enter normal quantity", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    goto gt1;
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
                if (CB1.SelectedIndex == 0)
                {
                    DR1 = StatisticDA.RetrieveStatisticCars(CB2.Text, formattedDate1, formattedDate2, Cost1.Text, Cost2.Text);
                    var records = DR1.OrderByDescending(x => x.Numofsales).Take(int.Parse(Quantity.Text)).ToArray();
                    Results.Clear();
                    Results.AddRange(records);
                    Labels.Clear();
                    foreach (var record in records) Labels.Add(record.Name);
                }
                if (CB1.SelectedIndex == 1)
                {
                    DR1 = StatisticDA.RetrieveStatisticBranches(CB2.Text, formattedDate1, formattedDate2, Cost1.Text, Cost2.Text);
                    var records = DR1.OrderByDescending(x => x.Numofsales).Take(int.Parse(Quantity.Text)).ToArray();
                    Results.Clear();
                    Results.AddRange(records);
                    Labels.Clear();
                    foreach (var record in records) Labels.Add(record.Name);
                }
                if (CB1.SelectedIndex == 2)
                {
                    DR1 = StatisticDA.RetrieveStatisticEmployees(CB2.Text, formattedDate1, formattedDate2, Cost1.Text, Cost2.Text);
                    var records = DR1.OrderByDescending(x => x.Numofsales).Take(int.Parse(Quantity.Text)).ToArray();
                    Results.Clear();
                    Results.AddRange(records);
                    Labels.Clear();
                    foreach (var record in records) Labels.Add(record.Name);
                }
                if (CB1.SelectedIndex == 3)
                {
                    CS1.Title = "Number of purchases:";
                    DR1 = StatisticDA.RetrieveStatisticClients(CB2.Text, formattedDate1, formattedDate2, Cost1.Text, Cost2.Text);
                    var records = DR1.OrderByDescending(x => x.Numofsales).Take(int.Parse(Quantity.Text)).ToArray();
                    Results.Clear();
                    Results.AddRange(records);
                    Labels.Clear();
                    foreach (var record in records) Labels.Add(record.Name);
                }
            gt1:;
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
                CB1.SelectedIndex = 0;
                CB2.SelectedIndex = 0;
                Quantity.Text = "15";
                Date1.Text = "from";
                Date2.Text = "to";
                Date1.SelectedDate = null;
                Date2.SelectedDate = null;
                Cost1.Text = "from";
                Cost2.Text = "to";
                CS1.Title = "Number of sales:";
                DR1 = StatisticDA.RetrieveStatisticCars("", "", "", "", "");
                var records = DR1.OrderByDescending(x => x.Numofsales).Take(int.Parse(Quantity.Text)).ToArray();
                Results.Clear();
                Results.AddRange(records);
                Labels.Clear();
                foreach (var record in records) Labels.Add(record.Name);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}