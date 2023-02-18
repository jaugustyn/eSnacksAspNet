using eSnacks.Data;
using eSnacks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eSnacks.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<eSnacksUser> _userManager;
        private readonly SignInManager<eSnacksUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<eSnacksUser> userManager, SignInManager<eSnacksUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
    }
}