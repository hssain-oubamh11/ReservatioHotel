using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SystemGestionReservation.Core.Enums;
namespace SystemGestionReservation.Application.DTOs.Chambre
{
    public class CreateChambreDto
    {
        [Required(ErrorMessage = "Le numéro de chambre est obligatoire.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Le type de chambre est obligatoire.")]
        public TypeChambre Type { get; set; }

        [Range(0, 50, ErrorMessage = "L'étage doit être entre 0 et 50.")]
        public int Etage { get; set; }

        [Range(1, 20, ErrorMessage = "La capacité doit être entre 1 et 20.")]
        public int CapaciteAccueil { get; set; }

        public string Description { get; set; }
    }
}
