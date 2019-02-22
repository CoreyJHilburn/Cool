using CoreyHallClub.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoreyHallClub.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Cool(Person P, string CoolName)
        {
            using (var C = new SqlConnection("Server=tcp:coreyhallclubdbserver.database.windows.net;Initial Catalog=CoreyHallClub_db;Persist Security Info=False;User ID=coreyhall27;Password=hC174377@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                if (CoolName != "")
                {
                    C.Execute(@"INSERT INTO[dbo].[Person]([Name])VALUES(@CoolName)", new { CoolName = CoolName.ToUpper() });
                    return Json(new { cool = true });
                }               
                return Json(new { cool = C.Query<Person>("SELECT * FROM person WHERE name = @Name", new { P.Name }).Any() });
            }
        }
    }
}