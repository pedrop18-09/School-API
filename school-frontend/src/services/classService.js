import api from './api';

export async function getAllClasses() {
  const response = await api.get('/class');
  return response.data;
}

export async function getClassById(id) {
  const response = await api.get(`/class/${id}`);
  return response.data;
}

export async function createClass(data) {
  const response = await api.post('/class', data);
  return response.data;
}

export async function updateClass(id, data) {
  const response = await api.put(`/class/${id}`, data);
  return response.data;
}

export async function deleteClass(id) {
  await api.delete(`/class/${id}`);
}