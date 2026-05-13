import api from './axiosConfig';

export const getAllChambres = () =>
  api.get('/chambres');

export const getChambresDisponibles = (dateArrivee, dateDepart, type = '') =>
  api.get('/chambres/disponibles', {
    params: { dateArrivee, dateDepart, ...(type && { type }) }
  });
export const createChambre = (data) =>
  api.post('/chambres', data);

export const updateChambre = (id, data) =>
  api.put(`/chambres/${id}`, data);

export const desactiverChambre = (id) =>
  api.delete(`/chambres/${id}`);

export const ajouterEquipement = (id, nomEquipement) =>
  api.post(`/chambres/${id}/equipements`, nomEquipement, {
    headers: { 'Content-Type': 'application/json' }
  });