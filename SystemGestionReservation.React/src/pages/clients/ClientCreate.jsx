import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { createClient } from '../../api/clientsApi';
import AlertMessage from '../../components/AlertMessage';

const ClientCreate = () => {
const navigate = useNavigate();
const [form, setForm] = useState({
nom: '', prenom: '', numeroIdentite: '',
    adresse: '', telephone: '', email: ''
  });
const [error, setError] = useState('');

const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    try
    {
        await createClient(form);
        navigate('/clients');
    }
    catch (err)
    {
        setError(err.response?.data?.message || 'Erreur lors de la création.');
    }
};

return (

  <>

    < div className = "d-flex justify-content-between align-items-center mb-3" >

      < h2 > Nouveau client</ h2 >

      < Link to = "/clients" className = "btn btn-secondary" >
           Retour
        </ Link >
      </ div >

      < div className = "row" >
        < div className = "col-md-6" >
          < div className = "card shadow-sm" >
            < div className = "card-body" >
              < AlertMessage type = "danger" message ={ error}
onClose ={ () => setError('')} />

< form onSubmit ={ handleSubmit}>
                {
    [
                  { name: 'nom',            label: 'Nom',              type: 'text'  },
                  { name: 'prenom',         label: 'Prénom',           type: 'text'  },
                  { name: 'numeroIdentite', label: "N° d'identité",    type: 'text'  },
                  { name: 'adresse',        label: 'Adresse',          type: 'text'  },
                  { name: 'telephone',      label: 'Téléphone',        type: 'tel'   },
                  { name: 'email',          label: 'Email',            type: 'email' },
                ].map(({ name, label, type }) => (
                  < div className = "mb-3" key ={ name}>
                    < label className = "form-label fw-bold" >{ label}</ label >
                    < input
                      type ={ type}
    className = "form-control"
                      value ={ form[name]}
    onChange ={
        (e) =>
      setForm({ ...form, [name]: e.target.value })}
                      required ={
                      ['nom', 'prenom', 'numeroIdentite']
                                 .includes(name)}
                    />
                  </ div >
                ))}
                < div className = "d-flex gap-2" >
                  < button type = "submit" className = "btn btn-primary" >
                     Enregistrer
                  </ button >
                  < Link to = "/clients"
                        className = "btn btn-outline-secondary" >
                    Annuler
                  </ Link >
                </ div >
              </ form >
            </ div >
          </ div >
        </ div >
      </ div >
    </>
  );
};

export default ClientCreate;