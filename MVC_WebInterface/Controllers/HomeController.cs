using Microsoft.AspNetCore.Mvc;
using MVC_WebInterface.Models;
using System.Diagnostics;

namespace MVC_WebInterface.Controllers
{
    public class HomeController : BaseController
    {
        private readonly PersonelManager personelManager;
        private readonly IMapper mapper;

        public HomeController(PersonelManager personelManager, IMapper mapper)
        {
            this.personelManager = personelManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var list = await personelManager.GetListAsync();
            var dtoList = mapper.Map<List<PersonelDTO>>(list);

            IndexModel model = new() { Personels = dtoList };
            return View(model);
        }



    }
}