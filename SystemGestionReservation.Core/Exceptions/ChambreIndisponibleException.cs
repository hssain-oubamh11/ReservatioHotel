using System;
using System.Collections.Generic;
using System.Text;

namespace SystemGestionReservation.Core.Exceptions
{
    public class ChambreIndisponibleException:Exception
    {
        public ChambreIndisponibleException(string numeroChambre)
       : base($"La chambre '{numeroChambre}' n'est pas disponible pour la période demandée.") { }
    }
}

