import { useState } from 'react'
import { useNavigate, Link } from 'react-router-dom'
import { createChambre } from '../../api/chambresApi.jsx'

const ChambreCreate = () => {
  const navigate = useNavigate()
  const [form, setForm] = useState({ numero: '', type: 'Simple', etage: 0, capaciteAccueil: 1, description: '' })
  const [error, setError] = useState('')

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      await createChambre({ ...form, etage: parseInt(form.etage), capaciteAccueil: parseInt(form.capaciteAccueil) })
      navigate('/chambres')
    } catch (err) {
      setError(err.response?.data?.message || 'Erreur creation.')
    }
  }

  return (
    <>
      <div className='d-flex justify-content-between align-items-center mb-3'>
        <h2>+ Nouvelle chambre</h2>
        <Link to='/chambres' className='btn btn-secondary'>Retour</Link>
      </div>
      <div className='row'>
        <div className='col-md-6'>
          <div className='card shadow-sm'>
            <div className='card-body'>
              {error && <div className='alert alert-danger'>{error}</div>}
              <form onSubmit={handleSubmit}>
                <div className='mb-3'>
                  <label className='form-label fw-bold'>Numero</label>
                  <input type='text' className='form-control' value={form.numero}
                    onChange={(e) => setForm({ ...form, numero: e.target.value })} required />
                </div>
                <div className='mb-3'>
                  <label className='form-label fw-bold'>Type</label>
                  <select className='form-select' value={form.type}
                    onChange={(e) => setForm({ ...form, type: e.target.value })}>
                    <option value='Simple'>Simple</option>
                    <option value='Double'>Double</option>
                    <option value='Suite'>Suite</option>
                    <option value='Familiale'>Familiale</option>
                  </select>
                </div>
                <div className='mb-3'>
                  <label className='form-label fw-bold'>Etage</label>
                  <input type='number' min='0' max='50' className='form-control' value={form.etage}
                    onChange={(e) => setForm({ ...form, etage: e.target.value })} />
                </div>
                <div className='mb-3'>
                  <label className='form-label fw-bold'>Capacite</label>
                  <input type='number' min='1' max='20' className='form-control' value={form.capaciteAccueil}
                    onChange={(e) => setForm({ ...form, capaciteAccueil: e.target.value })} />
                </div>
                <div className='mb-3'>
                  <label className='form-label fw-bold'>Description</label>
                  <input type='text' className='form-control' value={form.description}
                    onChange={(e) => setForm({ ...form, description: e.target.value })} />
                </div>
                <div className='d-flex gap-2'>
                  <button type='submit' className='btn btn-primary'>Enregistrer</button>
                  <Link to='/chambres' className='btn btn-outline-secondary'>Annuler</Link>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </>
  )
}

export default ChambreCreate