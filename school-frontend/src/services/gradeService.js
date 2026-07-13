import api from './api';

export async function getGradeById(id) {
  const response = await api.get(`/grade/${id}`);
  return response.data;
}

export async function createGrade(data) {
  const response = await api.post('/grade', data);
  return response.data;
}

export async function updateGrade(id, data) {
  const response = await api.put(`/grade/${id}`, data);
  return response.data;
}

export async function deleteGrade(id) {
  await api.delete(`/grade/${id}`);
}

export async function getClassPerformance(classSubjectId, quarter) {
  const response = await api.get(`/grade/class-performance/${classSubjectId}`, {
    params: { quarter },
  });
  return response.data;
}

export async function getEntrySheet(classSubjectId, quarter) {
  const response = await api.get(`/grade/entry-sheet/${classSubjectId}`, {
    params: { quarter },
  });
  return response.data;
}

export async function getMyGrades(year) {
  const response = await api.get('/grade/my-grades', { params: { year } });
  return response.data;
}

export async function getMyYears() {
  const response = await api.get('/grade/my-years');
  return response.data;
}