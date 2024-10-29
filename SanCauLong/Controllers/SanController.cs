using SanCauLong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanCauLong.Controllers
{
    public class SanController : Controller
    {
        // GET: San
        SanCauLongEntities db = new SanCauLongEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachSan()
        {
            var sanCauLongs = db.SanCauLongs.ToList();
            return View(sanCauLongs);
        }
    }
}