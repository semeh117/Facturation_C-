using System.ComponentModel.DataAnnotations;
using FacturationTn.Domain.Common;

namespace FacturationTn.Domain.Entities
{
    public class LigneFacture : BaseEntity
    {
        [Required]
        public int FactureId { get; set; }
        public Facture Facture { get; set; } = null!;

        [Required]
        public int ProduitId { get; set; }
        public required Produit Produit { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être d'au moins 1.")]
        public int Quantite { get; set; }

        // Snapshot data at the moment of billing
        [Required]
        public decimal PrixUnitaireHTApplique { get; set; }
        
        [Required]
        public decimal TauxTVAApplique { get; set; }
    }
}