using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoboGebV6.Models;

namespace RoboGebV6.Controllers
{
    [Authorize]
    public class RacersController : Controller
    {
        private RacerContext db = new RacerContext();
        // GET: Racers




        [AllowAnonymous]
        public ActionResult izleyicitemelsort(string sortOrder, int? lastAddedId)
        {


            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TimeSortParm = sortOrder == "Time" ? "time_desc" : "Time";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Team";

            if (lastAddedId.HasValue)
            {
                ViewBag.LastAddedId = lastAddedId.Value;
            }

            // Tüm verileri önce bellege al
            //var allRacers = db.Racers.ToList();

            var allRacers = db.Racers.Where(r => r.Kategori == "Temel Çizgi İzleyen").ToList();

            // TimeInSeconds hesapla ve sırala
            var racers = allRacers.Select(r => new
            {
                Racer = r,
                TimeInSeconds = CalculateTimeInSeconds(r.FinishTime)
            });


            //Toplam Puan Hesaplama - Tasarla Çalıştır



            // Sıralamayı yap
            switch (sortOrder)
            {
                case "name_desc": racers = racers.OrderByDescending(x => x.Racer.Name); break;
                case "Time": racers = racers.OrderBy(x => x.TimeInSeconds); break;
                case "time_desc": racers = racers.OrderByDescending(x => x.TimeInSeconds); break;
                case "Team": racers = racers.OrderBy(x => x.Racer.Team); break;
                case "team_desc": racers = racers.OrderByDescending(x => x.Racer.Team); break;
                default: racers = racers.OrderBy(x => x.TimeInSeconds); break;
            }

            // Sonucu modele dönüştür
            var result = racers.Select(x => x.Racer).ToList();

            return View(result);
        }

        [AllowAnonymous]
        public ActionResult izleyicitasarlasort(string sortOrder, int? lastAddedId)
        {


            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TimeSortParm = sortOrder == "Time" ? "time_desc" : "Time";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Team";

            if (lastAddedId.HasValue)
            {
                ViewBag.LastAddedId = lastAddedId.Value;
            }

            // Tüm verileri önce bellege al
            //var allRacers = db.Racers.ToList();

            var allRacers = db.Racers.Where(r => r.Kategori == "Tasarla Çalıştır").ToList();

            foreach (var tasarla in allRacers)
            {
                tasarla.Toplam = (tasarla.Kod + tasarla.Tasarim) / 2.0;
                db.Entry(tasarla).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();

            // TimeInSeconds hesapla ve sırala
            var racers = allRacers.Select(r => new
            {
                Racer = r,
                TimeInSeconds = CalculateTimeInSeconds(r.FinishTime)
            });

            // Sıralamayı yap
            switch (sortOrder)
            {
                case "name_desc": racers = racers.OrderByDescending(x => x.Racer.Name); break;
                case "Time": racers = racers.OrderBy(x => x.TimeInSeconds); break;
                case "time_desc": racers = racers.OrderByDescending(x => x.TimeInSeconds); break;
                case "Team": racers = racers.OrderBy(x => x.Racer.Team); break;
                case "team_desc": racers = racers.OrderByDescending(x => x.Racer.Team); break;
                default: racers = racers.OrderBy(x => x.TimeInSeconds); break;
            }

            // Sonucu modele dönüştür
            var result = racers.Select(x => x.Racer).ToList();

            return View(result);
        }

        [AllowAnonymous]
        public ActionResult izleyiciracersort(string sortOrder, int? lastAddedId)
        {


            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TimeSortParm = sortOrder == "Time" ? "time_desc" : "Time";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Team";

            if (lastAddedId.HasValue)
            {
                ViewBag.LastAddedId = lastAddedId.Value;
            }

            // Tüm verileri önce bellege al
            //var allRacers = db.Racers.ToList();

            var allRacers = db.Racers.Where(r => r.Kategori == "Hızlı Çizgi İzleyen").ToList();

            // TimeInSeconds hesapla ve sırala
            var racers = allRacers.Select(r => new
            {
                Racer = r,
                TimeInSeconds = CalculateTimeInSeconds(r.FinishTime)
            });

            // Sıralamayı yap
            switch (sortOrder)
            {
                case "name_desc": racers = racers.OrderByDescending(x => x.Racer.Name); break;
                case "Time": racers = racers.OrderBy(x => x.TimeInSeconds); break;
                case "time_desc": racers = racers.OrderByDescending(x => x.TimeInSeconds); break;
                case "Team": racers = racers.OrderBy(x => x.Racer.Team); break;
                case "team_desc": racers = racers.OrderByDescending(x => x.Racer.Team); break;
                default: racers = racers.OrderBy(x => x.TimeInSeconds); break;
            }

            // Sonucu modele dönüştür
            var result = racers.Select(x => x.Racer).ToList();

            return View(result);
        }

        [HttpGet]
        public ActionResult Kontrol()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kontrol(string etiket)
        {
            var yarismaci = db.Racers.FirstOrDefault(y => y.etiket == etiket);

            if (yarismaci == null)
            {
                ViewBag.Message = "Etikete ait yarışmacı bulunamadı.";
                return View();
            }

            return View(yarismaci);
        }


        public ActionResult Siralama()
        {
            // Veritabanından yarışmacıları çek
            var yarismacilar = db.Racers.ToList();
            return View(yarismacilar);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult racersort(string sortOrder, int? lastAddedId)
        {


            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TimeSortParm = sortOrder == "Time" ? "time_desc" : "Time";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Team";

            if (lastAddedId.HasValue)
            {
                ViewBag.LastAddedId = lastAddedId.Value;
            }

            // Tüm verileri önce bellege al
            //var allRacers = db.Racers.ToList();

            var allRacers = db.Racers.Where(r => r.Kategori == "Hızlı Çizgi İzleyen").ToList();

            // TimeInSeconds hesapla ve sırala
            var racers = allRacers.Select(r => new
            {
                Racer = r,
                TimeInSeconds = CalculateTimeInSeconds(r.FinishTime)
            });

            // Sıralamayı yap
            switch (sortOrder)
            {
                case "name_desc": racers = racers.OrderByDescending(x => x.Racer.Name); break;
                case "Time": racers = racers.OrderBy(x => x.TimeInSeconds); break;
                case "time_desc": racers = racers.OrderByDescending(x => x.TimeInSeconds); break;
                case "Team": racers = racers.OrderBy(x => x.Racer.Team); break;
                case "team_desc": racers = racers.OrderByDescending(x => x.Racer.Team); break;
                default: racers = racers.OrderBy(x => x.TimeInSeconds); break;
            }

            // Sonucu modele dönüştür
            var result = racers.Select(x => x.Racer).ToList();

            return View(result);
        }

        public ActionResult tasarlasort(string sortOrder, int? lastAddedId)
        {


            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TimeSortParm = sortOrder == "Time" ? "time_desc" : "Time";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Team";

            if (lastAddedId.HasValue)
            {
                ViewBag.LastAddedId = lastAddedId.Value;
            }

            // Tüm verileri önce bellege al
            //var allRacers = db.Racers.ToList();

            var allRacers = db.Racers.Where(r => r.Kategori == "Tasarla Çalıştır").ToList();

            foreach (var tasarla in allRacers)
            {
                tasarla.Toplam = (tasarla.Kod + tasarla.Tasarim) / 2.0;
                db.Entry(tasarla).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();

            // TimeInSeconds hesapla ve sırala
            var racers = allRacers.Select(r => new
            {
                Racer = r,
                TimeInSeconds = CalculateTimeInSeconds(r.FinishTime)
            });

            // Sıralamayı yap
            switch (sortOrder)
            {
                case "name_desc": racers = racers.OrderByDescending(x => x.Racer.Name); break;
                case "Time": racers = racers.OrderBy(x => x.TimeInSeconds); break;
                case "time_desc": racers = racers.OrderByDescending(x => x.TimeInSeconds); break;
                case "Team": racers = racers.OrderBy(x => x.Racer.Team); break;
                case "team_desc": racers = racers.OrderByDescending(x => x.Racer.Team); break;
                default: racers = racers.OrderBy(x => x.TimeInSeconds); break;
            }

            // Sonucu modele dönüştür
            var result = racers.Select(x => x.Racer).ToList();

            return View(result);
        }


        public ActionResult temelsort(string sortOrder, int? lastAddedId)
        {


            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TimeSortParm = sortOrder == "Time" ? "time_desc" : "Time";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Team";

            if (lastAddedId.HasValue)
            {
                ViewBag.LastAddedId = lastAddedId.Value;
            }

            // Tüm verileri önce bellege al
            //var allRacers = db.Racers.ToList();

            var allRacers = db.Racers.Where(r => r.Kategori == "Temel Çizgi İzleyen").ToList();

            // TimeInSeconds hesapla ve sırala
            var racers = allRacers.Select(r => new
            {
                Racer = r,
                TimeInSeconds = CalculateTimeInSeconds(r.FinishTime)
            });


            //Toplam Puan Hesaplama - Tasarla Çalıştır



            // Sıralamayı yap
            switch (sortOrder)
            {
                case "name_desc": racers = racers.OrderByDescending(x => x.Racer.Name); break;
                case "Time": racers = racers.OrderBy(x => x.TimeInSeconds); break;
                case "time_desc": racers = racers.OrderByDescending(x => x.TimeInSeconds); break;
                case "Team": racers = racers.OrderBy(x => x.Racer.Team); break;
                case "team_desc": racers = racers.OrderByDescending(x => x.Racer.Team); break;
                default: racers = racers.OrderBy(x => x.TimeInSeconds); break;
            }

            // Sonucu modele dönüştür
            var result = racers.Select(x => x.Racer).ToList();

            return View(result);
        }
        private int CalculateTimeInSeconds(string finishTime)
        {
            if (string.IsNullOrEmpty(finishTime))
                return int.MaxValue;

            var parts = finishTime.Split(':');

            if (parts.Length == 3) // ss:dd:dd formatı
                return int.Parse(parts[0]) * 3600 + int.Parse(parts[1]) * 60 + int.Parse(parts[2]);

            if (parts.Length == 2) // d:dd:dd formatı
                return int.Parse(parts[0]) * 60 + int.Parse(parts[1]);

            return int.MaxValue;
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Racer racer, HttpPostedFileBase ProfileImage1, HttpPostedFileBase ProfileImage2, HttpPostedFileBase ProfileImage3)
        {
         
            if (racer.Kategori == "Tasarla Çalıştır")
            {

                if (ModelState.IsValid)
                {
                    db.Racers.Add(racer);
                    db.SaveChanges();

                    // Yeni eklenen yarışmacının ID'sini ViewBag ile gönder
                    ViewBag.LastAddedId = racer.Id;
                    return RedirectToAction("Create", new { lastAddedId = racer.Id });
                }

            }

            var yarismaci = db.Racers.FirstOrDefault(y => y.etiket == racer.etiket);

            if (yarismaci != null)
            {
                ViewBag.Message = "Aynı etikete sahip yarışmacı var.";
                return View();
            }

            if (ProfileImage1 != null && ProfileImage1.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ProfileImage1.FileName);
                string path = Path.Combine(Server.MapPath("~/img"), fileName);
                ProfileImage1.SaveAs(path);
                racer.ProfileImage1 = "/img/" + fileName;
            }


            if (ProfileImage2 != null && ProfileImage2.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ProfileImage2.FileName);
                string path = Path.Combine(Server.MapPath("~/img"), fileName);
                ProfileImage2.SaveAs(path);
                racer.ProfileImage2 = "/img/" + fileName;
            }


            if (ProfileImage3 != null && ProfileImage3.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ProfileImage3.FileName);
                string path = Path.Combine(Server.MapPath("~/img"), fileName);
                ProfileImage3.SaveAs(path);
                racer.ProfileImage3 = "/img/" + fileName;
            }

            if (ModelState.IsValid)
            {
                db.Racers.Add(racer);
                db.SaveChanges();

                // Yeni eklenen yarışmacının ID'sini ViewBag ile gönder
                ViewBag.LastAddedId = racer.Id;
                return RedirectToAction("Create", new { lastAddedId = racer.Id });
            }

            return View(racer);




        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            var racer = db.Racers.Find(id);
            if (racer == null)
            {
                return HttpNotFound("Yarışmacı bulunamadı.");
            }

            db.Racers.Remove(racer);
            db.SaveChanges();

            return RedirectToAction("Index"); ;
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Racer racer = db.Racers.Find(id);
            if (racer == null) return HttpNotFound();
            return View(racer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Racer racer)
        {
            if (ModelState.IsValid)
            {
                var existingRacer = db.Racers.Find(racer.Id);
                if (existingRacer == null)
                {
                    return HttpNotFound();
                }

                // Sadece belirli alanları güncelle
                db.Entry(existingRacer).Property(r => r.Name).CurrentValue = racer.Name;
                db.Entry(existingRacer).Property(r => r.Team).CurrentValue = racer.Team;
                db.Entry(existingRacer).Property(r => r.FinishTime).CurrentValue = racer.FinishTime;


                db.Entry(existingRacer).Property(r => r.Name).IsModified = true;
                db.Entry(existingRacer).Property(r => r.Team).IsModified = true;
                db.Entry(existingRacer).Property(r => r.FinishTime).IsModified = true;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(racer);



            /*
            ModelState.Remove("arac");
            ModelState.Remove("Kategori");
            ModelState.Remove("Tasarim");
            ModelState.Remove("Kod");
            ModelState.Remove("Toplam");
            ModelState.Remove("ProfileImage1");
            ModelState.Remove("ProfileImage2");
            ModelState.Remove("ProfileImage3");
            ModelState.Remove("etiket");
            ModelState.Remove("RegistrationDate");

            if (ModelState.IsValid)
            {
                db.Entry(racer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(racer);

            */
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }










        public ActionResult tasarlaedit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Racer racer = db.Racers.Find(id);
            if (racer == null) return HttpNotFound();
            return View(racer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]



        public ActionResult tasarlaedit(Racer racer)
        {
            if (ModelState.IsValid)
            {
                var existingRacer = db.Racers.Find(racer.Id);
                if (existingRacer == null)
                {
                    return HttpNotFound();
                }

                // Sadece belirli alanları güncelle
                db.Entry(existingRacer).Property(r => r.Kod).CurrentValue = racer.Kod;
                db.Entry(existingRacer).Property(r => r.Tasarim).CurrentValue = racer.Tasarim;
                db.Entry(existingRacer).Property(r => r.Toplam).CurrentValue = racer.Toplam;


                db.Entry(existingRacer).Property(r => r.Kod).IsModified = true;
                db.Entry(existingRacer).Property(r => r.Tasarim).IsModified = true;
                db.Entry(existingRacer).Property(r => r.Toplam).IsModified = true;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(racer);



            /*
            ModelState.Remove("arac");
            ModelState.Remove("Kategori");
            ModelState.Remove("Tasarim");
            ModelState.Remove("Kod");
            ModelState.Remove("Toplam");
            ModelState.Remove("ProfileImage1");
            ModelState.Remove("ProfileImage2");
            ModelState.Remove("ProfileImage3");
            ModelState.Remove("etiket");
            ModelState.Remove("RegistrationDate");

            if (ModelState.IsValid)
            {
                db.Entry(racer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(racer);

            */
        }







    }
}