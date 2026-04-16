using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FacturationTn.Domain.Common;
using FacturationTn.Domain.Enums;

namespace FacturationTn.Domain.Entities
{
    public class Facture : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string NumeroFacture { get; set; } = string.Empty; // Fixed warning

        [Required]
        public DateTime DateFacture { get; set; } = DateTime.UtcNow; // Fixed consistency

        public StatutFacture Statut { get; set; } = StatutFacture.Brouillon;

        public decimal TotalHT { get; set; }
        public decimal TotalTVA { get; set; }
        public decimal TimbreFiscal { get; set; } = 1.000m; 
        public decimal TotalTTC { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; } // Optional: made nullable to avoid warnings before loading

        public ICollection<LigneFacture> LignesFacture { get; set; } = new List<LigneFacture>();
    }
}