import { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { getAllClients } from '../../api/clientsApi';
import { getChambresDisponibles } from '../../api/chambresApi';
import { createReservation } from '../../api/reservationsApi';
import AlertMessage from '../../components/AlertMessage';

const ReservationCreate = () => {
  const navigate = useNavigate();
  const [form, setForm] = useState({
    clientId: '', chambreId: '',
    dateArrivee: '', dateDepart: '', nombrePersonnes: 1
  });
  const [clients,  setClients]  = useState([]);
  const [chambres, setChambres] = useState([]);
  const [error,    setError]    = useState('');

  useEffect(() => {
    getAllClients().then((r) => setClients(r.data));
  }, []);

  useEffect(() => {
    if (form.dateArrivee && form.dateDepart) {
      getChambresDisponibles(form.dateArrivee, form.dateDepart)
        .then((r) => setChambres(r.data));
    }
  }, [form.dateArrivee, form.dateDepart]);

  const nuits = form.dateArrivee && form.dateDepart
    ? Math.max(0, Math.round(
        (new Date(form.dateDepart) - new Date(form.dateArrivee))
        / (1000 * 60 * 60 * 24)))
    : 0;

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createReservation({
        ...form,
        clientId:       parseInt(form.clientId),
        chambreId:      parseInt(form.chambreId),
        nombrePersonnes: parseInt(form.nombrePersonnes)
      });
      navigate(`/reservations?clientId=${form.clientId}`);
    } catch (err) {
      setError(err.response?.data?.message || 'Erreur de création.');
    }
  };

  return (
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2> Nouvelle réservation</h2>
        <Link to="/clients" className="btn btn-secondary">Retour</Link>
      </div>

      <div className="row">
        <div className="col-md-7">
          <div className="card shadow-sm">
            <div className="card-body">
              <AlertMessage type="danger" message={error}
                            onClose={() => setError('')} />
              <form onSubmit={handleSubmit}>

                <div className="mb-3">
                  <label className="form-label fw-bold">Client</label>
                  <select
                    className="form-select"
                    value={form.clientId}
                    onChange={(e) =>
                      setForm({ ...form, clientId: e.target.value })}
                    required>
                    <option value="">-- Sélectionner un client --</option>
                    {clients.map((c) => (
                      <option key={c.id} value={c.id}>
                        {c.prenom} {c.nom}
                      </option>
                    ))}
                  </select>
                </div>

                <div className="row">
                  <div className="col-md-6 mb-3">
                    <label className="form-label fw-bold">
                      Date d'arrivée
                    </label>
                    <input
                      type="date" className="form-control"
                      value={form.dateArrivee}
                      onChange={(e) =>
                        setForm({ ...form, dateArrivee: e.target.value })}
                      required />
                  </div>
                  <div className="col-md-6 mb-3">
                    <label className="form-label fw-bold">
                      Date de départ
                    </label>
                    <input
                      type="date" className="form-control"
                      value={form.dateDepart}
                      onChange={(e) =>
                        setForm({ ...form, dateDepart: e.target.value })}
                      required />
                  </div>
                </div>

                {nuits > 0 && (
                  <div className="alert alert-info py-2">
                    🌙 Durée du séjour : <strong>{nuits} nuit(s)</strong>
                  </div>
                )}

                <div className="mb-3">
                  <label className="form-label fw-bold">Chambre</label>
                  <select
                    className="form-select"
                    value={form.chambreId}
                    onChange={(e) =>
                      setForm({ ...form, chambreId: e.target.value })}
                    required
                    disabled={chambres.length === 0}>
                    <option value="">
                      {chambres.length === 0
                        ? '-- Choisir les dates d\'abord --'
                        : '-- Sélectionner une chambre --'}
                    </option>
                    {chambres.map((c) => (
                      <option key={c.id} value={c.id}>
                        N°{c.numero} — {c.type} (Étage {c.etage})
                      </option>
                    ))}
                  </select>
                </div>

                <div className="mb-3">
                  <label className="form-label fw-bold">
                    Nombre de personnes
                  </label>
                  <input
                    type="number" min="1" max="20"
                    className="form-control"
                    value={form.nombrePersonnes}
                    onChange={(e) =>
                      setForm({ ...form, nombrePersonnes: e.target.value })}
                    required />
                </div>

                <div className="d-flex gap-2">
                  <button type="submit" className="btn btn-primary">
                    💾 Confirmer la réservation
                  </button>
                  <Link to="/clients"
                        className="btn btn-outline-secondary">
                    Annuler
                  </Link>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default ReservationCreate;