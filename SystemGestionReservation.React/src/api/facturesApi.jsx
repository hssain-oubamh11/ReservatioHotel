import api from './axiosConfig';

export const getFactureById = (id) =>
  api.get(`/factures/${id}`);

export const getFacturesByClient = (clientId) =>
  api.get(`/factures/client/${clientId}`);

export const getFactureByReservation = (reservationId) =>
  api.get(`/factures/reservation/${reservationId}`);
export const getAllFactures = () => api.get('/factures');