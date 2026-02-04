using MahApps2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahApps2.Data
{
    public static class BudgetData
    {
        public static void AddBudgetToDb(Budget budget)
        {
            using (var db = new BudgetContext())
            {
                db.Database.Migrate();
                db.Add(budget);
                db.SaveChanges();
            }
        }

        public static List<Budget> GetBudgets()
        {
            using (var db = new BudgetContext())
            {
                return db.budgets.OrderBy(b => b.Id).ToList();
            }
        }

        public static void UpdateBudgetDb(int budgetKey, DateTime startDate, DateTime endDate, string budgetAmount)
        {
            using (var db = new BudgetContext())
            {
                Budget budget = db.budgets.Find(budgetKey);
                budget.StartDate = startDate;
                budget.EndDate = endDate;
                budget.BudgetAmount = double.Parse(budgetAmount);
                db.SaveChanges();
            }
        }

        public static List<Expense> GetExpenses(int budgetId)
        {
            using (var db = new BudgetContext())
            {
                return db.expenses.Where(x => x.BudgetId == budgetId).OrderBy(x => x.Id).ToList();
            }
        }

        public static void AddExpenseToDb(Expense expense)
        {
            using (var db = new BudgetContext())
            {
                db.Database.Migrate();
                db.Add(expense);
                db.SaveChanges();
            }
        }
    }
}
