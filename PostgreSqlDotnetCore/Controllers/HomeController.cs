using Microsoft.AspNetCore.Mvc;
using PostgreSqlDotnetCore.Data;
using PostgreSqlDotnetCore.Models;


namespace PostgreSqlDotnetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        { 
         
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists
                var existingUser = _context.users.FirstOrDefault(u => u.email == user.email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("email", "Email address is already registered.");
                    return RedirectToAction("Login"); 
                }

                // Add the new user to the database
                _context.users.Add(user);
                _context.SaveChanges();

                // Redirect the user to the login page
                return RedirectToAction("Login");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Loginuser(User user)
        {
            user.email = null;
            if (ModelState.IsValid)
            {
                // Authenticate the user
                var authenticatedUser = _context.users.FirstOrDefault(u => u.username == user.username && u.password == user.password);
                if (authenticatedUser != null)
                {
                    // Store the authenticated user's ID in the session
                    HttpContext.Session.SetInt32("UserId", authenticatedUser.user_id);
                    HttpContext.Session.SetString("UserName", authenticatedUser.username);

                    // Redirect the user to the dashboard
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    // Display an error message and return to the login page
                    ModelState.AddModelError("", "Invalid email or password.");
                    return RedirectToAction("Login", "Home");
                }
            }

            return View(user);
        }
        public IActionResult Logout()
        {
            // Clear the user's ID from the session
            HttpContext.Session.Remove("UserId");

            // Redirect the user to the login page
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Cancel()
        {
            // Redirect the user to the login page
            return RedirectToAction("Login", "Home");
        }
    }
}