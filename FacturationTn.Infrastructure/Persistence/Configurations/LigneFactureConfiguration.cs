using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacturationTn.Domain.Entities;

namespace FacturationTn.Infrastructure.Persistence.Configurations
{
    public class LigneFactureConfiguration : IEntityTypeConfiguration<LigneFacture>
    {
        public void Configure(EntityTypeBuilder<LigneFacture> builder)
        {
            builder.Property(lf => lf.PrixUnitaireHTApplique)
                   .HasPrecision(18, 3);

            builder.Property(lf => lf.TauxTVAApplique)
                   .HasPrecision(18, 2);
        }
    }
}