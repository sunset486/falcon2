using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using falcon2.Core.Models.FileUploader;

namespace falcon2.Data.Configurations
{
    public class UploadedFileConfiguration : IEntityTypeConfiguration<UploadedFile>
    {
        public void Configure(EntityTypeBuilder<UploadedFile> builder)
        {
            builder.HasKey(uf => uf.Id);
            builder.Property(uf => uf.Id)
                .UseIdentityColumn();
            builder.Property(uf => uf.FileName)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(uf => uf.FileSize)
                .IsRequired();
            builder.Property(uf => uf.FileContent)
                .IsRequired();
            builder.Property(uf => uf.UploadDate)
                .IsRequired();
            builder.Property(uf => uf.UploadedBy)
                .IsRequired()
                .HasMaxLength(50);
            builder.ToTable("UploadedFiles");
        }
    }
}
