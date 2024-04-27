using Microsoft.AspNetCore.Mvc;
using PostgreSqlDotnetCore.Data;
using PostgreSqlDotnetCore.Models;
using PostgreSqlDotnetCore.repositories;
using System;
using System.Globalization;

namespace PostgreSqlDotnetCore.Controllers
{
   
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public IActionResult Index()
        {
            // Retrieve the authenticated user's ID from the session
            TempData["UserNmae"] = HttpContext.Session.GetString("UserName");
            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");
            if (userId.HasValue)
            {
                // Retrieve the user's monthly budget and expenses for the past 12 months
                var monthlyBudgets_12 = _context.budgets.Where(b => b.user_id == userId.Value && b.year >= DateTime.Now.Year - 1)
                                                    .OrderByDescending(b => b.year)
                                                    .ThenBy(b => b.month)
                                                    .ToList();
                var monthlyExpenses_12 = _context.expenses.Where(e => e.user_id == userId.Value && e.expense_date.Year >= DateTime.Now.Year - 1)
                                           .GroupBy(e => new { e.expense_date.Month, e.expense_date.Year })
                                           .ToDictionary(g => (g.Key.Month, g.Key.Year), g => g.Sum(e => e.amount));



                // Calculate the total expenses and remaining budget for the past 12 months
                var totalExpenses_12 = monthlyExpenses_12.Values.Sum();
                var totalBudget_12 = monthlyBudgets_12.Sum(b => b.amount);
                var Profit_12 = totalBudget_12 - totalExpenses_12;

                // Compute the average monthly expenses to forecast the next month's expenses
                var lastThreeMonthsExpenses = monthlyExpenses_12.OrderByDescending(e => e.Key.Year).ThenByDescending(e => e.Key.Month).Take(3).Select(e => e.Value).ToList();
                var averageMonthlyExpenses = lastThreeMonthsExpenses.Average();
                var nextMonthExpensesForecast = averageMonthlyExpenses;

                // Retrieve the user's monthly budget and expenses for the current month
                var monthlyBudget = _context.budgets.FirstOrDefault(b => b.user_id == userId.Value && b.month == DateTime.Now.Month && b.year == DateTime.Now.Year)?.amount ?? 0;
                var monthlyExpenses = _context.expenses.Where(e => e.user_id == userId.Value && e.expense_date.Month == DateTime.Now.Month && e.expense_date.Year == DateTime.Now.Year).Sum(e => e.amount);
                var remainingBudget = monthlyBudget - monthlyExpenses;
                var TotalExpenses = _context.expenses.Where(e => e.user_id == userId.Value).Sum(e => e.amount);
                var TotalBudget = _context.budgets.Where(e => e.user_id == userId.Value).Sum(e => e.amount);

                // Get the current month name
                var currentMonthName = new DateTimeFormatInfo().GetMonthName(DateTime.Now.Month);

                // Retrieve the expense data for the chart
                var expenseData = _context.expenses
                    .Where(e => e.user_id == userId.Value && e.expense_date.Month == DateTime.Now.Month && e.expense_date.Year == DateTime.Now.Year)
                    .GroupBy(e => e.expense_type)
                    .ToDictionary(g => g.Key, g => g.Sum(e => e.amount));

                // Retrieve the expense data for the chart
                var TotalExpenseData = _context.expenses
                    .Where(e => e.user_id == userId.Value)
                    .GroupBy(e => e.expense_type)
                    .ToDictionary(g => g.Key, g => g.Sum(e => e.amount));

                // Create the DashboardViewModel and pass it to the view
                var viewModel = new DashboardViewModel
                {
                    MonthlyBudget = monthlyBudget,
                    MonthlyExpenses = monthlyExpenses,
                    RemainingBudget = remainingBudget,
                    ExpenseData = expenseData,
                    UserName = userName,
                    TotalExpenseData = TotalExpenseData,
                    TotalExpenses = TotalExpenses,
                    CurrentMonth = currentMonthName,
                    NextMonthExpensesForecast = nextMonthExpensesForecast,
                    Profit = Profit_12,
                    MonthlyBudgets_12 = monthlyBudgets_12,
                    MonthlyExpenses_12= monthlyExpenses_12,
                    TotalBudget= TotalBudget
                };
                return View(viewModel);
            }
            else
            {
                // Redirect the user to the login page or handle the case where the user's ID is not available
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public IActionResult SetBudget([FromForm] decimal monthlyBudget)
        {
            // Retrieve the authenticated user's ID from the session
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                // Create or update the user's monthly budget
                var budget = _context.budgets.FirstOrDefault(b => b.user_id == userId.Value && b.month == DateTime.Now.Month && b.year == DateTime.Now.Year);
                if (budget == null)
                {
                    budget = new Budget
                    {
                        user_id = userId.Value,
                        month = DateTime.Now.Month,
                        year = DateTime.Now.Year,
                        amount = monthlyBudget
                    };
                    _context.budgets.Add(budget);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    budget.amount = monthlyBudget;
                }
                _context.SaveChanges();

                // Redirect back to the dashboard
                return RedirectToAction("Index");
            }
            else
            {
                // Redirect the user to the login page or handle the case where the user's ID is not available
                return RedirectToAction("Login", "Home");
            }
        }
    }
}

