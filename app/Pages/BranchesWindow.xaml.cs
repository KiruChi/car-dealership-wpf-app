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

namespace Kursova.Pages
{
    /// <summary>
    /// Interaction logic for BranchesWindow.xaml
    /// </summary>
    public partial class BranchesWindow : UserControl
    {
        private List<Branches> CR;
        private ObservableCollection<Branches> DeletedCR;
        private List<Checks> DR1;
        private Style originalButtonStyle;
        public string[] Names0 { get; set; }
        public string[] Names1 { get; set; }
        public BranchesWindow()
        {
            InitializeComponent();
            CR = BranchesDA.RetrieveBranches();
            dtGrid.ItemsSource = CR;
            CB0.SelectionChanged += CB_Changed;
            originalButtonStyle = btnInfo.Style;
            DR1 = new List<Checks>();
            DeletedCR = new ObservableCollection<Branches>();
            DataContext = this;
            Bor1.Visibility = Visibility.Collapsed;
            filterColumn.Width = new GridLength(0);
            FilterUpdate();
        }
        private void FilterUpdate()
        {
            int i = 0;
            Names0 = new string[] { "Available branches", "Deleted branches" };
            CB0.SelectedIndex = 0;
            Names1 = new string[] { "Motor show", "Service center" };

            DataContext = this;
        }
        private void btnAplly_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    if (CB1.SelectedIndex == 0)
                    {
                        dtGrid.ItemsSource = BranchesDA.RetrieveMotor_shows();
                    }
                    else if (CB1.SelectedIndex == 1)
                    {
                        dtGrid.ItemsSource = BranchesDA.RetrieveService_centers();
                    }
                    DataContext = this;
                }
                else
                {
                    MessageBox.Show("The filter when selecting Deleted branches is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                dtGrid.ItemsSource = BranchesDA.SearchBranches(s);
            }
            else
            {
                MessageBox.Show("The Search when selecting Deleted branches is not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                Branches c = (Branches)dtGrid.SelectedItem;
                AddBranchWindow win = new AddBranchWindow(c);
                win.Show();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB0.SelectedIndex == 0)
                {
                    AddBranchWindow win = new AddBranchWindow();
                    win.Show();
                }
                if (CB0.SelectedIndex == 1)
                {
                    Branches c = (Branches)dtGrid.SelectedItem;
                    BranchesDA.AddBranch(c.Id.ToString(), c.Name, c.Address, c.Phone.ToString(), c.Email, c.EDRPOU1.ToString(), c.Type);
                    Checks b = new Checks(0, 0, 0, "", 0, 0);
                    if (DR1 != null)
                    {
                        foreach (Checks i in DR1)
                        {
                            if (i.Service_centerid == c.Id)
                            {
                                b = i;
                                ChecksDA.AddCheck(b.Id.ToString(), b.Carid.ToString(), b.Service_centerid.ToString(), b.Date, b.Check_price.ToString(), b.Car_price.ToString());
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
                    Branches c = (Branches)dtGrid.SelectedItem;
                    if (c.Type == "Motor show")
                    {
                        
                        int i = 0;
                        int j = 0;
                        List<Branches> y = BranchesDA.RetrieveBranches();
                        foreach (Branches s in y)
                        {
                            i += 1;
                        }
                        CR = BranchesDA.DeleteMotor_show(c.Id.ToString());
                        y = BranchesDA.RetrieveBranches();
                        foreach (Branches s in y)
                        {
                            j += 1;
                        }
                        if (j >= i)
                        {
                            MessageBox.Show("If the motor show has an employee and cars, remove them first", "Hint", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            DeletedCR.Add(c);
                            dtGrid.ItemsSource = CR;
                        }
                    }
                    else
                    {
                        DeletedCR.Add(c);
                        List<Checks> b = ChecksDA.RetrieveChecksWithServiceCenterId(c.Id.ToString());
                        if (b != null)
                        {
                            foreach (Checks s in b)
                            {
                                DR1.Add(s);
                            }
                        }
                        CR = BranchesDA.DeleteService_center(c.Id.ToString());
                        dtGrid.ItemsSource = CR;
                    }
                }
                if (CB0.SelectedIndex == 1)
                {
                    Branches c = (Branches)dtGrid.SelectedItem;
                    if (DR1 != null)
                    {
                        foreach (Checks i in DR1)
                        {
                            if (i.Service_centerid == c.Id)
                            {
                                DR1.Remove(i);
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
                    CR = BranchesDA.RetrieveBranches();
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
                    CR = BranchesDA.RetrieveBranches();
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