import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getAllChambres, desactiverChambre } from '../../api/chambresApi';
import { useAuth } from '../../context/AuthContext';
import AlertMessage from '../../components/AlertMessage';

const ChambresList = () => {
  const [chambres, setChambres] = useState([]);
  const [alert, setAlert]       = useState({ type: '', message: '' });
  const { isAdmin }             = useAuth();

  const fetchChambres = async () => {
    const res = await getAllChambres();
    setChambres(res.data);
  };

  useEffect(() => { fetchChambres(); }, []);

  const handleDesactiver = async (id) => {
    if (!window.confirm('Désactiver cette chambre ?')) return;
    await desactiverChambre(id);
    setAlert({ type: 'success', message: 'Chambre désactivée.' });
    fetchChambres();
  };

  return (
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2> Chambres</h2>
        {isAdmin() && (
          <Link to="/chambres/create" className="btn btn-primary">
             Nouvelle chambre
          </Link>
        )}
      </div>

      <AlertMessage type={alert.type} message={alert.message}
                    onClose={() => setAlert({ type: '', message: '' })} />

      <div className="row row-cols-1 row-cols-md-3 g-4">
        {chambres.map((chambre) => (
          <div className="col" key={chambre.id}>
            <div className="card h-100 shadow-sm">
              <div className={`card-header d-flex justify-content-between
                align-items-center
                ${chambre.estActive ? 'bg-success' : 'bg-secondary'}
                text-white`}>
                <span className="fw-bold">
                   Chambre N° {chambre.numero}
                </span>
                <span className="badge bg-white text-dark">
                  {chambre.type}
                </span>
              </div>
              <div className="card-body">
                <p className="mb-1">
                  <strong>Étage :</strong> {chambre.etage}
                </p>
                <p className="mb-1">
                  <strong>Capacité :</strong> {chambre.capaciteAccueil} pers.
                </p>
                <p className="mb-2 text-muted small">
                  {chambre.description}
                </p>
                <div>
                  {chambre.equipements?.map((eq, i) => (
                    <span key={i}
                          className="badge bg-info text-dark me-1 mb-1">
                      {eq}
                    </span>
                  ))}
                </div>
              </div>
              {isAdmin() && (
                <div className="card-footer bg-white d-flex gap-2">
                  <Link to={`/chambres/${chambre.id}/edit`}
                        className="btn btn-sm btn-warning flex-fill">
                     Modifier
                  </Link>
                  <button
                    className="btn btn-sm btn-danger flex-fill"
                    onClick={() => handleDesactiver(chambre.id)}>
                     Désactiver
                  </button>
                </div>
              )}
            </div>
          </div>
        ))}
      </div>
    </>
  );
};

export default ChambresList;