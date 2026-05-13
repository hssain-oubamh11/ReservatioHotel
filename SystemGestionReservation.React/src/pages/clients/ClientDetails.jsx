import { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import { getClientById } from '../../api/clientsApi';
import { getReservationsByClient } from '../../api/reservationsApi';

const ClientDetails = () => {
  const { id } = useParams();
  const [client, setClient] = useState(null);
  const [reservations, setReservations] = useState([]);

  useEffect(() => {
    getClientById(id).then((res) => setClient(res.data));
    getReservationsByClient(id).then((res) => setReservations(res.data));
  }, [id]);

  if (!client) return <div className="text-center mt-5">Chargement...</div>;

  return (
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2> {client.prenom} {client.nom}</h2>
        <div className="d-flex gap-2">
          <Link to={`/clients/${id}/edit`} className="btn btn-warning">Modifier</Link>
          <Link to="/clients" className="btn btn-secondary">Retour</Link>
        </div>
      </div>

      <div className="row">
        <div className="col-md-6">
          <div className="card shadow-sm mb-3">
            <div className="card-header bg-primary text-white">
              Informations personnelles
            </div>
            <div className="card-body">
              <table className="table table-borderless mb-0">
                <tbody>
                  <tr>
                    <td className="fw-bold text-muted" style={{width:'40%'}}>Nom complet</td>
                    <td>{client.prenom} {client.nom}</td>
                  </tr>
                  <tr>
                    <td className="fw-bold text-muted">N° Identité</td>
                    <td><span className="badge bg-secondary">{client.numeroIdentite}</span></td>
                  </tr>
                  <tr>
                    <td className="fw-bold text-muted">Adresse</td>
                    <td>{client.adresse}</td>
                  </tr>
                  <tr>
                    <td className="fw-bold text-muted">Téléphone</td>
                    <td>{client.telephone}</td>
                  </tr>
                  <tr>
                    <td className="fw-bold text-muted">Email</td>
                    <td>{client.email}</td>
                  </tr>
                  <tr>
                    <td className="fw-bold text-muted">Statut</td>
                    <td>
                      <span className={`badge ${client.estActif ? 'bg-success' : 'bg-danger'}`}>
                        {client.estActif ? 'Actif' : 'Inactif'}
                      </span>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>

        <div className="col-md-5 offset-md-1">
          <div className="card shadow-sm">
            <div className="card-header bg-secondary text-white">Actions rapides</div>
            <div className="card-body d-grid gap-2">
              <Link to={`/reservations?clientId=${id}`}
                className="btn btn-outline-primary">Voir les réservations</Link>
              <Link to="/factures"
                className="btn btn-outline-info">Voir les factures</Link>
              <Link to="/reservations/create"
                className="btn btn-outline-success">Nouvelle réservation</Link>
            </div>
          </div>
        </div>
      </div>

      <div className="row mt-3">
        <div className="col-md-12">
          <div className="card shadow-sm">
            <div className="card-header bg-dark text-white">
              Historique des séjours
            </div>
            <div className="card-body p-0">
              {reservations.length === 0 ? (
                <div className="alert alert-info m-3">Aucun séjour enregistré.</div>
              ) : (
                <table className="table table-bordered mb-0">
                  <thead className="table-light">
                    <tr>
                      <th>N° Réservation</th>
                      <th>Chambre</th>
                      <th>Arrivée</th>
                      <th>Départ</th>
                      <th>Montant</th>
                      <th>Statut</th>
                    </tr>
                  </thead>
                  <tbody>
                    {reservations.map((r) => (
                      <tr key={r.id}>
                        <td>#{r.id}</td>
                        <td>{r.chambreNumero}</td>
                        <td>{new Date(r.dateArrivee).toLocaleDateString('fr-FR')}</td>
                        <td>{new Date(r.dateDepart).toLocaleDateString('fr-FR')}</td>
                        <td>{r.montantTotal?.toLocaleString('fr-MA')} MAD</td>
                        <td>
                          <span className={`badge ${
                            r.statut === 'EnCours' ? 'bg-success' :
                            r.statut === 'Terminee' ? 'bg-secondary' :
                            r.statut === 'Annulee' ? 'bg-danger' :
                            'bg-warning text-dark'}`}>
                            {r.statut}
                          </span>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              )}
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default ClientDetails;