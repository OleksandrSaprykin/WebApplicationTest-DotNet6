using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace WebApplicationTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private static List<Test> heroes = new List<Test>
        {
                new Test
                {
                    Id = 1,
                    Name = "Spider Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "New York City"
                },
                new Test
                {
                    Id = 3,
                    Name = "Iron Man",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Place = "Long Island"
                }
        };
        private readonly DataContext _context;

        public TestController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Test>>> Get()
        {
            return Ok(await _context.Tests.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Test>>> Get(int id)
        {
            var hero = await _context.Tests.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(heroes);
        }
        [HttpPost]
        public async Task<ActionResult<List<Test>>> AddHero(Test hero)
        {
            _context.Tests.Add(hero);
            await _context.SaveChangesAsync();  
            return Ok(await _context.Tests.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Test>>> UpdateHero(Test request)
        {
            var dbHero = await _context.Tests.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found.");
            

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName; 
            dbHero.LastName = request.LastName;   
            dbHero.Place = request.Place;
            
            await _context.SaveChangesAsync();

            return Ok(await _context.Tests.ToListAsync());

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Test>>> Delete(int id)
        {
            var hero = heroes.Find(h => h.Id == id);
            if (hero == null)
                return BadRequest("Hero not found.");
            heroes.Remove(hero);
            return Ok(heroes);
        }
    }
}
