using Microsoft.AspNetCore.Mvc;
using static MVC_WebInterface.Models.Shared.MvcInfos;

namespace MVC_WebInterface.Controllers
{
    public class PersonelController : BaseController
    {
        private readonly IMapper mapper;
        private readonly PersonelManager personelManager;

        public PersonelController(IMapper mapper, PersonelManager personelManager)
        {
            this.mapper = mapper;
            this.personelManager = personelManager;
        }

        public async Task<IActionResult> Index(string message)
        {
            var list = await personelManager.GetListAsync();
            var dtoList = mapper.Map<List<PersonelDTO>>(list);

            IndexModel model = new() { Personels = dtoList };
            ViewBag.Message = message;
            return View(model);
        }

        // GET: PersonelController/Details/5
        public async Task<IActionResult> Details(string sicilNo)
        {
            var personel = await personelManager.GetAsync(GetSessionUser().Name, sicilNo);
            var personelDto = mapper.Map<PersonelDTO>(personel);
            return View(personelDto);
        }

        // GET: PersonelController/Create
        public async Task<IActionResult> Create(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        // POST: PersonelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonelDTO personelDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (personelDTO.MezuniyetBilgisi.Equals(Mezuniyet.All))
                    {
                        ModelState.AddModelError("Validation", "Lütfen Geçerli Bir Mezuniyet Seçimi Yapınız");
                        return View(personelDTO);
                    }

                    var personel = mapper.Map<Personel>(personelDTO);
                    var result = await personelManager.AddAsync(GetSessionUser().SicilNo, personel);
                    if (result == null)
                    {
                        return View(nameof(Create), personelDTO);
                    }

                    return RedirectToAction(nameof(Index), new { message = personelDTO.PersonelAdSoyad + " Kaydı Başarıyla Gerçekleşti" });
                }
                return View(personelDTO);
            }
            catch
            {
                return View(personelDTO);
            }
        }

        // GET: PersonelController/Edit/5
        public async Task<IActionResult> Edit(string message, string sicilNo)
        {
            var personel = await personelManager.GetAsync(GetSessionUser().SicilNo, sicilNo);
            var personelDto = mapper.Map<PersonelDTO>(personel);
            ViewBag.Message = message;
            return View(personelDto);
        }

        // POST: PersonelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonelDTO personelDTO)
        {
            try
            {
                var personel = mapper.Map<Personel>(personelDTO);
                var result = await personelManager.UpdateAsync(GetSessionUser().SicilNo, personel, "");
                if (result == null)
                {
                    return PartialView(_Warning, "Güncelleme Sırasında Bir Hata Oluştu");
                }

                return RedirectToAction(nameof(Index), new { message = personelDTO.PersonelAdSoyad + " Güncellemesi Başarıyla Gerçekleşti" });
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { message = personelDTO.PersonelAdSoyad + " Güncelleme İşlemi Sırasında Bir Hata Oluştu" });
            }
        }

        // GET: PersonelController/Delete/5
        public async Task<IActionResult> Delete(string sicilNo)
        {
            var personel = await personelManager.GetAsync(GetSessionUser().SicilNo, sicilNo);
            var personelDto = mapper.Map<PersonelDTO>(personel);
            return View(personelDto);
        }

        // POST: PersonelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string sicilNo, string adSoyad)
        {
            try
            {
                var result = await personelManager.DeleteAsync(GetSessionUser().SicilNo, sicilNo);
                if (!result)
                    return RedirectToAction(nameof(Index), new { message = adSoyad + " Silme İşlemi Sırasında Bir Hata Oluştu" });

                return RedirectToAction(nameof(Index), new { message = adSoyad + " Silme İşlemi Başarıyla Gerçekleşti" });
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { message = adSoyad + " Silme İşlemi Sırasında Bir Hata Oluştu" });
            }
        }
    }
}
