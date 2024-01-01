using DataAccess.Abstract;
using DataAccess.Concreate.Database;
using Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concreate
{
    public class LoginDal //: IRepository<Login>
    {
        static LoginDal loginDal;
        SqlServices sqlServices;
        SqlDataReader dataReader;

        private LoginDal()
        {
            sqlServices = SqlDatabase.GetInstance();
        }

        public string Add(Login entity)
        {
            throw new NotImplementedException();
        }

        public string Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Login Get(string sicil, string sifre)
        {
            try
            {
                dataReader = sqlServices.StoreReader("LoginList", new SqlParameter("@sicil", sicil), new SqlParameter("@sifre", sifre));
                Login item = null;
                while (dataReader.Read())
                {
                    item = new Login(dataReader["ID"].ConInt(), dataReader["SICIL"].ToString(), dataReader["SIFRE"].ToString());
                }
                dataReader.Close();
                return item;
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
            services.AddSingleton<LoginDal, LoginDal>();
        }

        public static LoginDal GetInstance()
        {
            if (loginDal == null)
            {
                loginDal = new LoginDal();
            }
            return loginDal;
        }

    }
}
