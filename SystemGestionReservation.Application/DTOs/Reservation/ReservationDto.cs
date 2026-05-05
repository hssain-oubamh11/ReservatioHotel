using System;
using System.Collections.Generic;
using System.Text;
using SystemGestionReservation.Core.Enums;

namespace SystemGestionReservation.Application.DTOs.Reservation
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientNomComplet { get; set; }
        public int ChambreId { get; set; }
        public string ChambreNumero { get; set; }
        public DateTime DateArrivee { get; set; }
        public DateTime DateDepart { get; set; }
        public int NombrePersonnes { get; set; }
        public int NombreNuits { get; set; }
        public StatutReservation Statut { get; set; }
        public DateTime DateCreation { get; set; }
        public decimal? RemiseAppliquee { get; set; }
    }
}
