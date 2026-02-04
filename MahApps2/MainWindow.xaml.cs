using MahApps.Metro.Controls;
using MahApps2.Controller;
using MahApps2.Data;
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
        private Budget selectedItem;
        private int budgetId;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            //double balance = 1600;
            //RemainingBalance = $"{balance}€";

            budgets = new ObservableCollection<Budget>(BudgetData.GetBudgets());

            BudgetListView.ItemsSource = budgets;
        }

        private void NewBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            if (BudgetStackPanel.Visibility == Visibility.Collapsed)
            {
                BudgetStackPanel.Visibility = Visibility.Visible;
                AddButtonFontIcon.Glyph = "\uE738";
            }
            else
            {
                BudgetStackPanel.Visibility = Visibility.Collapsed;
                AddButtonFontIcon.Glyph = "\uE710";
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

            if (CreateBudgetButtom.Content.ToString() == "Create Budget")
            {
                Budget budget = new Budget
                {
                    StartDate = (DateTime)StartDatePicker.SelectedDate,
                    EndDate = (DateTime)EndDatePicker.SelectedDate,
                    BudgetAmount = double.Parse(TotalBudgetTextBox.Text)
                };

                budgets.Add(budget);

                BudgetListView.ItemsSource = budgets;

                BudgetData.AddBudgetToDb(budget);

                //UpdateFlyout.CloseButtonVisibility = Visibility.Hidden;

                //BudgetStackPanel.Visibility = Visibility.Collapsed;

                //UpdateFlyout.IsOpen = true;

                ShowSuccess("Successfully Added Budget");
            }
            else
            {
                if (CreateBudgetButtom.Content.ToString() == "Update Budget")
                {
                    BudgetData.UpdateBudgetDb(budgetId,
                        (DateTime)StartDatePicker.SelectedDate,
                        (DateTime)EndDatePicker.SelectedDate,
                        TotalBudgetTextBox.Text
                    );

                    StartDatePicker.SelectedDate = null;
                    EndDatePicker.SelectedDate = null;
                    TotalBudgetTextBox.Text = null;

                    BudgetStackPanel.Visibility = Visibility.Collapsed;
                    AddButtonFontIcon.Glyph = "\uE710";
                    CancelUpdateButton.Visibility = Visibility.Hidden;
                    ShowSuccess("Successfully Updated Budget");
                }
            }
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
        private void ShowSuccess(String text)
        {
            UpdateFlyout.Background = Brushes.Green;

            FlyoutTextBlock.Text = text;

            UpdateFlyout.CloseButtonVisibility = Visibility.Hidden;

            BudgetStackPanel.Visibility = Visibility.Collapsed;

            UpdateFlyout.IsOpen = true;
        }

        private void BudgetListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = (Budget)BudgetListView.SelectedItem;

            RemainingBalance = $"{selectedItem.BudgetAmount}€";

            RemainingBudgetTextBlock.Text = RemainingBalance;

            AddExpenseButton.Visibility = Visibility.Visible;

            ExpensesListView.ItemsSource = BudgetData.GetExpenses(selectedItem.Id);
        }
        
        private void EditBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            Budget budget = b.CommandParameter as Budget;

            StartDatePicker.SelectedDate = budget.StartDate;
            EndDatePicker.SelectedDate = budget.EndDate;
            TotalBudgetTextBox.Text = budget.BudgetAmount.ToString();

            BudgetStackPanel.Visibility = Visibility.Visible;
            AddButtonFontIcon.Glyph = "\uE738";
            CancelUpdateButton.Visibility = Visibility.Visible;

            CreateBudgetButtom.Content = "Update Budget";

            budgetId = budget.Id;
        }

        private void CancelUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            TotalBudgetTextBox.Text = null;

            BudgetStackPanel.Visibility = Visibility.Collapsed;
            AddButtonFontIcon.Glyph = "\uE710";
            CancelUpdateButton.Visibility = Visibility.Hidden;
        }

        private void AddExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            ExpenseStackPanel.Visibility = Visibility.Visible;
        }
        private void CreateExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            string errors = BudgetValidation.ValidateExpense(ExpenseTitleTextBox.Text, ExpenseAmountTextBox.Text);

            if (errors == "")
            {
                Expense expense = new Expense
                {
                    BudgetId = selectedItem.Id,
                    Title = ExpenseTitleTextBox.Text,
                    Amount = double.Parse(ExpenseAmountTextBox.Text)
                };

                BudgetData.AddExpenseToDb(expense);
                ShowSuccess("Successfully Added Expense");
                ExpensesListView.ItemsSource = BudgetData.GetExpenses(selectedItem.Id);
            }
            else
            { MessageBox.Show(errors); }
        }
    }
}