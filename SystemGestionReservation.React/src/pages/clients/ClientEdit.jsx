import { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import { getClientById, updateClient } from '../../api/clientsApi';
import AlertMessage from '../../components/AlertMessage';

const ClientEdit = () => {
  const { id }   = useParams();
  const navigate = useNavigate();
  const [form, setForm] = useState({
    nom: '', prenom: '', adresse: '', telephone: '', email: ''
  });
  const [error, setError] = useState('');

  useEffect(() => {
    getClientById(id).then((res) => {
      const c = res.data;
      setForm({
        nom: c.nom, prenom: c.prenom,
        adresse: c.adresse, telephone: c.telephone, email: c.email
      });
    });
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await updateClient(id, form);
      navigate('/clients');
    } catch (err) {
      setError(err.response?.data?.message || 'Erreur lors de la mise à jour.');
    }
  };

  return (
    <>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2> Modifier le client</h2>
        <Link to="/clients" className="btn btn-secondary">Retour</Link>
      </div>

      <div className="row">
        <div className="col-md-6">
          <div className="card shadow-sm">
            <div className="card-body">
              <AlertMessage type="danger" message={error}
                            onClose={() => setError('')} />
              <form onSubmit={handleSubmit}>
                {[
                  { name: 'nom',       label: 'Nom',       type: 'text'  },
                  { name: 'prenom',    label: 'Prénom',    type: 'text'  },
                  { name: 'adresse',   label: 'Adresse',   type: 'text'  },
                  { name: 'telephone', label: 'Téléphone', type: 'tel'   },
                  { name: 'email',     label: 'Email',     type: 'email' },
                ].map(({ name, label, type }) => (
                  <div className="mb-3" key={name}>
                    <label className="form-label fw-bold">{label}</label>
                    <input
                      type={type}
                      className="form-control"
                      value={form[name]}
                      onChange={(e) =>
                        setForm({ ...form, [name]: e.target.value })}
                    />
                  </div>
                ))}
                <div className="d-flex gap-2">
                  <button type="submit" className="btn btn-warning">
                     Modifier
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

export default ClientEdit;