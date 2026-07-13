import api from './api';

export async function getAllSubjects() {
  const response = await api.get('/subject');
  return response.data;
}

export async function createSubject(data) {
  const response = await api.post('/subject', data);
  return response.data;
}

export async function updateSubject(id, data) {
  const response = await api.put(`/subject/${id}`, data);
  return response.data;
}

export async function deleteSubject(id) {
  await api.delete(`/subject/${id}`);
}