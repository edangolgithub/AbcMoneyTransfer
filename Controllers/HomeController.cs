using AbcMoneyTransfer.Data;
using AbcMoneyTransfer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AbcMoneyTransfer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly ApplicationDbContext _context;
        //private readonly Seed _seed;

        public HomeController(ILogger<HomeController> logger/*,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, Seed seed*/)
        {
            //_userManager = userManager;
            //_roleManager = roleManager;
            //_context = context;
           // _seed = seed;
            _logger= logger;
        }

 
       

        public async Task<IActionResult> IndexAsync()
        {
            //await _seed.SeedAsync(_userManager, _roleManager, _context);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
