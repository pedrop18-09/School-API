import { useEffect, useState, useCallback } from 'react';
import { Link } from 'react-router-dom';
import { ArrowLeft, UserX, RotateCcw } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';
import { getInactiveTeachers, reactivateTeacher } from '../../services/teacherService';

export default function InactiveTeachersPage() {
  const [teachers, setTeachers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [reactivatingId, setReactivatingId] = useState(null);

  const loadTeachers = useCallback(() => {
    getInactiveTeachers()
      .then(setTeachers)
      .catch(() => setTeachers([]))
      .finally(() => setLoading(false));
  }, []);

  useEffect(() => {
    loadTeachers();
  }, [loadTeachers]);

  async function handleReactivate(id) {
    setReactivatingId(id);
    try {
      await reactivateTeacher(id);
      setTeachers((prev) => prev.filter((t) => t.id !== id));
    } catch {
      alert('Não foi possível reativar este professor.');
    } finally {
      setReactivatingId(null);
    }
  }

  return (
    <div className="min-h-screen bg-[#1F3D2E]">
      <DashboardHeader />

      <main className="max-w-2xl mx-auto px-6 py-8">
        <Link
          to="/secretary/dashboard"
          className="inline-flex items-center gap-2 text-sm text-[#C9D6C9] hover:text-[#E8C468] transition-colors mb-6"
        >
          <ArrowLeft size={15} />
          Voltar
        </Link>

        <div className="flex items-center gap-2 mb-8">
          <UserX size={20} className="text-[#E8C468]" />
          <h1 className="font-serif text-2xl text-[#F5F3E7]">Professores Desativados</h1>
        </div>

        {loading ? (
          <p className="text-[#8FA697] text-sm">Carregando...</p>
        ) : teachers.length === 0 ? (
          <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-8 text-center">
            <p className="text-[#8FA697] text-sm italic">
              Nenhum professor desativado no momento.
            </p>
          </div>
        ) : (
          <ul className="space-y-3">
            {teachers.map((t) => (
              <li
                key={t.id}
                className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-4 flex items-center justify-between"
              >
                <div>
                  <p className="text-sm text-[#F5F3E7]">{t.name}</p>
                  <p className="text-xs text-[#8FA697] mt-0.5">CPF {t.cpf}</p>
                </div>
                <button
                  onClick={() => handleReactivate(t.id)}
                  disabled={reactivatingId === t.id}
                  className="flex items-center gap-1.5 bg-[#E8C468] text-[#1F3D2E] text-sm font-medium
                             px-3 py-2 rounded-sm hover:bg-[#F0D385] transition-colors disabled:opacity-60"
                >
                  <RotateCcw size={14} />
                  {reactivatingId === t.id ? 'Reativando...' : 'Reativar'}
                </button>
              </li>
            ))}
          </ul>
        )}
      </main>
    </div>
  );
}