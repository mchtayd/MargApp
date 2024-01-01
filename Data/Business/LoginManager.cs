using DataAccess.Abstract;
using DataAccess.Concreate;
using Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class LoginManager //: IRepository<Login>
    {
        static LoginManager loginManager;
        LoginDal loginDal;

        private LoginManager()
        {
            loginDal = LoginDal.GetInstance();
        }

        public string Add(Login entity)
        {
            throw new NotImplementedException();
        }

        public string Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Login Get(string sicil,string sifre)
        {
            try
            {
                return loginDal.Get(sicil, sifre);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Login> GetList()
        {
            throw new NotImplementedException();
        }

        public string Update(Login entity)
        {
            throw new NotImplementedException();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<LoginManager, LoginManager>();
        }

        public static LoginManager GetInstance()
        {
            if (loginManager == null)
            {
                loginManager = new LoginManager();
            }
            return loginManager;
        }
    }
}
