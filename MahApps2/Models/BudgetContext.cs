using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MahApps2.Models
{
    public class BudgetContext : DbContext
    {
        public DbSet<Budget> budgets { get; set; }
        public DbSet<Expense> expenses { get; set; }

        public string path = @"Data/budget.db";

        //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={path}");
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data",
                "budget.db"
            );

            options.UseSqlite($"Data Source={path}");
        }

    }
}
