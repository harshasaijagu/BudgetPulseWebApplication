
using System.Collections.Generic;
using System.Linq;
using System;
using PostgreSqlDotnetCore.Data;
using PostgreSqlDotnetCore.Models;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace PostgreSqlDotnetCore.repositories
{
    public class ExpenseRepository
    {
        private readonly List<Expense> _expenses;

        public ExpenseRepository()
        {
            // Initialize the expense data
            _expenses = new List<Expense>
            {
               
            };
        }

        public List<Expense> GetExpensesByUser(string username)
        {
            // Retrieve the expenses for the user
            return _expenses.Where(e => e.user_id == 1).ToList();
        }

        //public void AddExpense(Expense expense)
        //{
        //    // Add the new expense to the list
        //    expense.expense_id = _expenses.Count + 1;
        //    expense.user_id = 1;
        //    expense.budget_id = 1;
            
        //}
    }

};

