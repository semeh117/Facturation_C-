using FacturationTn.Domain.Entities;
using System.Linq;

namespace FacturationTn.Application.Services
{
    public class FactureService
    {
        // The Tunisian Tax Stamp is fixed at 1.000 TND
        private const decimal TimbreFiscal = 1.000m; 

        public void CalculerTotaux(Facture facture)
        {
            if (facture.LignesFacture == null || !facture.LignesFacture.Any())
            {
                facture.TotalHT = 0;
                facture.TotalTVA = 0;
                facture.TotalTTC = 0;
                return;
            }

            decimal totalHT = 0;
            decimal totalTVA = 0;

            foreach (var ligne in facture.LignesFacture)
            {
                // HT for this line = Price * Quantity
                decimal ligneHT = ligne.PrixUnitaireHTApplique * ligne.Quantite;
                totalHT += ligneHT;

                // TVA for this line = Line HT * (VAT Rate / 100)
                decimal ligneTVA = ligneHT * (ligne.TauxTVAApplique / 100);
                totalTVA += ligneTVA;
            }

            facture.TotalHT = totalHT;
            facture.TotalTVA = totalTVA;
            facture.TimbreFiscal = TimbreFiscal;

            // TTC = HT + TVA + Timbre Fiscal
            facture.TotalTTC = facture.TotalHT + facture.TotalTVA + facture.TimbreFiscal;
        }
    }
}