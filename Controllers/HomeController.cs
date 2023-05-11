using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibraryNG.Models;
using System.Data;
using LibraryNG;


namespace LibraryNG.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult DashBoard()
    {
        return View();
    }
    [HttpPost]
    public JsonResult SaveMethod(InfoLibrary ifl)
    {
        string sql = string.Format(@"Insert into InfoLibrary(MaLoi, TenLoi, PhanLoaiNG, ChuThich) values(@MaLoi, @TenLoi, @PhanLoaiNG,@Chuthich)");
        int rows = DataProvider.ExecuteNonQuery(sql, new Dictionary<string, object>{
            { "@MaLoi", ifl.MaLoi },
            { "@TenLoi", ifl.TenLoi },
            { "@PhanLoaiNG", ifl.PhanLoaiNG },
            { "@Chuthich", ifl.ChuThich == null ? ifl.ChuThich = " ":ifl.ChuThich }
        });

        if (rows <= 0)
            return Json(new {resault = "Thêm thất bại"});
        return Json(new {resault = "ok"});
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
