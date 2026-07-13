import { Navigate } from 'react-router-dom';
import { getCurrentUser } from '../services/authService';

export default function ProtectedRoute({ allowedRoles, children }) {
  const user = getCurrentUser();

  // Ninguém logado -> manda pra Home
  if (!user) {
    return <Navigate to="/" replace />;
  }

  // Logado, mas com Role errado pra essa rota -> manda pra Home também
  if (allowedRoles && !allowedRoles.includes(user.role)) {
    return <Navigate to="/" replace />;
  }

  return children;
}