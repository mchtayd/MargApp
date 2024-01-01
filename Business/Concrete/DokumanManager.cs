using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DokumanManager
    {
        private readonly DokumanDal dokumanDal;

        public DokumanManager(DokumanDal dokumanDal)
        {
            this.dokumanDal = dokumanDal;
        }

        public async Task<Dokuman> AddAsync(string sessionSicilNo, Dokuman entity) => await dokumanDal.AddAsync(sessionSicilNo, entity);

        public async Task<bool> DeleteAsync(string sessionSicilNo, string id) => await dokumanDal.DeleteAsync(sessionSicilNo, id);

        public async Task<Dokuman> GetAsync(string sessionSicilNo, string id) => await dokumanDal.GetAsync(sessionSicilNo, id);
        public async Task<List<Dokuman>> GetListAsync() => await dokumanDal.GetListAsync();

        public async Task<Dokuman> UpdateAsync(string sessionSicilNo, Dokuman entity, string oldName) => await dokumanDal.UpdateAsync(sessionSicilNo, entity, oldName);
    }
}
