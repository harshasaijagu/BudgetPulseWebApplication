using Microsoft.AspNetCore.Mvc;
using PostgreSqlDotnetCore.Data;
using PostgreSqlDotnetCore.Models;
using PostgreSqlDotnetCore.repositories;
using System;
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
            // Retrieve the authenticated user's ID from the sessio
            TempData["UserNmae"] = HttpContext.Session.GetString("UserName");
            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");
            if (userId.HasValue)
            {
                // Retrieve the user's monthly budget and expenses
                var monthlyBudget = _context.budgets.FirstOrDefault(b => b.user_id == userId.Value && b.month == DateTime.Now.Month && b.year == DateTime.Now.Year)?.amount ?? 0;
                var monthlyExpenses = _context.expenses.Where(e => e.user_id == userId.Value && e.expense_date.Month == DateTime.Now.Month && e.expense_date.Year == DateTime.Now.Year).Sum(e => e.amount);
                var remainingBudget = monthlyBudget - monthlyExpenses;

                // Retrieve the expense data for the chart
                var expenseData = _context.expenses
                    .Where(e => e.user_id == userId.Value && e.expense_date.Month == DateTime.Now.Month && e.expense_date.Year == DateTime.Now.Year)
                    .GroupBy(e => e.expense_type)
                    .ToDictionary(g => g.Key, g => g.Sum(e => e.amount));

                // Create the DashboardViewModel and pass it to the view
                var viewModel = new DashboardViewModel
                {
                    MonthlyBudget = monthlyBudget,
                    MonthlyExpenses = monthlyExpenses,
                    RemainingBudget = remainingBudget,
                    ExpenseData = expenseData,
                    UserName= userName
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

