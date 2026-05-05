using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemGestionReservation.Core.Enums;
using System.Text;

namespace SystemGestionReservation.Application.DTOs.Chambre
{
    public class UpdateChambreDto
    {
        [Required]
        public TypeChambre Type { get; set; }

        [Range(0, 50)]
        public int Etage { get; set; }

        [Range(1, 20)]
        public int CapaciteAccueil { get; set; }

        public string Description { get; set; }
    }
}
