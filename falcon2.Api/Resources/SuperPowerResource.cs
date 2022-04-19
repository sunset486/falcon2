namespace falcon2.Api.Resources
{
    public class SuperPowerResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SuperHeroResource SuperHero { get; set; }
    }
}
