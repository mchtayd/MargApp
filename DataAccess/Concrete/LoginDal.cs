using DataAccess.Abstract;
using Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class LoginDal //: IRepository<Login>
    {
        AbsSqlService sqlService;
        SqlDataProcess sqlDataProcess;
        SqlDataReader dataReader;
        LogService logService;

        public LoginDal(AbsSqlService sqlService, LogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        public async Task<Login> AddAsync(string sessionSicilNo, Login entity)
        {
            try
            {
                await sqlService.StoredAsync("LOGINLER_Create", new SqlParameter("@sicil", entity.Sicil), new SqlParameter("@sifre", entity.Sifre));

                await logService.InfoAsync(nameof(AddAsync), $"Session Sicil No: {sessionSicilNo}", entity.ToString());
            }
            catch (Exception ex)
            {
                await logService.ErrorAsync(ex.Message, nameof(AddAsync), $"Session Sicil No: {sessionSicilNo}", entity.ToString());
                return null;
            }
            sqlDataProcess.CloseSqlConnection();
            return entity;
        }

        public async Task<bool> DeleteAsync(string sessionSicilNo, int id)
        {
            bool result = false;
            try
            {
                sqlDataProcess = await sqlService.StoreReaderAsync("LOGINLER_Delete", new SqlParameter("@id", id));
                dataReader = sqlDataProcess.SqlDataReader;
                if (dataReader.Read())
                {
                    result = dataReader["Result"].ConBool();
                }
                dataReader.Close();
                await logService.InfoAsync(nameof(DeleteAsync), "Id: " + id);
            }
            catch (Exception ex)
            {
                sqlService.CloseDataReader(dataReader);
                await logService.ErrorAsync(ex.Message, nameof(DeleteAsync), $"Session Sicil No: {sessionSicilNo}", "Id: " + id);
            }
            sqlDataProcess.CloseSqlConnection();
            return result;
        }

        public async Task<Login> LoginAsync(string sicil, string sifre)
        {
            Login login = null;
            try
            {
                sqlDataProcess = await sqlService.StoreReaderAsync("LOGINLER_GetSingle", new SqlParameter("@sicil", sicil), new SqlParameter("@sifre", sifre));
                dataReader = sqlDataProcess.SqlDataReader;
                while (dataReader.Read())
                {
                    login = GetLoginFromDataReader(dataReader);
                }
                dataReader.Close();
                return login;
            }
            catch (Exception ex)
            {
                sqlService.CloseDataReader(dataReader);
                await logService.ErrorAsync(ex.Message, nameof(LoginAsync), "Sicil: " + sicil);
            }
            sqlDataProcess.CloseSqlConnection();
            return login;
        }

        public async Task<List<Login>> GetListAsync()
        {
            List<Login> logins = null;
            try
            {
                sqlDataProcess = await sqlService.StoreReaderAsync("LOGINLER_GetAll");
                dataReader = sqlDataProcess.SqlDataReader;
                logins = new List<Login>();
                while (dataReader.Read())
                {
                    Login login = GetLoginFromDataReader(dataReader);
                    logins.Add(login);
                }
                dataReader.Close();
                await logService.InfoAsync(nameof(GetListAsync));
            }
            catch (Exception ex)
            {
                sqlService.CloseDataReader(dataReader);
                await logService.ErrorAsync(ex.Message, nameof(GetListAsync));
                return null;
            }
            sqlDataProcess.CloseSqlConnection();
            return logins;
        }

        public async Task<Login> UpdateAsync(string sessionSicilNo, Login entity)
        {
            try
            {
                sqlDataProcess = await sqlService.StoreReaderAsync("LOGINLER_Update", new SqlParameter("@sicil", entity.Sicil), new SqlParameter("@sifre", entity.Sifre));

                await logService.InfoAsync(nameof(UpdateAsync), $"Session Sicil No: {sessionSicilNo}", "Login -> " + entity.ToString());
            }
            catch (Exception ex)
            {
                await logService.ErrorAsync(ex.Message, nameof(UpdateAsync), $"Session Sicil No: {sessionSicilNo}", "Sicil: " + entity.Sicil);
                return null;
            }
            sqlDataProcess.CloseSqlConnection();
            return entity;
        }

        private Login GetLoginFromDataReader(SqlDataReader dataReader)
        {
            Login login = new(dataReader["Id"].ConInt()
                , dataReader["Sicil"].ToString());
            return login;

        }

        public static LoginDal GetInstance()
        {
            throw new NotImplementedException();
        }

        public Login Get(string sicil, string sifre)
        {
            throw new NotImplementedException();
        }
    }
}
