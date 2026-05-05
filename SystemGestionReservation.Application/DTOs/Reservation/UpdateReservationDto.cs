using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SystemGestionReservation.Application.DTOs.Reservation
{
    public class UpdateReservationDto
    {
        [Required]
        public DateTime DateArrivee { get; set; }

        [Required]
        public DateTime DateDepart { get; set; }

        [Required]
        public int ChambreId { get; set; }

        [Range(1, 20)]
        public int NombrePersonnes { get; set; }
    }
}
