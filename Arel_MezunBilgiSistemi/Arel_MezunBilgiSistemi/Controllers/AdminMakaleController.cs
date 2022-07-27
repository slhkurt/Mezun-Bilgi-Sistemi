using Arel_MezunBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Arel_MezunBilgiSistemi.Controllers
{
    public class AdminMakaleController : Controller
    {
        Context db = new Context();
        // GET: AdminMakale
        public ActionResult Index()
        {
            return View(db.Makalelers.AsNoTracking().ToList());
        }

        public ActionResult Ekle()
        {
            ViewBag.ktg = db.Kategorilers.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Makaleler m,string Etiket,HttpPostedFileBase Foto)
        {
            if (Foto != null)
            {
                WebImage img = new WebImage(Foto.InputStream);
                FileInfo foto = new FileInfo(Foto.FileName);
                img.Resize(330, 232);
                img.Save(Server.MapPath("~/Content/Images/MakaleFoto/" + foto));
                m.Foto = "/Content/Images/MakaleFoto/" + foto;
            }

            if (Etiket != null)
            {
                string[] etk = Etiket.Split(',');
                foreach (var e in etk)
                {
                    var etiketlistesi = new Etiketler { EtiketAdi = e };
                    db.Etiketlers.Add(etiketlistesi);
                    m.Etiketlers.Add(etiketlistesi);
                }
                db.SaveChanges();
            }
            m.Okunma = 0;
            m.YayinTarih = DateTime.Now;
            m.UyeID = 1;
            db.Makalelers.Add(m);
            db.SaveChanges();
            return RedirectToAction("Index", "AdminMakale");
        }

        public ActionResult Duzenle(int id)
        {
            var mkl = db.Makalelers.Where(x => x.MakaleId == id).SingleOrDefault();
            return View(mkl);
        }

        [HttpPost]
        public ActionResult Duzenle(Makaleler m, string Etiket, HttpPostedFileBase Foto)
        {
            var mkl = db.Makalelers.Where(x => x.MakaleId == m.MakaleId).SingleOrDefault();
            if (System.IO.File.Exists(Server.MapPath(mkl.Foto)))
            {
                System.IO.File.Delete(Server.MapPath(mkl.Foto));
            }

            if (Foto != null)
            {
                WebImage img = new WebImage(Foto.InputStream);
                FileInfo foto = new FileInfo(Foto.FileName);
                img.Resize(330, 232);
                img.Save(Server.MapPath("~/Content/Images/MakaleFoto/" + foto));
                mkl.Foto = "/Content/Images/MakaleFoto/" + foto;
            }

            foreach (var e in mkl.Etiketlers.ToList())
            {
                db.Etiketlers.Remove(e);
            }

            if (Etiket != null)
            {
                string[] etk = Etiket.Split(',');
                foreach (var e in etk)
                {
                    var etiketlistesi = new Etiketler { EtiketAdi = e };
                    db.Etiketlers.Add(etiketlistesi);
                    mkl.Etiketlers.Add(etiketlistesi);
                }
                db.SaveChanges();
            }
            mkl.Baslik = m.Baslik;
            mkl.Icerik = m.Icerik;
            mkl.KategoriID = m.KategoriID;
            db.SaveChanges();


            return RedirectToAction("Index", "AdminMakale");
        }

        public ActionResult Detay(int id)
        {
            var mkl = db.Makalelers.Where(x => x.MakaleId == id).SingleOrDefault();
            return View(mkl);
        }

        public ActionResult Sil(int id)
        {
            var mkl = db.Makalelers.Where(x => x.MakaleId == id).SingleOrDefault();
            return View(mkl);
        }

        [HttpPost]
        public ActionResult Sil(Makaleler m)
        {
            var mkl = db.Makalelers.Where(x => x.MakaleId == m.MakaleId).SingleOrDefault();
            if (System.IO.File.Exists(Server.MapPath(mkl.Foto)))
            {
                System.IO.File.Delete(Server.MapPath(mkl.Foto));
            }

            foreach (var y in mkl.Yorumlars.ToList())
            {
                db.Yorumlars.Remove(y);
            }

            foreach (var e in mkl.Etiketlers.ToList())
            {
                db.Etiketlers.Remove(e);
            }

            db.Makalelers.Remove(mkl);
            db.SaveChanges();
            return RedirectToAction("Index", "AdminMakale");
        }
    }
}