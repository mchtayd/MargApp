using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity
{
    public class Personel
    {
        int id; string personelAdSoyad, tc; DateTime dogumTarihi; string dogumYeri, telefon, ikametAdresi, mezuniyetBilgisi;

        public int Id { get => id; set => id = value; }
        public string PersonelAdSoyad { get => personelAdSoyad; set => personelAdSoyad = value; }
        public string Tc { get => tc; set => tc = value; }
        public DateTime DogumTarihi { get => dogumTarihi; set => dogumTarihi = value; }
        public string DogumYeri { get => dogumYeri; set => dogumYeri = value; }
        public string Telefon { get => telefon; set => telefon = value; }
        public string IkametAdresi { get => ikametAdresi; set => ikametAdresi = value; }
        public string MezuniyetBilgisi { get => mezuniyetBilgisi; set => mezuniyetBilgisi = value; }

        public Personel(int id, string personelAdSoyad, string tc, DateTime dogumTarihi, string dogumYeri, string telefon, string ikametAdresi, string mezuniyetBilgisi)
        {
            this.id = id;
            this.personelAdSoyad = personelAdSoyad;
            this.tc = tc;
            this.dogumTarihi = dogumTarihi;
            this.dogumYeri = dogumYeri;
            this.telefon = telefon;
            this.ikametAdresi = ikametAdresi;
            this.mezuniyetBilgisi = mezuniyetBilgisi;
        }

        public Personel(string personelAdSoyad, string tc, DateTime dogumTarihi, string dogumYeri, string telefon, string ikametAdresi, string mezuniyetBilgisi)
        {
            this.personelAdSoyad = personelAdSoyad;
            this.tc = tc;
            this.dogumTarihi = dogumTarihi;
            this.dogumYeri = dogumYeri;
            this.telefon = telefon;
            this.ikametAdresi = ikametAdresi;
            this.mezuniyetBilgisi = mezuniyetBilgisi;
        }
    }
}
