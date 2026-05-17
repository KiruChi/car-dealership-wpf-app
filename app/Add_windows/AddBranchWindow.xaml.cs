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
    /// Interaction logic for AddBranchWindow.xaml
    /// </summary>
    public partial class AddBranchWindow : Window
    {
        private Branches CR;
        private int k = 0;
        public string[] Names1 { get; set; }
        public AddBranchWindow()
        {
            InitializeComponent();
            Initialize();
            Inf.Text = "Add";
            k = 0;
        }
        public AddBranchWindow(Branches cr)
        {
            InitializeComponent();
            Initialize();
            CR = cr;
            TB1.Text = cr.Name;
            TB2.Text = cr.Address;
            TB3.Text = cr.Phone.ToString();
            TB4.Text = cr.Email;
            TB5.Text = cr.EDRPOU1.ToString();
            if (cr.Type == "Motor show")
            {
                CB1.SelectedIndex = 0;
            }
            else if (cr.Type == "Service center")
            {
                CB1.SelectedIndex = 1;
            }
            k = 1;
        }
        private void Initialize()
        {
            int i = 0;

            Names1 = new String[] { "Motor show", "Service center" };

            DataContext = this;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                long result;
                if (long.TryParse(TB3.Text, out result) == false)
                {
                    MessageBox.Show("Please enter a number in Phone number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt64(TB3.Text) < 0)
                {
                    MessageBox.Show("Please enter a positive number in Phone number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if(TB3.Text.Length != 10)
                {
                    MessageBox.Show("Incorrectly entered Phone number", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Regex.IsMatch(TB4.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") == false) 
                {
                    MessageBox.Show("Incorrectly entered Email", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (TB5.Text.Length != 8)
                {
                    MessageBox.Show("Incorrectly entered EDRPOU number", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CB1.Text == "Type")
                {
                    MessageBox.Show("Incorrectly entered Type", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (k == 0)
                    {
                        int n = 0;
                        List<Branches> ts = BranchesDA.RetrieveBranches();
                        foreach (Branches t in ts)
                        {
                            if (t.EDRPOU1.ToString() == TB5.Text)
                            {
                                n = 1;
                            }
                        }
                        if (n == 1)
                        {
                            MessageBox.Show("A branch with such EDRPOU already exists", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                            goto gt1;
                        }

                        int i = 0;
                        int j = 0;
                        List<Branches> sa = BranchesDA.RetrieveBranches();
                        foreach (Branches u in sa)
                        {
                            i += 1;
                        }
                        BranchesDA.AddBranch("false", TB1.Text, TB2.Text, TB3.Text, TB4.Text, TB5.Text, CB1.Text);
                        sa = BranchesDA.RetrieveBranches();
                        foreach (Branches u in sa)
                        {
                            j += 1;
                        }
                        if (j <= i)
                        {
                            MessageBox.Show("Error adding a new branch", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("A new branch has been added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            AddBrWind.Close();
                        }
                    gt1:;
                    }
                    else if (k == 1)
                    {
                        BranchesDA.UpdateBranch(CR.Id.ToString(), TB1.Text, TB2.Text, TB3.Text, TB4.Text, TB5.Text, CB1.Text);
                        MessageBox.Show("The branch has been changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
            AddBrWind.Close();
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox box)
            {
                if (string.IsNullOrEmpty(box.Text))
                    box.Background = (ImageBrush)FindResource("watermark");
                else
                    box.Background = null;
            }
        }
    }
}