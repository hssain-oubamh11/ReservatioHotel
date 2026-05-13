import { useState, useEffect } from 'react';
import { useSearchParams, Link } from 'react-router-dom';
import {
  getReservationsByClient, checkIn,
  checkOut, annulerReservation
} from '../../api/reservationsApi';
import { getClientById } from '../../api/clientsApi';
import AlertMessage from '../../components/AlertMessage';
import { useNavigate } from 'react-router-dom';

const statutBadge = {
  EnAttente:        'bg-warning text-dark',
  Confirmee: 'bg-success',
  CheckInEffectue: 'bg-primary',
  CheckOutEffectue: 'bg-secondary',
  Annulee: 'bg-danger'
};

const ReservationsList = () => {
    const [searchParams]                    = useSearchParams();
    const clientId                          = searchParams.get('clientId');
    const [reservations, setReservations]   = useState([]);
    const [clientNom, setClientNom]         = useState('');
    const [alert, setAlert]                 = useState({ type: '', message: '' });
    const navigate                          = useNavigate();

    useEffect(() => {
    if (!clientId) return;
    getReservationsByClient(clientId).then((r) =>
      setReservations(r.data));
    getClientById(clientId).then((r) =>
      setClientNom(`${ r.data.prenom} ${ r.data.nom}`));
}, [clientId]);

const handleCheckIn = async (id) => {
    await checkIn(id);
    setAlert({ type: 'success', message: 'Check-in effectué.' });
    getReservationsByClient(clientId).then((r) => setReservations(r.data));
};

const handleCheckOut = async (id) => {
    const res = await checkOut(id);
    const factureId = res.data.factureId;
    setAlert({
    type: 'success',
      message: `Check -out effectué.Facture #${factureId} générée.`
    });
    navigate(`/ factures /${ factureId}`);
};

const handleAnnuler = async (id) => {
    if (!window.confirm('Annuler cette réservation ?')) return;
    const res = await annulerReservation(id);
    setAlert({
    type: 'warning',
      message: `${ res.data.message} — Pénalité: ${ res.data.penaliteAppliquee}`
    });
    getReservationsByClient(clientId).then((r) => setReservations(r.data));
};

return (

  <>

    < div className = "d-flex justify-content-between align-items-center mb-3" >

      < h2 >
          📅 Réservations
          {clientNom && (
            <small className="text-muted fs-6 ms-2">— {clientNom}</ small >
          )}
        </ h2 >
        < Link to = "/reservations/create" className = "btn btn-primary" >
          ➕ Nouvelle réservation
        </Link>
      </div>

      <AlertMessage type ={alert.type} message ={ alert.message}
onClose ={ () => setAlert({ type: '', message: '' })} />


< div className = "card shadow-sm" >

< div className = "card-body p-0" >

< table className = "table table-striped table-hover mb-0" >

< thead className = "table-primary" >

< tr >

< th > Chambre </ th >

< th > Arrivée </ th >

< th > Départ </ th >

< th > Nuits </ th >

< th > Remise </ th >

< th > Statut </ th >

< th className = "text-center" > Actions </ th >

</ tr >

</ thead >

< tbody >
              {
    reservations.map((r) => (
                < tr key ={ r.id}>
                  < td >{ r.chambreNumero}</ td >
                  < td >{ new Date(r.dateArrivee).toLocaleDateString('fr-FR')}</ td >
                  < td >{ new Date(r.dateDepart).toLocaleDateString('fr-FR')}</ td >
                  < td >{ r.nombreNuits}</ td >
                  < td >{ r.remiseAppliquee ? `${ r.remiseAppliquee}%` : '—'}</ td >
                  < td >
                    < span className ={`badge ${ statutBadge[r.statut] || 'bg-secondary'}`}>
                      { r.statut}
                    </ span >
                  </ td >
                  < td className = "text-center" >
                    {
        (r.statut === 'Confirmee' ||
                      r.statut === 'EnAttente') && (
                      <>
                        < button
                          className = "btn btn-sm btn-primary me-1"
                          onClick ={ () => handleCheckIn(r.id)}
        title = "Check-in" >
                          ✅
                        </ button >
                        < button
                          className = "btn btn-sm btn-danger"
                          onClick ={ () => handleAnnuler(r.id)}
        title = "Annuler" >
                          ❌
                        </ button >
                      </>
                    )}
    {
        r.statut === 'CheckInEffectue' && (

      < button
                        className = "btn btn-sm btn-success"
                        onClick ={ () => handleCheckOut(r.id)}
        title = "Check-out" >
                        🏁 Check -out
                      </ button >
                    )}
                  </ td >
                </ tr >
              ))}
            </ tbody >
          </ table >
          {
    reservations.length === 0 && (
            < div className = "alert alert-info m-3" >
              Aucune réservation trouvée.
            </ div >
          )}
        </ div >
      </ div >
    </>
  );
};

export default ReservationsList;