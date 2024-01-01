using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class DokumanDal //: IRepositoryAsync<Dokuman>
    {
        AbsSqlService sqlService;
        SqlDataReader dataReader;
        LogService logService;
        SqlDataProcess sqlDataProcess;

        public DokumanDal(AbsSqlService sqlService, LogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        public async Task<Dokuman> AddAsync(string sessionSicilNo, Dokuman entity)
        {
            try
            {
                await sqlService.StoredAsync("DOKUMANLAR_Create", new SqlParameter("@dokumanNo", entity.DokumanNo), new SqlParameter("@dokumanTanimi", entity.DokumanTanimi), new SqlParameter("@yayinlamaTarihi", entity.YayinlanmaTarihi));

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

        public async Task<bool> DeleteAsync(string sessionSicilNo, string id)
        {
            bool result = false;
            try
            {
                sqlDataProcess = await sqlService.StoreReaderAsync("DOKUMANLAR_Delete", new SqlParameter("@id", id));
                dataReader = sqlDataProcess.SqlDataReader;
                if (dataReader.Read())
                {
                    result = dataReader["Result"].ConBool();
                }
                dataReader.Close();
                await logService.InfoAsync(nameof(DeleteAsync), "Session Sici No" + sessionSicilNo, "Id: " + id);
            }
            catch (Exception ex)
            {
                sqlService.CloseDataReader(dataReader);
                await logService.ErrorAsync(ex.Message, nameof(DeleteAsync), $"Session Sicil No: {sessionSicilNo} Id: {id}");
            }
            sqlDataProcess.CloseSqlConnection();
            return result;
        }

        public async Task<Dokuman> GetAsync(string sessionSicilNo, string id)
        {
            Dokuman dokuman = null;
            try
            {
                sqlDataProcess = await sqlService.StoreReaderAsync("DOKUMANLAR_GetSingle", new SqlParameter("@id", id));
                dataReader = sqlDataProcess.SqlDataReader;
                while (dataReader.Read())
                {
                    dokuman = GetDokumanFromDataReader(dataReader);
                }
                dataReader.Close();
                return dokuman;
            }
            catch (Exception ex)
            {
                sqlService.CloseDataReader(dataReader);
                await logService.ErrorAsync(ex.Message, nameof(GetAsync), "Session Sicil: " + sessionSicilNo, "Id: " + id);
            }
            sqlDataProcess.CloseSqlConnection();
            return dokuman;
        }

        public async Task<List<Dokuman>> GetListAsync()
        {
            List<Dokuman> dokumanlar = null;
            try
            {
                sqlDataProcess = await sqlService.StoreReaderAsync("DOKUMANLAR_GetAll");
                dataReader = sqlDataProcess.SqlDataReader;
                dokumanlar = new List<Dokuman>();
                while (dataReader.Read())
                {
                    Dokuman dokuman = GetDokumanFromDataReader(dataReader);
                    dokumanlar.Add(dokuman);
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
            return dokumanlar;
        }

        public async Task<Dokuman> UpdateAsync(string sessionSicilNo, Dokuman entity, string oldName)
        {
            try
            {
                await sqlService.StoredAsync("DOKUMANLAR_Update", new SqlParameter("@id", entity.Id), new SqlParameter("@dokumanNo", entity.DokumanNo), new SqlParameter("@dokumanTanimi", entity.DokumanTanimi), new SqlParameter("@yayinlamaTarihi", entity.YayinlanmaTarihi));

                await logService.InfoAsync(nameof(UpdateAsync), $"Session Sicil No: {sessionSicilNo}", "Login -> " + entity.ToString());
            }
            catch (Exception ex)
            {
                await logService.ErrorAsync(ex.Message, nameof(UpdateAsync), "Session Sicil: " + sessionSicilNo, entity.ToString());
                return null;
            }
            sqlDataProcess.CloseSqlConnection();
            return entity;
        }

        private Dokuman GetDokumanFromDataReader(SqlDataReader dataReader)
        {
            Dokuman dokuman = new(dataReader["Id"].ConInt()
                , dataReader["DokumanNo"].ToString()
                , dataReader["DokumanTanimi"].ToString()
                , dataReader["YayinlamaTarihi"].ConDate()
                );
            return dokuman;

        }
    }
}
