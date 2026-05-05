using System;
using System.Collections.Generic;
using System.Text;

namespace SystemGestionReservation.Application.DTOs.Client
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NumeroIdentite { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public bool EstActif { get; set; }
    }
}
