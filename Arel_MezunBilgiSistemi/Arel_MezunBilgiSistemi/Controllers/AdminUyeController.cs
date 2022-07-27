using Arel_MezunBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arel_MezunBilgiSistemi.Controllers
{
    public class AdminUyeController : Controller
    {
        Context db = new Context();
        // GET: AdminUye
        public ActionResult Index()
        {
            var uye = db.Uyelers.ToList();
            return View(uye);
        }

        public ActionResult Detay(int id)
        {
            var uye = db.Uyelers.Where(x => x.UyeId == id).SingleOrDefault();
            return View(uye);
        }

        public ActionResult Sil(int id)
        {
            var uye = db.Uyelers.Where(x => x.UyeId == id).SingleOrDefault();
            return View(uye);
        }

        [HttpPost]
        public ActionResult Sil(Uyeler u)
        {
            var uye = db.Uyelers.Where(x => x.UyeId == u.UyeId).SingleOrDefault();

            if (System.IO.File.Exists(Server.MapPath(uye.Foto)))
            {
                System.IO.File.Delete(uye.Foto);
            }

            foreach (var y in uye.Yorumlars.ToList())
            {
                db.Yorumlars.Remove(y);
            }
            db.SaveChanges();

            db.Uyelers.Remove(uye);
            db.SaveChanges();

            return RedirectToAction("Index", "AdminUye");
        }
    }
}