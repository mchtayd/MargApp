using System.ComponentModel.DataAnnotations;

namespace MVC_WebInterface.Models.DTO
{
    public class PersonelDTO
    {
        [Key]
        [Display(Name = "Sicil No")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Lütfen Ad Soyad Belirtiniz")]
        [Display(Name = "Ad Soyad")]
        public string PersonelAdSoyad { get; set; }

        public string Tc { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [Required(ErrorMessage = "Lütfen Doğum Tarihi Belirtiniz")]
        public DateTime DogumTarihi { get; set; }

        [Display(Name = "Doğum Yeri")]
        public string DogumYeri { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Lütfen Telefon No Belirtiniz")]
        public string Telefon { get; set; }

        [Display(Name = "İkamet Adresi")]
        public string IkametAdresi { get; set; }

        [Display(Name = "Mezuniyet")]
        public Mezuniyet MezuniyetBilgisi { get; set; }

        [Required(ErrorMessage = "Lütfen Şifre Belirtiniz")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }

    public enum Mezuniyet
    {
        [Display(Name = " Hepsi")]
        All = 0,
        [Display(Name = " İlkokul")]
        Ilkokul,
        Ortaokul,
        Lise,
        Üniversite,
        [Display(Name = " Yüksek Lisans")]
        YüksekLisans
    }
}
