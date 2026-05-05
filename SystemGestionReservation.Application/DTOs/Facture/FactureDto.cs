using System;
using System.Collections.Generic;
using System.Text;

namespace SystemGestionReservation.Application.DTOs.Facture
{
    public class FactureDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public string ClientNomComplet { get; set; }
        public string ChambreNumero { get; set; }
        public DateTime DateArrivee { get; set; }
        public DateTime DateDepart { get; set; }
        public int NombreNuits { get; set; }
        public DateTime DateEmission { get; set; }
        public decimal MontantTotal { get; set; }
        public bool EstPayee { get; set; }
        public List<LigneFactureDto> Lignes { get; set; } = new();
    }
}
