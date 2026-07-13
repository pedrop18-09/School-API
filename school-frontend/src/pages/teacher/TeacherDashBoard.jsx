import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { BookOpen, ChevronRight } from 'lucide-react';
import DashboardHeader from "../../components/DashboardHeader";
import { getMyClassSubjects } from "../../services/classSubjectService";

export default function TeacherDashboard() {
  const navigate = useNavigate();
  const [classSubjects, setClassSubjects] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getMyClassSubjects()
      .then(setClassSubjects)
      .catch(() => setClassSubjects([]))
      .finally(() => setLoading(false));
  }, []);

  return (
    <div className="min-h-screen bg-[#1F3D2E]">
      <DashboardHeader />

      <main className="max-w-3xl mx-auto px-6 py-8">
        <div className="flex items-center gap-2 mb-6">
          <BookOpen size={20} className="text-[#E8C468]" />
          <h1 className="font-serif text-2xl text-[#F5F3E7]">Minhas Turmas</h1>
        </div>

        {loading ? (
          <p className="text-[#8FA697] text-sm">Carregando...</p>
        ) : classSubjects.length === 0 ? (
          <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-8 text-center">
            <p className="text-[#8FA697] text-sm italic">
              Você ainda não está vinculado a nenhuma turma.
            </p>
          </div>
        ) : (
          <ul className="space-y-3">
            {classSubjects.map((cs) => (
              <li key={cs.id}>
                <button
                  onClick={() => navigate(`/teacher/class/${cs.id}`)}
                  className="w-full flex items-center justify-between bg-[#2C5240] border border-[#4A6E5A]
                             rounded-sm p-5 text-left hover:border-[#E8C468] transition-colors group"
                >
                  <div>
                    <p className="font-serif text-lg text-[#F5F3E7]">{cs.subjectName}</p>
                    <p className="text-sm text-[#C9D6C9] mt-0.5">{cs.className}</p>
                  </div>
                  <ChevronRight
                    size={18}
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