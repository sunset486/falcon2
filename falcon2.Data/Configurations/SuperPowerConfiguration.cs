using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using falcon2.Core.Models;

namespace falcon2.Data.Configurations
{
    public class SuperPowerConfiguration : IEntityTypeConfiguration<SuperPower>
    {
        public void Configure(EntityTypeBuilder<SuperPower> builder)
        {
            builder.HasKey(sp => sp.Id);
            builder.Property(sp => sp.Id)
                .UseIdentityColumn();
            builder.Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasOne(sp => sp.SuperHero)
                .WithMany(sh => sh.SuperPowers)
                .HasForeignKey(sp => sp.SuperHeroId);
            builder.ToTable("SuperPowers");
        }
    }
}
