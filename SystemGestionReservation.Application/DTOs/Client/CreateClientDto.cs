using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SystemGestionReservation.Application.DTOs.Client
{
    public class CreateClientDto
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le numéro d'identité est obligatoire.")]
        public string NumeroIdentite { get; set; }

        public string Adresse { get; set; }

        [Phone(ErrorMessage = "Numéro de téléphone invalide.")]
        public string Telephone { get; set; }

        [EmailAddress(ErrorMessage = "Adresse email invalide.")]
        public string Email { get; set; }
    }
}
