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
    public IActionResult DashBoard(string giatritimkiem)
    {
        string query = @"select *
                        from InfoLibrary
                        where MaLoi like @X or TenLoi like @X or PhanLoaiNG like @X or ChuThich like @X";
        DataTable dt = DataProvider.ExcuteQuery(query, new Dictionary<string, object>{
            {"@X", "%"+giatritimkiem+"%"}
        });
        List<InfoLibrary> librariesList = new List<InfoLibrary>();
        foreach (DataRow row in dt.Rows)
        {
            InfoLibrary library = new InfoLibrary();
            library.MaLoi = row["MaLoi"].ToString();
            library.TenLoi = row["TenLoi"].ToString();
            library.PhanLoaiNG  = row["PhanLoaiNG"].ToString();
            library.ChuThich = row["ChuThich"].ToString();
            librariesList.Add(library);
        }
        ViewData["Library"] = librariesList;
        return View();
    }
    [HttpPost]
    public JsonResult SaveMethod(InfoLibrary ifl)
    {
            string sql = string.Format(@"Insert into InfoLibrary(MaLoi, TenLoi, PhanLoaiNG, ChuThich) values(@MaLoi, @TenLoi, @PhanLoaiNG, @Chuthich)");
            int rows = DataProvider.ExecuteNonQuery(sql, new Dictionary<string, object>{
                { "@MaLoi", ifl.MaLoi },
                { "@TenLoi", ifl.TenLoi },
                { "@PhanLoaiNG", ifl.PhanLoaiNG },
                { "@Chuthich", ifl.ChuThich == null ? null : ifl.ChuThich },
            });
            if (rows <= 0)
                return Json(new { resault = "Thêm thất bại" });
            return Json(new {resault = "ok"});
    }
    [HttpPost]
    public JsonResult UpdateMethod(InfoLibrary ifl)
    {
        string query = string.Format(@"UPDATE InfoLibrary SET MaLoi = @MaLoi, TenLoi = @TenLoi, PhanLoaiNG = @PhanLoaiNG, ChuThich = @Chuthich WHERE MaLoi = @MaLoi");
        int rows = DataProvider.ExecuteNonQuery(query, new Dictionary<string, object>{
            { "@MaLoi", ifl.MaLoi },
            { "@TenLoi", ifl.TenLoi },
            { "@PhanLoaiNG", ifl.PhanLoaiNG },
            { "@Chuthich", ifl.ChuThich == null ? null : ifl.ChuThich },
        });
        if (rows <= 0)
            return Json(new { resault = "Cập nhật thất bại" });
        return Json(new { resault = "ok" });
    }
    [HttpPost]
    public JsonResult DeleteMethod(InfoLibrary ifl)
    {
        string query = string.Format(@"delete from InfoLibrary where MaLoi = @MaLoi");
        int rows = DataProvider.ExecuteNonQuery(query, new Dictionary<string, object>{
            {"@MaLoi", ifl.MaLoi}
        });
        if (rows <= 0)
            return Json(new { resault = "Xóa thất bại" });
        return Json(new {resault = "ok"});
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
