using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgreSqlDotnetCore.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class Budget
    {

        [Key]
        public int budget_id { get; set; }

        [Required]
        public int user_id { get; set; }

        [Required]
        public int month { get; set; }

        [Required]
        public int year { get; set; }

        [Required]
        public decimal amount { get; set; }
    }

    public class DashboardViewModel
    {
        public decimal MonthlyBudget { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public decimal RemainingBudget { get; set; }
        public Dictionary<string, decimal>? ExpenseData { get; set; }
        public string UserName { get; set; }
    }

    public class Expense
    {
        [Key]
        public int expense_id { get; set; }
        public int user_id { get; set; }
        public string expense_type { get; set; }
        public decimal amount { get; set; }
        public DateOnly expense_date { get; set; }

    }

    public class ExpenseForecast
    {
        public int forecast_id { get; set; }
        public int user_id { get; set; }
        public string expense_type { get; set; }
        public decimal predicted_amount { get; set; }
        public DateOnly forecast_date { get; set; }
    }
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string? email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        
    }

    public class NewUser
    {
        [Key]
        public int user_id { get; set; }
        public string? email { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }

    }
    public class TeamMember
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public string Bio { get; set; }
    }
}