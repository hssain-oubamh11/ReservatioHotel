import { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import { getFactureById } from '../../api/facturesApi';

const FactureDetails = () => {
  const { id } = useParams();
  const [facture, setFacture] = useState(null);

  useEffect(() => {
    getFactureById(id).then((r) => setFacture(r.data));
  }, [id]);

  if (!facture) return <div className="text-center mt-5">Chargement...</div>;

  return (
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>🧾 Facture #{facture.id}</h2>
        <div className="d-flex gap-2">
          <button
            className="btn btn-outline-secondary"
            onClick={() => window.print()}>
            🖨️ Imprimer
          </button>
          <Link
            to={`/reservations?clientId=${facture.reservationId}`}
            className="btn btn-secondary">
            ← Retour
          </Link>
        </div>
      </div>

      <div className="card shadow-sm mb-3">
        <div className="card-body">
          <div className="row">
            <div className="col-md-6">
              <p><strong>Client :</strong> {facture.clientNomComplet}</p>
              <p><strong>Chambre :</strong> {facture.chambreNumero}</p>
            </div>
            <div className="col-md-6">
              <p>
                <strong>Arrivée :</strong>
                {new Date(facture.dateArrivee).toLocaleDateString('fr-FR')}
              </p>
              <p>
                <strong>Départ :</strong>
                {new Date(facture.dateDepart).toLocaleDateString('fr-FR')}
              </p>
              <p><strong>Nuits :</strong> {facture.nombreNuits}</p>
              <p>
                <strong>Émise le :</strong>
                {new Date(facture.dateEmission).toLocaleString('fr-FR')}
              </p>
            </div>
          </div>
        </div>
      </div>

      <table className="table table-bordered shadow-sm">
        <thead className="table-light">
          <tr>
            <th>Description</th>
            <th className="text-center">Qté</th>
            <th className="text-end">Prix unitaire</th>
            <th className="text-end">Sous-total</th>
          </tr>
        </thead>
        <tbody>
          {facture.lignes?.map((ligne, i) => (
            <tr key={i}>
              <td>{ligne.description}</td>
              <td className="text-center">{ligne.quantite}</td>
              <td className="text-end">
                {ligne.prixUnitaire.toLocaleString('fr-MA')} MAD
              </td>
              <td className="text-end">
                {ligne.sousTotal.toLocaleString('fr-MA')} MAD
              </td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr className="table-primary fw-bold">
            <td colSpan="3" className="text-end fs-5">TOTAL</td>
            <td className="text-end fs-5">
              {facture.montantTotal.toLocaleString('fr-MA')} MAD
            </td>
          </tr>
        </tfoot>
      </table>

      <div className="mt-3">
        {facture.estPayee
          ? <span className="badge bg-success fs-6">✅ Payée</span>
          : <span className="badge bg-warning text-dark fs-6">
              ⏳ En attente de paiement
            </span>
        }
      </div>
    </>
  );
};

export default FactureDetails;