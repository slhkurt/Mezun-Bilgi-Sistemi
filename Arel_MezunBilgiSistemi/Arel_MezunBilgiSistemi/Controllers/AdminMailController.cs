using Arel_MezunBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Arel_MezunBilgiSistemi.Controllers
{
    public class AdminMailController : Controller
    {
        Context db = new Context();
        // GET: AdminMail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MailGonder(int id)
        {
            var uye = db.Uyelers.Where(x => x.UyeId == id).FirstOrDefault();
            return View(uye);
        }

        [HttpPost]
        public ActionResult MailGonder(int id, string Mail, string Konu, string Mesaj)
        {
            var uyeler = db.Uyelers.Where(u => u.UyeId == id).FirstOrDefault();
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("slhkurt2@gmail.com", "salihkurt5734.");
                client.EnableSsl = true;
                MailMessage msj = new MailMessage();
                msj.From = new MailAddress("slhkurt2@gmail.com", "Salih Kurt");
                msj.To.Add(new MailAddress(uyeler.Email));
                msj.Subject = Konu;
                msj.Priority = MailPriority.Normal;
                msj.Body = Mesaj;
                client.Send(msj);
                ViewBag.succes = "Mail Başarılı Bir Şekilde Gönderilmiştir.";
                return View(uyeler);
            }
            catch (Exception)
            {
                ViewBag.hata = "Mail gönderilirken bir hata oluştu!";
                return View(uyeler);
            }
        }

        public ActionResult TopluMailGonder()
        {
            var uyeler = db.Uyelers.ToList();
            return View(uyeler);
        }

        [HttpPost]
        public ActionResult TopluMailGonder(string Konu, string Mesaj)
        {
            var uyeler = db.Uyelers.ToList();
            Thread.Sleep(1000);
            string donenDeger = Yavaslat(Konu, Mesaj);
            if (donenDeger == "Başarılı")
            {
                ViewBag.succes = "Mail Başarılı Bir Şekilde Gönderildi";
                return View(uyeler);
            }
            else
            {
                ViewBag.hata = "Mail Gönderilirken Bir Hata Oluştu!";
                return View(uyeler);
            }
        }

        private string Yavaslat(string Konu, string Mesaj)
        {
            var uyeler = db.Uyelers.ToList();
            try
            {
                foreach (var mail in uyeler)
                {
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.Credentials = new NetworkCredential("slhkurt2@gmail.com", "salihkurt5734.");
                    client.EnableSsl = true;
                    MailMessage msj = new MailMessage();
                    msj.From = new MailAddress("slhkurt2@gmail.com", "Salih Kurt");
                    msj.To.Add(new MailAddress(mail.Email));
                    msj.Subject = Konu;
                    msj.Body = Mesaj;
                    client.Send(msj);
                }
                return "Başarılı";
            }
            catch (Exception)
            {
                return "Hata";
            }
        }
    }
}