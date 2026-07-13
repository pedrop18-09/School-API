import { useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { ArrowLeft, TrendingUp, TrendingDown, CheckCircle2, XCircle } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';
import { getClassPerformance } from '../../services/gradeService';

const quarters = [
  { value: 'FirstQuarter', label: '1º Trimestre' },
  { value: 'SecondQuarter', label: '2º Trimestre' },
  { value: 'ThirdQuarter', label: '3º Trimestre' },
];

export default function TeacherClassPerformance() {
  const { classSubjectId } = useParams();
  const [quarter, setQuarter] = useState('');
  const [performance, setPerformance] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  function handleQuarterChange(value) {
    setQuarter(value);
    setError('');
    setPerformance(null);

    if (!value) return;

    setLoading(true);
    getClassPerformance(classSubjectId, value)
      .then(setPerformance)
      .catch(() => setError('Não foi possível carregar o desempenho desta turma.'))
      .finally(() => setLoading(false));
  }

  return (
    <div className="min-h-screen bg-[#1F3D2E]">
      <DashboardHeader />

      <main className="max-w-3xl mx-auto px-6 py-8">
        <Link
          to={`/teacher/class/${classSubjectId}`}
          className="inline-flex items-center gap-2 text-sm text-[#C9D6C9] hover:text-[#E8C468] transition-colors mb-6"
        >
          <ArrowLeft size={15} />
          Voltar
        </Link>

        <h1 className="font-serif text-2xl text-[#F5F3E7] mb-6">Desempenho da Turma</h1>

        <div className="mb-6">
          <label className="block font-mono text-[11px] tracking-widest text-[#C9D6C9] uppercase mb-2">
            Trimestre
          </label>
          <select
            value={quarter}
            onChange={(e) => handleQuarterChange(e.target.value)}
            className="w-full sm:w-64 bg-[#173226] border border-[#4A6E5A] rounded-sm py-3 px-3
                       text-[#F5F3E7] focus:outline-none focus:border-[#E8C468] transition-colors"
          >
            <option value="">Selecione...</option>
            {quarters.map((q) => (
              <option key={q.value} value={q.value}>{q.label}</option>
            ))}
          </select>
        </div>

        {loading && <p className="text-[#8FA697] text-sm">Carregando...</p>}
        {error && <p className="text-[#E27D60] text-sm">{error}</p>}

        {performance && (
          <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-6">
            <div className="flex items-center justify-between border-b border-[#4A6E5A] pb-4 mb-4">
              <div>
                <p className="font-serif text-xl text-[#F5F3E7]">{performance.subjectName}</p>
                <p className="text-sm text-[#C9D6C9]">{performance.className}</p>
              </div>
              <div className="text-right">
                <p className="font-mono text-[11px] tracking-widest text-[#8FA697] uppercase">Média da turma</p>
                <p className="font-serif text-2xl text-[#E8C468]">{performance.classAverage.toFixed(1)}</p>
              </div>
            </div>

            {performance.students.length === 0 ? (
              <p className="text-center text-[#8FA697] text-sm italic py-6">
                Nenhuma nota lançada neste trimestre ainda.
              </p>
            ) : (
              <ul className="divide-y divide-[#4A6E5A]">
                {performance.students.map((s) => (
                  <li key={s.studentId} className="py-3 flex items-center justify-between">
                    <span className="text-sm text-[#F5F3E7]">{s.studentName}</span>

                    <div className="flex items-center gap-4">
                      <span className="font-mono text-sm text-[#F5F3E7] w-10 text-right">
                        {s.average.toFixed(1)}
                      </span>

                      <span
                        title={s.aboveAverage ? 'Acima da média da turma' : 'Abaixo da média da turma'}
                        className="flex items-center"
                      >
                        {s.aboveAverage ? (
                          <TrendingUp size={16} className="text-[#7FB88F]" />
                        ) : (
                          <TrendingDown size={16} className="text-[#E27D60]" />
                        )}
                      </span>

                      <span
                        title={s.approved ? 'Aprovado (nota mínima atingida)' : 'Abaixo da nota mínima'}
                        className="flex items-center"
                      >
                        {s.approved ? (
                          <CheckCircle2 size={16} className="text-[#7FB88F]" />
                        ) : (
                          <XCircle size={16} className="text-[#E27D60]" />
                        )}
                      </span>
                    </div>
                  </li>
                ))}
              </ul>
            )}
          </div>
        )}
      </main>
    </div>
  );
}