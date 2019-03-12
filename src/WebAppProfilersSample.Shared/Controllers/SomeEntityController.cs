using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppProfilersSample.Shared.Data;
using WebAppProfilersSample.Shared.Data.Entities;

namespace WebAppProfilersSample.Shared.Controllers
{
    [Route("some")]
    public class SomeEntityController : ControllerBase
    {
        private readonly ProfilingSampleDbContext _db;

        public SomeEntityController(ProfilingSampleDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<SomeEntity>>> GetAllAsync()
        {
            var result = await _db.Somes.ToListAsync();
            return result;
        }

        [HttpGet]
        [Route("{someId}/others")]
        public async Task<ActionResult<IReadOnlyCollection<SomeOtherEntity>>> GetAllOthersAsync(long someId)
        {
            var result = await _db.Others.Where(o => o.Some.Id == someId).ToListAsync();
            return result;
        }
    }
}
