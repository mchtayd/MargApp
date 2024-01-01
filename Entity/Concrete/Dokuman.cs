using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Dokuman
    {
        int id; string dokumanNo, dokumanTanimi; DateTime yayinlanmaTarihi;

        public int Id { get => id; set => id = value; }
        public string DokumanNo { get => dokumanNo; set => dokumanNo = value; }
        public string DokumanTanimi { get => dokumanTanimi; set => dokumanTanimi = value; }
        public DateTime YayinlanmaTarihi { get => yayinlanmaTarihi; set => yayinlanmaTarihi = value; }

        public Dokuman(int id, string dokumanNo, string dokumanTanimi, DateTime yayinlanmaTarihi)
        {
            Id = id;
            DokumanNo = dokumanNo;
            DokumanTanimi = dokumanTanimi;
            YayinlanmaTarihi = yayinlanmaTarihi;
        }

        public Dokuman(string dokumanNo, string dokumanTanimi, DateTime yayinlanmaTarihi)
        {
            Id = id;
            DokumanNo = dokumanNo;
            DokumanTanimi = dokumanTanimi;
            YayinlanmaTarihi = yayinlanmaTarihi;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Dokuman No: {DokumanNo}, Dokuman Tanimi: {DokumanTanimi}, Yayinlama Tarihi: {YayinlanmaTarihi}";
        }
    }
}
