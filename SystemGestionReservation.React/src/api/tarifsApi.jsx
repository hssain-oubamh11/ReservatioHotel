import api from './axiosConfig';

export const getAllTarifs = () =>
  api.get('/tarifs');

export const updateTarif = (id, data) =>
  api.put(`/tarifs/${id}`, data);

