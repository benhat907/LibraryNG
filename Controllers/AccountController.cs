using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using LibraryNG.Models;
using LibraryNG;

namespace LibraryNG.Controllers
{
    //[Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> LoginMethod(LoginModel log)
        {
            string query = "SELECT EMAIL, MATKHAU FROM Users WHERE EMAIL = @Email AND MATKHAU = @Password";
            DataTable data = DataProvider.ExcuteQuery(query, new Dictionary<string, object>{
                {"@Email", log.Email},
                {"@Password", log.MatKhau}
                        });
            if (data.Rows.Count <= 0)
                return Json(new { resault = "Đăng nhập thất bại" });

            // var claims = new List<Claim>(){
            //     new Claim("Email", log.Email),
            // };
            // var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            // var principal = new ClaimsPrincipal(identity);

            // await HttpContext.SignInAsync(principal);

            return Json(new { resault = "ok" });
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult RegisterMethod(LoginModel reg)
        {
            string query = string.Format(@"insert into Users(MaNV, HoTenNV, BoPhan, Email, MatKhau, RoleID, Avatar) values(@MaNV, @HoTenNV, @BoPhan, @Email, @MatKhau, @RoleID, @Avatar)");
            int rows = DataProvider.ExecuteNonQuery(query, new Dictionary<string, object>{
                {"@MaNV", reg.MaNV},
                {"@HoTenNV", reg.HoTenNV},
                {"@BoPhan", reg.BoPhan},
                {"@Email", reg.Email},
                {"@MatKhau", reg.MatKhau},
                {"@RoleID", reg.RoleID},
                {"@Avatar", reg.Avatar}
            });
            if (rows <= 0)
                return Json(new { resault = "Đăng ký không thành công" });

            return Json(new { resault = "ok" });
        }

        public async Task<JsonResult> Logout(){
            HttpContext.SignOutAsync();
            return Json(new { resault = "ok" });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}