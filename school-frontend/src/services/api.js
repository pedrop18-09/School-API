import axios from 'axios';

const apiUrl = import.meta.env.VITE_API_URL;

console.log(apiUrl)
const api = axios.create({
  baseURL: apiUrl,
});

// Interceptor de requisição: adiciona o token JWT automaticamente, se existir
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

// Interceptor de resposta: se a API disser que o token não é mais válido (401),
// limpa a sessão e manda o usuário de volta pra tela inicial
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      localStorage.removeItem('name');
      localStorage.removeItem('role');
      window.location.href = '/';
    }
    return Promise.reject(error);
  }
);

export default api;