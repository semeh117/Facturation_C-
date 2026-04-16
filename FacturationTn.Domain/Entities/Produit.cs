using System.ComponentModel.DataAnnotations;
using FacturationTn.Domain.Common;

namespace FacturationTn.Domain.Entities
{
    public class Produit : BaseEntity
    {
        
        [Required(ErrorMessage = "La désignation est obligatoire.")]
        [MaxLength(150)]
        public string Designation { get; set; } = string.Empty; // Fixed warning 

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix HT doit être supérieur à 0.")]
        public decimal PrixUnitaireHT { get; set; }

        [Required]
        public decimal TauxTVA { get; set; } // e.g., 7.00, 13.00, 19.00
    }
}