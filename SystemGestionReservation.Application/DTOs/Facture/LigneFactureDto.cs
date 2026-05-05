using System;
using System.Collections.Generic;
using System.Text;

namespace SystemGestionReservation.Application.DTOs.Facture
{
    public class LigneFactureDto
    {
        public string Description { get; set; }
        public int Quantite { get; set; }
        public decimal PrixUnitaire { get; set; }
        public decimal SousTotal { get; set; }
    }
}
