using MahApps.Metro.Controls;
using MahApps2.Controller;
using MahApps2.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MahApps2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public string RemainingBalance { get; set; }
        public ObservableCollection<Budget> budgets;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            double balance = 1600;
            RemainingBalance = $"{balance}€";

            budgets = new ObservableCollection<Budget>();
        }

        private void NewBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            if (BudgetStackPanel.Visibility == Visibility.Collapsed)
            {
                BudgetStackPanel.Visibility = Visibility.Visible;
            }
        }

        private void CreateBudgetButtom_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = BudgetValidation.ValidateBudget(TotalBudgetTextBox.Text,
                StartDatePicker.SelectedDate,
                EndDatePicker.SelectedDate);

            if (errorMessage != "")
            {
                ShowError(errorMessage);
                return;
            }

            Budget budget = new Budget
            {
                StartDate = (DateTime)StartDatePicker.SelectedDate,
                EndDate = (DateTime)EndDatePicker.SelectedDate,
                BudgetAmount = double.Parse(TotalBudgetTextBox.Text)
            };

            budgets.Add(budget);

            BudgetListView.ItemsSource = budgets;

            //UpdateFlyout.CloseButtonVisibility = Visibility.Hidden;

            //BudgetStackPanel.Visibility = Visibility.Collapsed;

            //UpdateFlyout.IsOpen = true;

            ShowSuccess();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateFlyout.IsOpen = true;
        }

        private void ShowError(string error)
        {
            UpdateFlyout.Background = Brushes.Red;

            FlyoutTextBlock.Text = error;

            UpdateFlyout.CloseButtonVisibility = Visibility.Hidden;

            // BudgetStackPanel.Visibility = Visibility.Collapsed;

            UpdateFlyout.IsOpen = true;
        }
        private void ShowSuccess()
        {
            UpdateFlyout.Background = Brushes.Green;

            FlyoutTextBlock.Text = "Successfully Added Budget";

            UpdateFlyout.CloseButtonVisibility = Visibility.Hidden;

            BudgetStackPanel.Visibility = Visibility.Collapsed;

            UpdateFlyout.IsOpen = true;
        }


    }
}