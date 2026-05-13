import api from './axiosConfig';

export const getAllClients = (terme = '') =>
  api.get(`/clients${terme ? `?terme=${terme}` : ''}`);

export const getClientById = (id) =>
  api.get(`/clients/${id}`);

export const createClient = (data) =>
  api.post('/clients', data);

export const updateClient = (id, data) =>
  api.put(`/clients/${id}`, data);

export const desactiverClient = (id) =>
  api.delete(`/clients/${id}`);