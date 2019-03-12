using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAppProfilersSample.Shared.Data;

namespace WebAppProfilersSample.Shared.Controllers
{
    [Route("another")]
    public class AnotherController : Controller
    {
        private readonly ProfilingSampleDbContext _db;

        public AnotherController(ProfilingSampleDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _db.Somes.ToListAsync();
            return View(result);
        }
    }
}
