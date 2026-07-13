import api from './api';

export async function getAllTeachers() {
  const response = await api.get('/teacher');
  return response.data;
}

export async function getTeacherById(id) {
  const response = await api.get(`/teacher/${id}`);
  return response.data;
}

export async function createTeacher(data) {
  const response = await api.post('/teacher', data);
  return response.data;
}

export async function updateTeacher(id, data) {
  const response = await api.put(`/teacher/${id}`, data);
  return response.data;
}

export async function deleteTeacher(id) {
  await api.delete(`/teacher/${id}`);
}

export async function getInactiveTeachers() {
  const response = await api.get('/teacher/inactive');
  return response.data;
}

export async function reactivateTeacher(id) {
  const response = await api.put(`/teacher/${id}/reactivate`);
  return response.data;
}