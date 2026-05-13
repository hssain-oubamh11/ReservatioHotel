import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const Navbar = () => {
  const { user, logoutUser, isReceptionniste } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logoutUser();
    navigate('/login');
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary shadow">
      <div className="container">
        <Link className="navbar-brand fw-bold" to="/clients">
          <i className="bi bi-building"></i> SGR Hôtel
        </Link>

        {user && (
          <div className="d-flex align-items-center gap-3 ms-auto">
            {isReceptionniste() && (
              <>
                <Link className="nav-link text-white" to="/clients">
                   Clients
                </Link>
                <Link className="nav-link text-white" to="/chambres">
                   Chambres
                </Link>
                <Link className="nav-link text-white"
                      to="/reservations/create">
                  Réservation
                </Link>
               <Link className="nav-link text-white" to="/factures">
                  Factures
                </Link>
                <Link className="nav-link text-white" to="/Tarifs">
                 Tarifs
                </Link>

                
              </>
            )}
            <span className="text-white-50 small">
              {user.login}
              <span className="badge bg-warning text-dark ms-2">
                {user.role}
              </span>
            </span>
            <button className="btn btn-outline-light btn-sm"
                    onClick={handleLogout}>
               Déconnexion
            </button>
          </div>
        )}
      </div>
    </nav>
  );
};

export default Navbar;