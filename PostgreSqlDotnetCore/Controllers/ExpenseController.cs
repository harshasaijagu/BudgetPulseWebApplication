using System;
using Microsoft.AspNetCore.Mvc;
using PostgreSqlDotnetCore.Data;
using PostgreSqlDotnetCore.Models;

namespace PostgreSqlDotnetCore.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieve the authenticated user's ID from the session
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                // Retrieve the expenses for the authenticated user
                var expenses = _context.expenses.Where(e => e.user_id == userId.Value).ToList();
                return View(expenses);
            }
            else
            {
                // Redirect the user to the login page or handle the case where the user's ID is not available
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public IActionResult AddExpense()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddExpense(Expense expense)
        {
            if (ModelState.IsValid)
            {
                expense.user_id = GetAuthenticatedUserId();
                expense.expense_date = DateOnly.Parse(expense.expense_date.ToString());

                // Add the new expense to the Expenses DbSet
                _context.expenses.Add(expense);
                _context.SaveChanges(); // Save changes to the database

                // Redirect to the expense list
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        [HttpGet]
        public IActionResult DeleteExpense(int id)
        {
            // Get the authenticated user's ID
            var userId = GetAuthenticatedUserId();

            // Retrieve the expense to be deleted
            var expense = _context.expenses.FirstOrDefault(e => e.expense_id == id && e.user_id == userId);

            if (expense == null)
            {
                return RedirectToAction("Index");
            }

            // Remove the expense from the Expenses DbSet
            _context.expenses.Remove(expense);
            _context.SaveChanges(); // Save changes to the database
            // Return a success response
            return RedirectToAction("Index");
        }
        public int GetAuthenticatedUserId()
        {
            // Get the authenticated user's ID from the session
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                return userId.Value;
            }
            else
            {
                // Handle the case where the user's ID is not available in the session
                // (e.g., redirect to the login page)
                return 0;
            }
        }
    }
}
