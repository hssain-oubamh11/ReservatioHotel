using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SystemGestionReservation.Core.Enums;

namespace SystemGestionReservation.Application.DTOs.Tarif
{
    public class CreateTarifDto
    {
        [Required]
        public TypeChambre TypeChambre { get; set; }

        [Required]
        public Saison Saison { get; set; }

        [Range(1, 100000, ErrorMessage = "Le prix doit être positif.")]
        public decimal PrixParNuit { get; set; }
    }
}
