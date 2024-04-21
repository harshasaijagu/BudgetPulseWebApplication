//using PostgreSqlDotnetCore.Models;
//using System;
//namespace PostgreSqlDotnetCore.repositories
//{
//    public class BudgetRepository
//    {
//        private readonly List<Budget> _budgets;

//        public BudgetRepository()
//        {
//            // Initialize the budget data
//            _budgets = new List<Budget>
//            {
//                new Budget
//                {
//                    budget_id = 1,
//                    user_id = 1,
//                    monthly_budget = 250,
//                    start_date = new DateTime(2023, 1, 1),
//                    end_date = new DateTime(2023, 12, 31)
                    
//                }
//            };
//        }

//        public Budget GetCurrentBudget(string username)
//        {
//            // Retrieve the current budget for the user
//            return _budgets.FirstOrDefault(b => b.user_id == 1);
//        }

//        public void UpdateBudget(string username, decimal monthlyBudget)
//        {
//            // Update the user's budget
//            var budget = _budgets.FirstOrDefault(b => b.user_id == 1);
//            if (budget != null)
//            {
//                budget.monthly_budget = monthlyBudget;
               
//            }
//        }
//    }
//}

