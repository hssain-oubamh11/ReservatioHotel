import api from './axiosConfig';

export const getReservationsByClient = (clientId) =>
  api.get(`/reservations/client/${clientId}`);

export const getReservationById = (id) =>
  api.get(`/reservations/${id}`);

export const createReservation = (data) =>
  api.post('/reservations', data);

export const updateReservation = (id, data) =>
  api.put(`/reservations/${id}`, data);

export const checkIn = (id) =>
  api.post(`/reservations/${id}/checkin`);

export const checkOut = (id) =>
  api.post(`/reservations/${id}/checkout`);

export const annulerReservation = (id) =>
  api.post(`/reservations/${id}/annuler`);

export const appliquerRemise = (id, pourcentage) =>
  api.post(`/reservations/${id}/remise`, pourcentage, {
    headers: { 'Content-Type': 'application/json' }
  });