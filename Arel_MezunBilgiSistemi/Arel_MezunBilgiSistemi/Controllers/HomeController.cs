using Arel_MezunBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Arel_MezunBilgiSistemi.Controllers
{
    public class HomeController : Controller
    {
        Context db = new Context();
        // GET: Home
        public ActionResult Index(int? Page)
        {
            var makale = db.Makalelers.OrderByDescending(x => x.MakaleId).ToPagedList(Page ?? 1, 5);
            return View(makale);
        }

        public ActionResult MakaleDetay(int id)
        {
            var mkl = db.Makalelers.Where(X => X.MakaleId == id).SingleOrDefault();
            if (mkl != null)
            {
                mkl.Okunma++;
            }
            return View(mkl);
        }

        public PartialViewResult KategoriWidget()
        {
            var kategoriler = db.Kategorilers.ToList();
            return PartialView(kategoriler);
        }

        public PartialViewResult PopulerMakaleler()
        {
            var mkl = db.Makalelers.OrderByDescending(x => x.Okunma).Take(3);
            return PartialView(mkl);
        }

        public PartialViewResult EtiketlerWidget()
        {
            var etk = db.Etiketlers.ToList();
            return PartialView(etk);
        }

        public ActionResult KategoriyeAitMakaleler(int id)
        {
            var mkl = db.Makalelers.Where(x => x.KategoriID == id).OrderByDescending(x => x.MakaleId).ToList();
            return View(mkl);
        }

        public ActionResult EtiketeAitMakaleler(int id)
        {
            var mkl = db.Makalelers.Where(x => x.Etiketlers.Any(e => e.EtiketId == id)).FirstOrDefault();
            return View(mkl);
        }

        [HttpPost]
        public JsonResult YorumYap(string yorum, int id)
        {
            if (!string.IsNullOrEmpty(yorum) && id > 0) 
            {
                int uyeid = (int)Session["uyeid"];
                db.Yorumlars.Add(new Yorumlar { Icerik = yorum, UyeID = uyeid, MakaleID = id, Tarihi = DateTime.Now });
                db.SaveChanges();
                return Json("Yorumunuz Başarılı Bir Şekilde Eklenmiştir", JsonRequestBehavior.AllowGet);
            }
            return Json("Yorumunuz Eklenirken Bir Hata Oluştu", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult YorumSil(int yorumID)
        {
            var yrm = db.Yorumlars.Where(x => x.YorumId == yorumID).SingleOrDefault();
            db.Yorumlars.Remove(yrm);
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult AramaYap(string aranan)
        {
            if (!string.IsNullOrEmpty(aranan))
            {
                var mkl = db.Makalelers.Where(x => x.Baslik.Contains(aranan)).ToList();
                if (mkl.Count == 0)
                {
                    ViewBag.NotFound = "Aramanızla Eşleşen Kayıt Bulunamadı!";
                    return View(mkl);
                }
                return View(mkl);
            }
            return View();
        }
        
    }
}