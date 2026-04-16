using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FacturationTn.Domain.Common;

namespace FacturationTn.Domain.Entities
{
    public class Client : BaseEntity
    {
        [Required(ErrorMessage = "Le nom ou la raison sociale est obligatoire.")]
        [MaxLength(100)]
        public string Nom { get; set; } = string.Empty; // Can be a person's name OR company name

        [MaxLength(100)]
        public string RaisonSociale { get; set; } = string.Empty; 

        [MaxLength(50)]
        public string MatriculeFiscal { get; set; } = string.Empty; // Tax ID / CIN

        [MaxLength(200)]
        public string Adresse { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Telephone { get; set; } = string.Empty;

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public ICollection<Facture> Factures { get; set; } = new List<Facture>();
    }
}