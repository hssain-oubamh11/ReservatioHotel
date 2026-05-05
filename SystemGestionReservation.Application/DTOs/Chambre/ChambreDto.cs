using System;
using System.Collections.Generic;
using System.Text;
using SystemGestionReservation.Core.Enums;

namespace SystemGestionReservation.Application.DTOs.Chambre
{
    public class ChambreDto
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public TypeChambre Type { get; set; }
        public int Etage { get; set; }
        public int CapaciteAccueil { get; set; }
        public string Description { get; set; }
        public bool EstActive { get; set; }
        public List<string> Equipements { get; set; } = new();
    }
}
