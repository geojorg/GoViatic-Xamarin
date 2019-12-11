using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GoViatic.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViaticsController : ControllerBase
    {
        private readonly DataContext _context;

        public ViaticsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Viatic> GetViatics()
        {
            return _context.Viatics;
        }
    }
}