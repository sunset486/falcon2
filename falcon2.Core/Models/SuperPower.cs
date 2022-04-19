using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falcon2.Core.Models
{
    public class SuperPower
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int SuperHeroId { get; set; }
        public SuperHero SuperHero { get; set; }
    }
}
