using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoboGebV6.Models
{
    public class Racer
    {
        public int Id { get; set; }


        [Display(Name = "Ad Soyad")]
        public string Name { get; set; }

        [Display(Name = "Takım")]
        public string Team { get; set; }

        [Display(Name = "Bitirme Süresi")]
        [RegularExpression(@"^(\d+:)?[0-5]?\d:[0-5]\d$", ErrorMessage = "Geçerli bir süre girin (ss:dd:dd veya d:dd:dd)")]
        public string FinishTime { get; set; }




        [Display(Name = "Kategori")]
        public string Kategori { get; set; }

        [Display(Name = "Arac Ağırlığı")]
        public int? arac { get; set; }


        [Display(Name = "Tasarım Puanı")]
        public int? Tasarim { get; set; }

        [Display(Name = "Kod Puanı")]
        public int? Kod { get; set; }

        [Display(Name = "Toplam Puanı")]
        public double? Toplam { get; set; }




        [Display(Name = "etiket")]
        public string etiket { get; set; }



        [Display(Name = " Arac Resmi Ön")]
        public string ProfileImage1 { get; set; } = "default.jpg";

        [Display(Name = "Arac Resmi Üst")]
        public string ProfileImage2 { get; set; } = "default.jpg";

        [Display(Name = "Araç Resmi Arka")]
        public string ProfileImage3 { get; set; } = "default.jpg";

        [Display(Name = "Kayıt Tarihi")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public int TimeInSeconds
        {
            get
            {
                if (string.IsNullOrEmpty(FinishTime))
                    return int.MaxValue;

                var parts = FinishTime.Split(':');
                if (parts.Length == 3)
                    return int.Parse(parts[0]) * 3600 + int.Parse(parts[1]) * 60 + int.Parse(parts[2]);
                else if (parts.Length == 2)
                    return int.Parse(parts[0]) * 60 + int.Parse(parts[1]);
                return int.MaxValue;
            }
        }

    }
}