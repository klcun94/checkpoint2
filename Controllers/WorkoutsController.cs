using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using capstone.Data;
using capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace capstone.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private ApplicationDbContext _context;
        public WorkoutsController(ApplicationDbContext context) {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Workouts> Get()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Workouts[] workouts = null;
            _context.Workouts.Where(w => w.User.Id == userId).ToArray();
            return workouts;
        }
        [HttpPost]
        public Workouts Post([FromBody]Workouts workouts)
        {
            _context.Workouts.Add(workouts);
            _context.SaveChanges();
            return workouts;
        }
    }
}
