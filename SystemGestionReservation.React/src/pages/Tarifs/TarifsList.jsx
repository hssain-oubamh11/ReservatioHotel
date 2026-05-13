import { useState, useEffect } from 'react';
import { getAllTarifs, updateTarif } from '../../api/tarifsApi';

const saisonLabel = {
  BasseSaison: { label: 'Basse saison', badge: 'bg-info text-dark' },
  HauteSaison: { label: 'Haute saison', badge: 'bg-danger' },
  Speciale:    { label: 'Période spéciale', badge: 'bg-warning text-dark' },
};

const TarifsList = () => {
  const [tarifs, setTarifs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [editId, setEditId] = useState(null);
  const [editValue, setEditValue] = useState('');
  const [message, setMessage] = useState(null);

  useEffect(() => {
    getAllTarifs()
      .then((r) => setTarifs(r.data))
      .finally(() => setLoading(false));
  }, []);

  const handleEdit = (tarif) => {
    setEditId(tarif.id);
    setEditValue(tarif.prixParNuit);
    setMessage(null);
  };

  const handleSave = async (id) => {
    try {
      await updateTarif(id, { prixParNuit: parseFloat(editValue) });
      setTarifs(tarifs.map((t) =>
        t.id === id ? { ...t, prixParNuit: parseFloat(editValue) } : t
      ));
      setEditId(null);
      setMessage({ type: 'success', text: 'Tarif mis à jour avec succès.' });
    } catch (err) {
      setMessage({ type: 'danger', text: err.response?.data?.message || 'Erreur.' });
    }
  };

  if (loading) return <div className="text-center mt-5">Chargement...</div>;

  // Grouper par type de chambre
  const grouped = tarifs.reduce((acc, t) => {
    if (!acc[t.typeChambre]) acc[t.typeChambre] = [];
    acc[t.typeChambre].push(t);
    return acc;
  }, {});

  return (
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2> Gestion des tarifs</h2>
      </div>

      {message && (
        <div className={`alert alert-${message.type} alert-dismissible`}>
          {message.text}
          <button className="btn-close" onClick={() => setMessage(null)} />
        </div>
      )}

      {Object.entries(grouped).map(([type, lignes]) => (
        <div key={type} className="card shadow-sm mb-4">
          <div className="card-header fw-bold">
             Chambre {type}
          </div>
          <table className="table table-bordered mb-0">
            <thead className="table-light">
              <tr>
                <th>Saison</th>
                <th className="text-end">Prix par nuit (MAD)</th>
                <th className="text-center">Action</th>
              </tr>
            </thead>
            <tbody>
              {lignes.map((t) => {
                const s = saisonLabel[t.saison] || { label: t.saison, badge: 'bg-secondary' };
                return (
                  <tr key={t.id}>
                    <td>
                      <span className={`badge ${s.badge}`}>{s.label}</span>
                    </td>
                    <td className="text-end">
                      {editId === t.id ? (
                        <input
                          type="number"
                          className="form-control form-control-sm d-inline w-auto text-end"
                          value={editValue}
                          min={0}
                          onChange={(e) => setEditValue(e.target.value)}
                        />
                      ) : (
                        <strong>{t.prixParNuit.toLocaleString('fr-MA')} MAD</strong>
                      )}
                    </td>
                    <td className="text-center">
                      {editId === t.id ? (
                        <div className="d-flex gap-2 justify-content-center">
                          <button
                            className="btn btn-sm btn-success"
                            onClick={() => handleSave(t.id)}>
                             Enregistrer
                          </button>
                          <button
                            className="btn btn-sm btn-outline-secondary"
                            onClick={() => setEditId(null)}>
                            Annuler
                          </button>
                        </div>
                      ) : (
                        <button
                          className="btn btn-sm btn-outline-warning"
                          onClick={() => handleEdit(t)}>
                           Modifier
                        </button>
                      )}
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
      ))}
    </>
  );
};

export default TarifsList;