import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { login } from '../api/authApi';

const Login = () => {
  const [form, setForm]       = useState({ login: '', password: '' });
  const [error, setError]     = useState('');
  const [loading, setLoading] = useState(false);
  const { loginUser }         = useAuth();
  const navigate              = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    try {
      const res = await login(form);
      loginUser(res.data);
      navigate('/clients');
    } catch {
      setError('Login ou mot de passe incorrect.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center"
         style={{ minHeight: '80vh' }}>
      <div className="card shadow-lg" style={{ width: '420px' }}>
        <div className="card-header bg-primary text-white text-center py-4">
          <h4 className="mb-0"> SGR Hôtel</h4>
          <small>Système de Gestion des Réservations</small>
        </div>
        <div className="card-body p-4">
          <h5 className="text-center mb-4"> Connexion</h5>

          {error && (
            <div className="alert alert-danger">{error}</div>
          )}

          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label className="form-label fw-bold">Login</label>
              <input
                type="text"
                className="form-control"
                placeholder="Entrez votre login"
                value={form.login}
                onChange={(e) =>
                  setForm({ ...form, login: e.target.value })}
                required
                autoFocus
              />
            </div>
            <div className="mb-4">
              <label className="form-label fw-bold">Mot de passe</label>
              <input
                type="password"
                className="form-control"
                placeholder="Entrez votre mot de passe"
                value={form.password}
                onChange={(e) =>
                  setForm({ ...form, password: e.target.value })}
                required
              />
            </div>
            <button
              type="submit"
              className="btn btn-primary w-100"
              disabled={loading}
            >
              {loading ? 'Connexion...' : ' Se connecter'}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default Login;