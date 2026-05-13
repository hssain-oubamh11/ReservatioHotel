import { useState, useEffect } from 'react'
import { useParams, useNavigate, Link } from 'react-router-dom'
import { getAllChambres, updateChambre } from '../../api/chambresApi.jsx'

const ChambreEdit = () => {
  const { id } = useParams()
  const navigate = useNavigate()
  const [form, setForm] = useState({ type: 'Simple', etage: 0, capaciteAccueil: 1, description: '' })

  useEffect(() => {
    getAllChambres().then((res) => {
      const c = res.data.find(x => x.id === parseInt(id))
      if (c) setForm({ type: c.type, etage: c.etage, capaciteAccueil: c.capaciteAccueil, description: c.description })
    })
  }, [id])

  const handleSubmit = async (e) => {
    e.preventDefault()
    await updateChambre(id, { ...form, etage: parseInt(form.etage), capaciteAccueil: parseInt(form.capaciteAccueil) })
    navigate('/chambres')
  }

  return (
    <>
      <div className='d-flex justify-content-between align-items-center mb-3'>
        <h2>Modifier chambre</h2>
        <Link to='/chambres' className='btn btn-secondary'>Retour</Link>
      </div>
      <div className='row'>
        <div className='col-md-6'>
          <div className='card shadow-sm'>
            <div className='card-body'>
              <form onSubmit={handleSubmit}>
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
                  <input type='number' className='form-control' value={form.etage}
                    onChange={(e) => setForm({ ...form, etage: e.target.value })} />
                </div>
                <div className='mb-3'>
                  <label className='form-label fw-bold'>Capacite</label>
                  <input type='number' className='form-control' value={form.capaciteAccueil}
                    onChange={(e) => setForm({ ...form, capaciteAccueil: e.target.value })} />
                </div>
                <div className='mb-3'>
                  <label className='form-label fw-bold'>Description</label>
                  <input type='text' className='form-control' value={form.description}
                    onChange={(e) => setForm({ ...form, description: e.target.value })} />
                </div>
                <div className='d-flex gap-2'>
                  <button type='submit' className='btn btn-warning'>Modifier</button>
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

export default ChambreEdit