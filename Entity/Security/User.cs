using Entity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Security
{
    public class User
    {
        private string userName, password;
        private int id;

        public string UserName { get => userName; set => userName = value; }
        public int Id { get => id; set => id = value; }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

    }
}
