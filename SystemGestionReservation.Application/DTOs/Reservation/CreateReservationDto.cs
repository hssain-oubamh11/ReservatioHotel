using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SystemGestionReservation.Application.DTOs.Reservation
{
    public class CreateReservationDto
    {
        [Required(ErrorMessage = "Le client est obligatoire.")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "La chambre est obligatoire.")]
        public int ChambreId { get; set; }

        [Required(ErrorMessage = "La date d'arrivée est obligatoire.")]
        public DateTime DateArrivee { get; set; }

        [Required(ErrorMessage = "La date de départ est obligatoire.")]
        public DateTime DateDepart { get; set; }

        [Range(1, 20, ErrorMessage = "Le nombre de personnes doit être entre 1 et 20.")]
        public int NombrePersonnes { get; set; }
    }
}
