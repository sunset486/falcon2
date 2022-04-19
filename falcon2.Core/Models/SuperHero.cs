namespace falcon2.Core.Models
{
    public class SuperHero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SuperPower> SuperPowers { get; set; }
    }
}
