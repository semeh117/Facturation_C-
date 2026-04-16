using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacturationTn.Domain.Entities;

namespace FacturationTn.Infrastructure.Persistence.Configurations
{
    public class FactureConfiguration : IEntityTypeConfiguration<Facture>
    {
        public void Configure(EntityTypeBuilder<Facture> builder)
        {
            builder.Property(f => f.TotalHT)
                   .HasPrecision(18, 3);

            builder.Property(f => f.TotalTVA)
                   .HasPrecision(18, 3);

            builder.Property(f => f.TimbreFiscal)
                   .HasPrecision(18, 3);

            builder.Property(f => f.TotalTTC)
                   .HasPrecision(18, 3);
        }
    }
}
