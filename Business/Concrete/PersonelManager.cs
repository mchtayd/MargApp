using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PersonelManager
    {
        private readonly PersonelDal personelDal;

        public PersonelManager(PersonelDal personelDal)
        {
            this.personelDal = personelDal;
        }

        public async Task<Personel> AddAsync(string sessionSicilNo, Personel entity) => await personelDal.AddAsync(sessionSicilNo, entity);


        public async Task<bool> DeleteAsync(string sessionSicilNo, string sicil) => await personelDal.DeleteAsync(sessionSicilNo, sicil);

        public async Task<Personel> GetAsync(string sessionSicilNo, string sicil) => await personelDal.GetAsync(sessionSicilNo, sicil);


        public async Task<List<Personel>> GetListAsync() => await personelDal.GetListAsync();


        public async Task<Personel> UpdateAsync(string sessionSicilNo, Personel entity, string oldName) => await personelDal.UpdateAsync(sessionSicilNo, entity, oldName);

    }
}
