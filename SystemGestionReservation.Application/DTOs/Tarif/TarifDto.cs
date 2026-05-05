using System;
using System.Collections.Generic;
using System.Text;
using SystemGestionReservation.Core.Enums;

namespace SystemGestionReservation.Application.DTOs.Tarif
{
    public class TarifDto
    {
        public int Id { get; set; }
        public TypeChambre TypeChambre { get; set; }
        public Saison Saison { get; set; }
        public decimal PrixParNuit { get; set; }
    }
}
