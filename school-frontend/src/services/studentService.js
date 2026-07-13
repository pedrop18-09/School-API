import api from './api';

export async function getAllStudents() {
  const response = await api.get('/student');
  return response.data;
}

export async function getStudentById(id) {
  const response = await api.get(`/student/${id}`);
  return response.data;
}

export async function createStudent(data) {
  const response = await api.post('/student', data);
  return response.data;
}

export async function updateStudent(id, data) {
  const response = await api.put(`/student/${id}`, data);
  return response.data;
}

export async function deleteStudent(id) {
  await api.delete(`/student/${id}`);
}

export async function getInactiveStudents() {
  const response = await api.get('/student/inactive');
  return response.data;
}

export async function reactivateStudent(id) {
  const response = await api.put(`/student/${id}/reactivate`);
  return response.data;
}