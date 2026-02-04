using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahApps2.Controller
{
    public static class BudgetValidation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="budget"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Returns a empty string if no error;
        /// if errors, string of those errors.</returns>
        public static string ValidateBudget(string budget, DateTime? startDate, DateTime? endDate)
        {
            // Budget Amount Validation           
            string errors = "";
            double val;
            bool isNum = double.TryParse(budget, out val);

            if (budget == "")
            {
                errors += "No Budget Given\n";
            }

            if (isNum == false)
            {
                errors += "Budget Amount Invalid\n";
            }

            // Dates Validation
            if (startDate == null || endDate == null)
            {
                errors += "Invalid Date(s)\n";
            }
            else if (startDate > endDate)
            {
                errors += "Start Date is After End Date\n";
            }
            else if (startDate == endDate)
            {
                errors += "Start Date and End Date are the same\n";
            }
            return errors;
        }
        public static string ValidateExpense(string title, string amount)
        {
            string errors = "";

            // MAke sure 'title' isn't blank
            if (title == "" || title == null)
            { errors += "Invalid Title \n"; }

            // Make sure 'amount' is valid:
            if (amount != "" || amount == null)
            {
                double val;
                bool isNum = double.TryParse(amount, out val);

                if (!isNum)
                {
                    errors += "Expense Amount Invalid \n";
                }
            }
            else { errors += "Invalid Amount \n"; }

            return errors;
        }
    }
}
