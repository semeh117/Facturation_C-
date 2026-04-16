using FacturationTn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacturationTn.Infrastructure.Persistence.Configurations
{
    public class ProduitConfiguration : IEntityTypeConfiguration<Produit>
    {
        public void Configure(EntityTypeBuilder<Produit> builder)
        {
            builder.Property(p => p.PrixUnitaireHT)
                   .HasPrecision(18, 3);

            builder.Property(p => p.TauxTVA)
                   .HasPrecision(18, 2);
        }
    }
}