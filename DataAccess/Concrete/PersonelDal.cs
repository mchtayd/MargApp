using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class PersonelDal //: IRepositoryAsync<Personel>
    {
        AbsSqlService sqlService;
        SqlDataProcess sqlDataProcess;
        SqlDataReader dataReader;
        LogService logService;

        public PersonelDal(AbsSqlService sqlService, LogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        public async Task<Personel> AddAsync(string sessionSicilNo, Personel entity)
        {
            try
            {
                await sqlService.StoredAsync("PERSONELLER_Create", new SqlParameter("@sicil", entity.Sicil), new SqlParameter("@adSoyad", entity.PersonelAdSoyad), new SqlParameter("@telefon", entity.Telefon), new SqlParameter("@dogumTarihi", entity.DogumTarihi), new SqlParameter("@tc", entity.Tc), new SqlParameter("@dogumYeri", entity.DogumYeri), new SqlParameter("@ikametAdresi", entity.IkametAdresi), new SqlParameter("@mezuniyet", entity.MezuniyetBilgisi));

                await logService.InfoAsync(nameof(AddAsync), $"Session Sicil No: {sessionSicilNo}", entity.ToString());
            }
            catch (Exception ex)
            {
                await logService.ErrorAsync(ex.Message, nameof(AddAsync), $"Session Sicil No: {sessionSicilNo}", entity.ToString());
                return null;
            }
            sqlDataProcess?.CloseSqlConnection();
            return entity;
        }

        public async Task<bool> DeleteAsync(string sessionSicilNo, string sicil)
        {
            bool result = false;
            try
            {
                sqlDataProcess = await sqlService.StoreReaderAsync("PERSONELLER_Delete", new SqlParameter("@sicil", sicil));
                dataReader = sqlDataProcess.SqlDataReader;
                if (dataReader.Read())
                {
                    result = dataReader["Result"].ConBool();
                }
                dataReader.Close();
                await logService.InfoAsync(nameof(DeleteAsync), "Sicil: " + sicil);
            }
            catch (Exception ex)
            {
                sqlService.CloseDataReader(dataReader);
                await logService.ErrorAsync(ex.Message, nameof(DeleteAsync), $"Session Sicil No: {sessionSicilNo}", "Sicil: " + sicil);
            }
            sqlDataProcess.CloseSqlConnection();
            return result;
        }

        public async Task<Personel> GetAsync(string sessionSicilNo, string sicil)
        {
            Personel personel = null;
            try
            {
                sqlDataProcess = await sqlService.StoreReaderAsync("PERSONELLER_GetSingle", new SqlParameter("@sicil", sicil));
                dataReader = sqlDataProcess.SqlDataReader;
                if (dataReader.Read())
                {
                    personel = GetPersonelFromDataReader(dataReader);
                }
                dataReader.Close();
                await logService.InfoAsync(nameof(GetAsync), $"Session Sicil No: {sessionSicilNo}", "Sicil: " + sicil);
            }
            catch (Exception ex)
            {
                sqlService.CloseDataReader(dataReader);
                await logService.ErrorAsync(ex.Message, nameof(GetAsync), $"Session Sicil No: {sessionSicilNo}", "Sicil: " + sicil);
                return null;
            }
            sqlDataProcess.CloseSqlConnection();
            return personel;
        }

        public async Task<List<Personel>> GetListAsync()
        {
            List<Personel> personels = null;
            try
            {
                sqlDataProcess = await sqlService.StoreReaderAsync("PERSONELLER_GetAll");
                dataReader = sqlDataProcess.SqlDataReader;
                personels = new List<Personel>();
                while (dataReader.Read())
                {
                    Personel personel = GetPersonelFromDataReader(dataReader);
                    personels.Add(personel);
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
            return personels;
        }

        public async Task<Personel> UpdateAsync(string sessionSicilNo, Personel entity, string oldName)
        {
            try
            {
                await sqlService.StoredAsync("PERSONELLER_Update", new SqlParameter("@sicil", entity.Sicil), new SqlParameter("@adSoyad", entity.PersonelAdSoyad), new SqlParameter("@telefon", entity.Telefon), new SqlParameter("@dogumTarihi", entity.DogumTarihi), new SqlParameter("@tc", entity.Tc), new SqlParameter("@dogumYeri", entity.DogumYeri), new SqlParameter("@ikametAdresi", entity.IkametAdresi), new SqlParameter("@mezuniyet", entity.MezuniyetBilgisi));

                await logService.InfoAsync(nameof(UpdateAsync), $"Session Sicil No: {sessionSicilNo}", "Personel -> " + entity.ToString());
            }
            catch (Exception ex)
            {
                await logService.ErrorAsync(ex.Message, nameof(UpdateAsync), $"Session Sicil No: {sessionSicilNo},", "Personel -> " + entity.ToString());
                return null;
            }
            sqlDataProcess?.CloseSqlConnection();
            return entity;
        }

        private Personel GetPersonelFromDataReader(SqlDataReader dataReader)
        {
            Personel personel = new(dataReader["Sicil"].ToString()
                , dataReader["AdSoyad"].ToString()
                , dataReader["Tc"].ToString()
                , dataReader["DogumTarihi"].ConDate()
                , dataReader["DogumYeri"].ToString()
                , dataReader["Telefon"].ToString()
                , dataReader["IkametAdresi"].ToString()
                , dataReader["Mezuniyet"].ToString()
                );
            return personel;

        }
    }
}
