import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getAllClients, desactiverClient } from '../../api/clientsApi';
import { useAuth } from '../../context/AuthContext';
import AlertMessage from '../../components/AlertMessage';

const ClientsList = () => {
  const [clients, setClients] = useState([]);
  const [terme, setTerme] = useState('');
  const [alert, setAlert] = useState({ type: '', message: '' });
  const { isAdmin } = useAuth();

  const fetchClients = async (t = '') => {
    const res = await getAllClients(t);
    setClients(res.data);
  };

  useEffect(() => { fetchClients(); }, []);

  const handleSearch = (e) => {
    e.preventDefault();
    fetchClients(terme);
  };

  const handleDesactiver = async (id) => {
    if (!window.confirm('Désactiver ce client ?')) return;
    await desactiverClient(id);
    setAlert({ type: 'success', message: 'Client désactivé.' });
    fetchClients(terme);
  };

  return (
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2> Clients</h2>
        <Link to="/clients/create" className="btn btn-primary">Nouveau client</Link>
      </div>

      <AlertMessage type={alert.type} message={alert.message}
        onClose={() => setAlert({ type: '', message: '' })} />

      <form onSubmit={handleSearch} className="mb-3">
        <div className="input-group">
          <input type="text" className="form-control"
            placeholder="Rechercher par nom, identité, téléphone..."
            value={terme} onChange={(e) => setTerme(e.target.value)} />
          <button className="btn btn-outline-secondary" type="submit">Rechercher</button>
        </div>
      </form>

      <div className="card shadow-sm">
        <div className="card-body p-0">
          <table className="table table-striped table-hover mb-0">
            <thead className="table-primary">
              <tr>
                <th>Nom complet</th>
                <th>N° Identité</th>
                <th>Téléphone</th>
                <th>Email</th>
                <th>Statut</th>
                <th className="text-center">Actions</th>
              </tr>
            </thead>
            <tbody>
              {clients.map((client) => (
                <tr key={client.id}>
                  <td className="fw-bold">{client.prenom} {client.nom}</td>
                  <td><span className="badge bg-secondary">{client.numeroIdentite}</span></td>
                  <td>{client.telephone}</td>
                  <td>{client.email}</td>
                  <td>
                    <span className={`badge ${client.estActif ? 'bg-success' : 'bg-danger'}`}>
                      {client.estActif ? 'Actif' : 'Inactif'}
                    </span>
                  </td>
                  <td className="text-center">
                    <Link to={`/clients/${client.id}`}
                      className="btn btn-sm btn-info text-white me-1">Voir</Link>
                    <Link to={`/clients/${client.id}/edit`}
                      className="btn btn-sm btn-warning me-1">Modifier</Link>
                    {isAdmin() && (
                      <button className="btn btn-sm btn-danger"
                        onClick={() => handleDesactiver(client.id)}>Supprimer</button>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          {clients.length === 0 && (
            <div className="alert alert-info m-3">Aucun client trouvé.</div>
          )}
        </div>
      </div>
    </>
  );
};

export default ClientsList;