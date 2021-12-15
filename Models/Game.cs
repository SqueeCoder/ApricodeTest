using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApricodeTest.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GameGenre> Genres { get; set; }
        public string Developer { get; set; }

    }
}
