using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity
{
    public class Login
    {
        int id; string sicil, sifre;

        public int Id { get => id; set => id = value; }
        public string Sicil { get => sicil; set => sicil = value; }
        public string Sifre { get => sifre; set => sifre = value; }

        public Login(int id, string sicil, string sifre)
        {
            this.id = id;
            this.sicil = sicil;
            this.sifre = sifre;
        }

        public Login(string sicil, string sifre)
        {
            this.sicil = sicil;
            this.sifre = sifre;
        }
    }
}
