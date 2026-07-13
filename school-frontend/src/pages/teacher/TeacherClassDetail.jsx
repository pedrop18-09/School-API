import { useNavigate, useParams, Link } from 'react-router-dom';
import { ArrowLeft, PenLine, BarChart3 } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';

export default function TeacherClassDetail() {
  const navigate = useNavigate();
  const { classSubjectId } = useParams();

  return (
    <div className="min-h-screen bg-[#1F3D2E]">
      <DashboardHeader />

      <main className="max-w-2xl mx-auto px-6 py-8">
        <Link
          to="/teacher/dashboard"
          className="inline-flex items-center gap-2 text-sm text-[#C9D6C9] hover:text-[#E8C468] transition-colors mb-8"
        >
          <ArrowLeft size={15} />
          Minhas turmas
        </Link>

        <h1 className="font-serif text-2xl text-[#F5F3E7] mb-8">O que deseja fazer?</h1>

        <div className="grid sm:grid-cols-2 gap-5">
          <button
            onClick={() => navigate(`/teacher/class/${classSubjectId}/grades`)}
            className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-6 text-left
                       hover:border-[#E8C468] hover:-translate-y-1 transition-all"
          >
            <PenLine size={22} className="text-[#E8C468] mb-4" />
            <h2 className="font-serif text-xl text-[#F5F3E7] mb-1">Lançar Notas</h2>
            <p className="text-sm text-[#C9D6C9]">Registrar ou editar notas dos alunos.</p>
          </button>

          <button
            onClick={() => navigate(`/teacher/class/${classSubjectId}/performance`)}
            className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-6 text-left
                       hover:border-[#E8C468] hover:-translate-y-1 transition-all"
          >
            <BarChart3 size={22} className="text-[#E8C468] mb-4" />
            <h2 className="font-serif text-xl text-[#F5F3E7] mb-1">Ver Desempenho</h2>
            <p className="text-sm text-[#C9D6C9]">Média da turma e situação de cada aluno.</p>
          </button>
        </div>
      </main>
    </div>
  );
}