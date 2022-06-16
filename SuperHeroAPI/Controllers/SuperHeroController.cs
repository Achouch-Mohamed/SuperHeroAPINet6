using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>(){
                new SuperHero
                {
                    ID = 1,
                    Name = "Mohamed Achouch",
                    FirstName= "Mohamed",
                    LastName="Achouch",
                    Place = "Rabat"
                },
                new SuperHero
                {
                    ID=2,
                    Name = "Ahmad Mousa",
                    FirstName= "Ahmad",
                    LastName="Mousa",
                    Place = "Merakch"
               }           
            
            };
        //Get Connction 
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context; 
        }





        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {     
             
            return Ok(await _context.superHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero superHero)
        {
           _context.superHeroes.Add(superHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.superHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.superHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("hero not fond !");
            return Ok(hero);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>>UpdateHero(SuperHero request)
        {
            var dbhero = await _context.superHeroes.FindAsync(request.ID);
            if (dbhero == null)
                return BadRequest("Hero Not Fond !");
            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;
            await _context.SaveChangesAsync();
            return Ok(dbhero);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var hero = await _context.superHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("hero not fond !");
            _context.superHeroes.Remove(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.superHeroes.ToListAsync());
        }
    }
}
