using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LoginManager
    {
        private readonly LoginDal loginDal;

        public LoginManager(LoginDal loginDal)
        {
            this.loginDal = loginDal;
        }

        public async Task<Login> AddAsync(string sessionSicilNo, Login entity) => await loginDal.AddAsync(sessionSicilNo, entity);
        public async Task<Login> LoginAsync(string sicil, string sifre) => await loginDal.LoginAsync(sicil, sifre);
        public async Task<bool> DeleteAsync(string sessionSicilNo, int id) => await loginDal.DeleteAsync(sessionSicilNo, id);
        public async Task<Login> UpdateAsync(string sessionSicilNo, Login entity) => await loginDal.UpdateAsync(sessionSicilNo, entity);
        public async Task<List<Login>> GetListAsync() => await loginDal.GetListAsync();

    }
}
