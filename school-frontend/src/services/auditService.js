import api from './api';

export async function getAllAuditLogs() {
  const response = await api.get('/audit');
  return response.data;
}

export async function getAuditLogsByEntity(entityName) {
  const response = await api.get(`/audit/entity/${entityName}`);
  return response.data;
}

export async function getAuditLogsByDateRange(start, end) {
  const response = await api.get('/audit/date-range', {
    params: { start, end },
  });
  return response.data;
}