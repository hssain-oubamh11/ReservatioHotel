import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getAllFactures } from '../../api/facturesApi';

const FacturesList = () => {
  const [factures, setFactures] = useState([]);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');

  useEffect(() => {
    getAllFactures()
      .then((r) => setFactures(r.data))
      .finally(() => setLoading(false));
  }, []);

  const filtered = factures.filter((f) =>
    f.clientNomComplet?.toLowerCase().includes(search.toLowerCase()) ||
    f.chambreNumero?.toString().includes(search) ||
    f.id?.toString().includes(search)
  );

  if (loading) return <div className="text-center mt-5">Chargement...</div>;

  return (
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2> Factures</h2>
      </div>

      <input
        type="text"
        className="form-control mb-3"
        placeholder="Rechercher par client, chambre, n° facture..."
        value={search}
        onChange={(e) => setSearch(e.target.value)}
      />

      {filtered.length === 0 ? (
        <div className="alert alert-info">Aucune facture trouvée.</div>
      ) : (
        <table className="table table-bordered shadow-sm">
          <thead className="table-light">
            <tr>
              <th>N° Facture</th>
              <th>Client</th>
              <th>Chambre</th>
              <th>Arrivée</th>
              <th>Départ</th>
              <th>Nuits</th>
              <th className="text-end">Montant</th>
              <th className="text-center">Statut</th>
              <th className="text-center">Action</th>
            </tr>
          </thead>
          <tbody>
            {filtered.map((f) => (
              <tr key={f.id}>
                <td>#{f.id}</td>
                <td>{f.clientNomComplet}</td>
                <td>{f.chambreNumero}</td>
                <td>{new Date(f.dateArrivee).toLocaleDateString('fr-FR')}</td>
                <td>{new Date(f.dateDepart).toLocaleDateString('fr-FR')}</td>
                <td className="text-center">{f.nombreNuits}</td>
                <td className="text-end">
                  {f.montantTotal.toLocaleString('fr-MA')} MAD
                </td>
                <td className="text-center">
                  {f.estPayee
                    ? <span className="badge bg-success">✅ Payée</span>
                    : <span className="badge bg-warning text-dark">⏳ En attente</span>
                  }
                </td>
                <td className="text-center">
                  <Link
                    to={`/factures/${f.id}`}
                    className="btn btn-sm btn-outline-primary">
                    Voir
                  </Link>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </>
  );
};

export default FacturesList;