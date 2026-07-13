import { useEffect, useState, useMemo } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { ArrowLeft, Link2, Search, ChevronRight } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';
import { getAllClassSubjects } from '../../services/classSubjectService';

export default function ClassSubjectsListPage() {
  const navigate = useNavigate();
  const [classSubjects, setClassSubjects] = useState([]);
  const [loading, setLoading] = useState(true);
  const [query, setQuery] = useState('');

  useEffect(() => {
    getAllClassSubjects()
      .then(setClassSubjects)
      .catch(() => setClassSubjects([]))
      .finally(() => setLoading(false));
  }, []);

  const filtered = useMemo(() => {
    if (!query.trim()) return classSubjects;
    const q = query.trim().toLowerCase();
    return classSubjects.filter((cs) =>
      cs.className.toLowerCase().includes(q) ||
      cs.subjectName.toLowerCase().includes(q) ||
      cs.teacherName.toLowerCase().includes(q)
    );
  }, [classSubjects, query]);

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

        <div className="flex items-center justify-between mb-6 flex-wrap gap-3">
          <div className="flex items-center gap-2">
            <Link2 size={20} className="text-[#E8C468]" />
            <h1 className="font-serif text-2xl text-[#F5F3E7]">Vínculos Turma-Disciplina</h1>
          </div>
          <div className="flex items-center gap-4">
            <Link
              to="/secretary/classes/new"
              className="text-sm text-[#E8C468] hover:text-[#F0D385] transition-colors font-medium"
            >
              + Criar Turma
            </Link>
            <Link
              to="/secretary/classes"
              className="text-sm text-[#E8C468] hover:text-[#F0D385] transition-colors font-medium"
            >
              Lista de Turmas
            </Link>
            <button
              onClick={() => navigate('/secretary/class-subjects/new')}
              className="text-sm text-[#E8C468] hover:text-[#F0D385] transition-colors font-medium"
            >
              + Novo Vínculo
            </button>
          </div>
        </div>

        <div className="relative mb-5">
          <Search size={16} className="absolute left-3 top-1/2 -translate-y-1/2 text-[#8FA697]" />
          <input
            type="text"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
            placeholder="Pesquisar por turma, disciplina ou professor..."
            className="w-full bg-[#173226] border border-[#4A6E5A] rounded-sm py-2.5 pl-10 pr-3
                       text-sm text-[#F5F3E7] placeholder:text-[#5C7A68]
                       focus:outline-none focus:border-[#E8C468] transition-colors"
          />
        </div>

        {loading ? (
          <p className="text-[#8FA697] text-sm">Carregando...</p>
        ) : filtered.length === 0 ? (
          <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-8 text-center">
            <p className="text-[#8FA697] text-sm italic">
              {query ? 'Nenhum resultado encontrado.' : 'Nenhum vínculo cadastrado ainda.'}
            </p>
          </div>
        ) : (
          <ul className="space-y-2">
            {filtered.map((cs) => (
              <li key={cs.id}>
                <button
                  onClick={() => navigate(`/secretary/class-subjects/${cs.id}/edit`)}
                  className="w-full flex items-center justify-between bg-[#2C5240] border border-[#4A6E5A]
                             rounded-sm p-4 text-left hover:border-[#E8C468] transition-colors group"
                >
                  <div>
                    <p className="text-sm text-[#F5F3E7]">
                      {cs.className} · <span className="text-[#C9D6C9]">{cs.subjectName}</span>
                    </p>
                    <p className="text-xs text-[#8FA697] mt-0.5">Prof. {cs.teacherName}</p>
                  </div>
                  <ChevronRight
                    size={16}
                    className="text-[#8FA697] group-hover:text-[#E8C468] group-hover:translate-x-1 transition-all"
                  />
                </button>
              </li>
            ))}
          </ul>
        )}
      </main>
    </div>
  );
}