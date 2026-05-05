using System;
using System.Collections.Generic;
using System.Text;

namespace SystemGestionReservation.Core.Exceptions
{
    public class ReservationIntrouvableException:Exception
    {
        public ReservationIntrouvableException(int id)
        : base($"Aucune réservation trouvée avec l'identifiant {id}.") { }
    }
}
