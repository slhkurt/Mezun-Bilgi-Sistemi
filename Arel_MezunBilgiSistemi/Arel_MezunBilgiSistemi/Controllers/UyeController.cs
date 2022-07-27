using Arel_MezunBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Arel_MezunBilgiSistemi.Controllers
{
    public class UyeController : Controller
    {
        Context db = new Context();
        // GET: Uye
        public ActionResult Index(int id)
        {
            var uye = db.Uyelers.Where(X => X.UyeId == id).SingleOrDefault();
            if (uye == null)
            {
                return View("Hata");
            }
            if (uye.UyeId == Convert.ToInt32(Session["uyeid"]))
            {
                return View(uye);
            }
            else
            {
                return Hata();
            }
        }

        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KayitOl(Uyeler u)
        {
            if (u.AdiSoyadi != null && u.KullaniciAdi != null && u.Sifre != null && u.Email != null)
            {
                var email = db.Uyelers.Where(x => x.Email == u.Email).SingleOrDefault();
                if (email == null)
                {
                    string s = new SifrelemeAgoritmasi().Md5(u.Sifre);
                    u.Sifre = s;
                    u.KayitTarihi = DateTime.Now;
                    u.YetkiID = 3;

                    db.Uyelers.Add(u);
                    db.SaveChanges();

                    return RedirectToAction("GirisYap", "Uye");
                }
            }
            ViewBag.error = "Bu Email ile Kayıt Olamazsanız Lütfen Başka Bir Email İle Tekrar Deneyiniz";
            return View();
        }

        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(string kadi, string sifre)
        {
            if (!string.IsNullOrEmpty(kadi) && !string.IsNullOrEmpty(sifre))
            {
                string s = new SifrelemeAgoritmasi().Md5(sifre);
                var kuladi = db.Uyelers.Where(X => X.KullaniciAdi == kadi && X.Sifre == s).SingleOrDefault();
                if (kuladi != null)
                {
                    Session["adsoyad"] = kuladi.AdiSoyadi;
                    Session["kadi"] = kuladi.KullaniciAdi;
                    Session["email"] = kuladi.Email;
                    Session["uyeid"] = kuladi.UyeId;
                    Session["yetkiid"] = kuladi.YetkiID;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.hata = "Kullanıcı Adı veya Şifre Hatalı! Lütfen Tekrar Deneyiniz";
                }
            }
            return View(ViewBag.bos = "Kullanıcı Adı veya Şifre Boş Geçilemez!");
        }

        public ActionResult CikisYap()
        {
            Session.Clear();
            return RedirectToAction("GirisYap", "Uye");
        }

        public ActionResult Hata()
        {
            return View();
        }

        public ActionResult ProfilDuzenle(int id)
        {
            var uye = db.Uyelers.Where(x => x.UyeId == id).SingleOrDefault();
            if (uye == null)
            {
                return View("Hata");
            }
            if (uye.UyeId == Convert.ToInt32(Session["uyeid"])) 
            {
                return View(uye);
            }
            else
            {
                return View("Hata");
            }
        }

        [HttpPost]
        public ActionResult ProfilDuzenle(Uyeler u)
        {
            var uye = db.Uyelers.Where(x => x.UyeId == u.UyeId).SingleOrDefault();
            uye.AdiSoyadi = u.AdiSoyadi;
            uye.KullaniciAdi = u.KullaniciAdi;
            uye.Email = u.Email;
            db.SaveChanges();
            return View(uye);
        }

        [HttpPost]
        public ActionResult UyeFotoKaydet(int id, HttpPostedFileBase Foto)
        {
            var uye = db.Uyelers.Where(x => x.UyeId == id).SingleOrDefault();
            if (System.IO.File.Exists(Server.MapPath(uye.Foto)))
            {
                System.IO.File.Delete(Server.MapPath(uye.Foto));
            }

            if (Foto != null)
            {
                WebImage img = new WebImage(Foto.InputStream);
                FileInfo foto = new FileInfo(Foto.FileName);
                img.Resize(112, 122);
                img.Save(Server.MapPath("~/Content/Images/UyeFoto/" + foto));
                uye.Foto = "/Content/Images/UyeFoto/" + foto;
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { id = uye.UyeId });
        }

        [HttpPost]
        public ActionResult FotoSil(int id)
        {
            var uye = db.Uyelers.Where(x => x.UyeId == id).SingleOrDefault();
            if (System.IO.File.Exists(Server.MapPath(uye.Foto)))
            {
                System.IO.File.Delete(Server.MapPath(uye.Foto));

            }
            uye.Foto = null;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = uye.UyeId });
        }

        public ActionResult UyeEmail(int id)
        {
            var uye = db.Uyelers.Where(x => x.UyeId == id).SingleOrDefault();
            return View(uye);
        }

        [HttpPost]
        public JsonResult MevcutEmail(string eposta)
        {
            var eml = db.Uyelers.Where(X => X.Email == eposta).SingleOrDefault();
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("slhkurt2@gmail.com", "salihkurt5734.");
                client.EnableSsl = true;

                MailMessage msj = new MailMessage();
                msj.From = new MailAddress("slhkurt2@gmail.com");
                msj.To.Add(eml.Email);
                msj.Subject = "Şifreni Sıfırla";
                msj.IsBodyHtml = true;
                msj.Body = "<html><body style=\"font-family:arial;font-size:24px;margin:20px 20px;padding:10px;border:1px solid;text-align:center;\"><p> Merhaba " + eml.KullaniciAdi+ " Şireni Sıfırlama Talebinde Bulundun. Şifreni Sıfırlamak İçin Link'e Tıklayarak Şifreni Sıfırlaya Bilirsin</p><a href=\" http://localhost:50573/Uye/ParolaSifirla/ " + eml.UyeId+"\" style\"background-color:blue;padding:15px;text-decoration:none;font-size:14px;\">Şifreni Sıfırla</a><p> Şifrenizi Sıfırlama Talebinde Bulunmadıysanız Lütfen Bunu Bize Bildirin! Bir İşlem Yapmadığınız Taktirde Şifreniz Sıfırlanmayacaktır.</p></body></html>";

                client.Send(msj);
                return Json("Email "+eml.Email+" Adresinize Şifre Değiştirme Bağlantınız Gönderilmiştir", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(""+eml.Email+" Adresine Gönderilirken Bir Hata Oluştu", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ParolaSifirla(int id)
        {
            var uye = db.Uyelers.Where(x => x.UyeId == id).FirstOrDefault();
            return View(uye);
        }

        [HttpPost]
        public ActionResult ParolaSifirla(string p1, string p2, int id)
        {
            var uye = db.Uyelers.Where(x => x.UyeId == id).SingleOrDefault();
            if (p1 == p2) 
            {
                string yenisifre = new SifrelemeAgoritmasi().Md5(p1);
                uye.Sifre = yenisifre;
                db.SaveChanges();
                ViewBag.basarili = "Şifreniz Başarılı Bir Şekilde Değiştirilmiştir";
                return View(uye);
            }
            else
            {
                ViewBag.hata = "Şifreniz Uyuşmuyor! Dikkatli Bir Şekilde Tekrar Deneyiniz";
                return View(uye);
            }
        }
    }
}