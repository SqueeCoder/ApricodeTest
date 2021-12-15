using ApricodeTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApricodeTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        GamesContext db;
        public GamesController(GamesContext context)
        {
            db = context;
            if (!db.Games.Any())
            {
                db.Add(new Game() { Name = "Dota", Genres = new List<GameGenre>() { new GameGenre() { Genre = Genre.RPG }, new GameGenre() { Genre = Genre.MOBA } }, Developer = "IceFrog" });
                db.Add(new Game() { Name = "Age Of Empires", Genres = new List<GameGenre>() { new GameGenre() { Genre = Genre.RTS } }, Developer = "Ensemble Studios" });
                db.Add(new Game() { Name = "Mortal Kombat", Genres = new List<GameGenre>() { new GameGenre() { Genre = Genre.Fighting } }, Developer = "Midway" });
                db.Add(new Game() { Name = "Street Fighter", Genres = new List<GameGenre>() { new GameGenre() { Genre = Genre.Fighting } }, Developer = "Capcom" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> Get()
        {
            return await db.Games.ToListAsync();
        }

        [HttpGet("{genre}")]
        public IActionResult Get(Genre genre)
        {
            List<Game> foundGames = db.Games.Where(g => g.Genres.Any(gg => gg.Genre == genre)).ToList();
            return new ObjectResult(foundGames);
        }

        [HttpPost]
        public async Task<ActionResult<Game>> Post(Game game)
        {
            if (game == null)
            {
                return BadRequest();
            }
            db.Games.Add(game);
            await db.SaveChangesAsync();
            return Ok(game);
        }

        [HttpPut]
        public async Task<ActionResult<Game>> Put(Game game)
        {
            if (game == null)
            {
                return BadRequest();
            }
            if (!db.Games.Any(x => x.Id == game.Id))
            {
                return NotFound();
            }
            db.Update(game);
            await db.SaveChangesAsync();
            return Ok(game);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> Delete(int id)
        {
            Game game = db.Games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            db.Games.Remove(game);
            await db.SaveChangesAsync();
            return Ok(game);
        }
    }
}
