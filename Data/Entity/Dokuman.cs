using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity
{
    public class Dokuman
    {
        int id; string dokumanNo, dokumanTanimi; DateTime yayinlanmaTarihi;

        public int Id { get => id; set => id = value; }
        public string DokumanNo { get => dokumanNo; set => dokumanNo = value; }
        public string DokumanTanimi { get => dokumanTanimi; set => dokumanTanimi = value; }
        public DateTime YayinlanmaTarihi { get => yayinlanmaTarihi; set => yayinlanmaTarihi = value; }
    }
}
