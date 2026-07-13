import { useNavigate } from 'react-router-dom';
import { LogOut, School } from 'lucide-react';
import { logout, getCurrentUser } from '../services/authService';

export default function DashboardHeader() {
  const navigate = useNavigate();
  const user = getCurrentUser();

  function handleLogout() {
    logout();
    navigate('/');
  }

  return (
    <header className="sticky top-0 z-20 bg-[#1F3D2E]/95 backdrop-blur border-b border-[#4A6E5A] px-6 py-4">
      <div className="max-w-6xl mx-auto flex items-center justify-between">
        <div className="flex items-center gap-3">
          <School size={22} className="text-[#E8C468]" />
          <span className="font-serif text-xl text-[#F5F3E7]">Sistema Escolar</span>
        </div>

        <div className="flex items-center gap-4">
          <span className="text-sm text-[#C9D6C9]">
            Olá, <span className="text-[#F5F3E7] font-medium">{user?.name}</span>
          </span>
          <button
            onClick={handleLogout}
            className="flex items-center gap-2 text-sm text-[#C9D6C9] hover:text-[#E27D60] transition-colors"
          >
            <LogOut size={16} />
            Sair
          </button>
        </div>
      </div>
    </header>
  );
}