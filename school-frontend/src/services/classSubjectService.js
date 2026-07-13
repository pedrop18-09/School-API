import api from './api';

// ===================== TEACHER =====================
export async function getMyClassSubjects() {
  const response = await api.get('/class-subject/my');
  return response.data;
}

// ===================== SECRETARY =====================
export async function getAllClassSubjects() {
  const response = await api.get('/class-subject');
  return response.data;
}

export async function getClassSubjectById(id) {
  const response = await api.get(`/class-subject/${id}`);
  return response.data;
}

export async function createClassSubject(data) {
  const response = await api.post('/class-subject', data);
  return response.data;
}

export async function updateClassSubject(id, data) {
  const response = await api.put(`/class-subject/${id}`, data);
  return response.data;
}

export async function deleteClassSubject(id) {
  await api.delete(`/class-subject/${id}`);
}