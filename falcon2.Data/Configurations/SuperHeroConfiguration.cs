using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using falcon2.Core.Models;

namespace falcon2.Data.Configurations
{
    public class SuperHeroConfiguration : IEntityTypeConfiguration<SuperHero>
    {
        public void Configure(EntityTypeBuilder<SuperHero> builder)
        {
            builder.HasKey(sh => sh.Id);
            builder.Property(sh => sh.Id)
                .UseIdentityColumn();
            builder.Property(sh => sh.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.ToTable("SuperHeroes");
        }
    }
}
