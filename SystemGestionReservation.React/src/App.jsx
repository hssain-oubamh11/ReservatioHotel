import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import { AuthProvider } from './context/AuthContext.jsx'
import PrivateRoute from './components/PrivateRoute.jsx'
import Navbar from './components/Navbar.jsx'
import Login from './pages/Login.jsx'
import ClientsList from './pages/clients/ClientList.jsx'
import ClientCreate from './pages/clients/ClientCreate.jsx'
import ClientEdit from './pages/clients/ClientEdit.jsx'
import ClientDetails from './pages/clients/ClientDetails.jsx'
import ChambresList from './pages/chambres/ChambresList.jsx'
import ChambreCreate from './pages/chambres/ChambreCreate.jsx'
import ChambreEdit from './pages/chambres/ChambreEdit.jsx'
import ReservationsList from './pages/reservations/ReservationsList.jsx'
import ReservationCreate from './pages/reservations/ReservationCreate.jsx'
import FactureDetails from './pages/factures/FactureDetails.jsx'
import FacturesList from './pages/factures/FactureList.jsx'
import TarifsList from './pages/Tarifs/TarifsList.jsx'
function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Navbar />
        <div className='container mt-4'>
          <Routes>
            <Route path='/login' element={<Login />} />
            <Route path='/' element={<Navigate to='/clients' />} />
            <Route path='/clients' element={<PrivateRoute roles={['Administrateur','Receptionniste']}><ClientsList /></PrivateRoute>} />
            <Route path='/clients/create' element={<PrivateRoute roles={['Administrateur','Receptionniste']}><ClientCreate /></PrivateRoute>} />
            <Route path='/clients/:id/edit' element={<PrivateRoute roles={['Administrateur','Receptionniste']}><ClientEdit /></PrivateRoute>} />
            <Route path='/clients/:id' element={<PrivateRoute roles={['Administrateur','Receptionniste']}><ClientDetails /></PrivateRoute>} />
            <Route path='/chambres' element={<PrivateRoute roles={['Administrateur','Receptionniste']}><ChambresList /></PrivateRoute>} />
            <Route path='/chambres/create' element={<PrivateRoute roles={['Administrateur']}><ChambreCreate /></PrivateRoute>} />
            <Route path='/chambres/:id/edit' element={<PrivateRoute roles={['Administrateur']}><ChambreEdit /></PrivateRoute>} />
            <Route path='/reservations' element={<PrivateRoute roles={['Administrateur','Receptionniste']}><ReservationsList /></PrivateRoute>} />
            <Route path='/reservations/create' element={<PrivateRoute roles={['Administrateur','Receptionniste']}><ReservationCreate /></PrivateRoute>} />
            <Route path='/factures/:id' element={<PrivateRoute roles={['Administrateur','Receptionniste']}><FactureDetails /></PrivateRoute>} />
            <Route path='/factures' element={<PrivateRoute roles={['Administrateur','Receptionniste']}> <FacturesList /></PrivateRoute>} />
            <Route path='/acces-refuse' element={<div className='alert alert-danger mt-5 text-center'><h4>Acces refuse</h4></div>} />
            <Route path='/Tarifs' element={<PrivateRoute roles={['Administrateur']}> <TarifsList /></PrivateRoute>} />
          </Routes>
        </div>
      </BrowserRouter>
    </AuthProvider>
  )
}

export default App