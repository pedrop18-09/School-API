import api from './api';

export async function loginSecretary(email, password) {
  const response = await api.post('/auth/secretary/login', { email, password });
  saveSession(response.data);
  return response.data;
}

export async function loginTeacher(cpf, password) {
  const response = await api.post('/auth/teacher/login', { cpf, password });
  saveSession(response.data);
  return response.data;
}

export async function loginStudent(cpf, password) {
  const response = await api.post('/auth/student/login', { cpf, password });
  saveSession(response.data);
  return response.data;
}

// Guarda o token e os dados do usuário logado no localStorage
function saveSession(authResponse) {
  localStorage.setItem('token', authResponse.token);
  localStorage.setItem('name', authResponse.name);
  localStorage.setItem('role', authResponse.role);
}

export function logout() {
  localStorage.removeItem('token');
  localStorage.removeItem('name');
  localStorage.removeItem('role');
}

export function getCurrentUser() {
  const token = localStorage.getItem('token');
  if (!token) return null;

  return {
    token,
    name: localStorage.getItem('name'),
    role: localStorage.getItem('role'),
  };
}