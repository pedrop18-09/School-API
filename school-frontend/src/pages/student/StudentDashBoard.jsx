import { useEffect, useState } from 'react';
import { GraduationCap, BookOpen } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';
import { getMyGrades, getMyYears } from '../../services/gradeService';

const quarterOrder = ['FirstQuarter', 'SecondQuarter', 'ThirdQuarter'];
const quarterLabels = {
  FirstQuarter: '1º Tri',
  SecondQuarter: '2º Tri',
  ThirdQuarter: '3º Tri',
};

export default function StudentDashboard() {
  const [years, setYears] = useState([]);
  const [selectedYear, setSelectedYear] = useState(null);
  const [data, setData] = useState(null);
  const [loadingYears, setLoadingYears] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    getMyYears()
      .then((yearsList) => {
        setYears(yearsList);
        if (yearsList.length > 0) {
          setSelectedYear(yearsList[0]); // mais recente primeiro (já vem ordenado desc)
        }
      })
      .catch(() => setError('Não foi possível carregar seus anos letivos.'))
      .finally(() => setLoadingYears(false));
  }, []);

  useEffect(() => {
    if (!selectedYear) return;

    let cancelled = false;

    getMyGrades(selectedYear)
      .then((result) => {
        if (!cancelled) {
          setData(result);
          setError('');
        }
      })
      .catch(() => {
        if (!cancelled) setError('Não foi possível carregar suas notas.');
      });

    return () => {
      cancelled = true;
    };
  }, [selectedYear]);

  return (
    <div className="min-h-screen bg-[#1F3D2E]">
      <DashboardHeader />

      <main className="max-w-3xl mx-auto px-6 py-8">
        <div className="flex items-center justify-between flex-wrap gap-4 mb-6">
          <div>
            <div className="flex items-center gap-2 mb-1">
              <GraduationCap size={20} className="text-[#E8C468]" />
              <h1 className="font-serif text-2xl text-[#F5F3E7]">Minhas Notas</h1>
            </div>
            {data && <p className="text-sm text-[#C9D6C9]">{data.className}</p>}
          </div>

          {years.length > 0 && (
            <select
              value={selectedYear || ''}
              onChange={(e) => setSelectedYear(Number(e.target.value))}
              className="bg-[#173226] border border-[#4A6E5A] rounded-sm py-2 px-3
                         text-sm text-[#F5F3E7] focus:outline-none focus:border-[#E8C468] transition-colors"
            >
              {years.map((y) => (
                <option key={y} value={y}>{y}</option>
              ))}
            </select>
          )}
        </div>

        {loadingYears && <p className="text-[#8FA697] text-sm">Carregando...</p>}
        {error && <p className="text-[#E27D60] text-sm">{error}</p>}

        {!loadingYears && years.length === 0 && (
          <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-8 text-center">
            <p className="text-[#8FA697] text-sm italic">Nenhum ano letivo disponível ainda.</p>
          </div>
        )}

        {selectedYear && !data && !error && (
          <p className="text-[#8FA697] text-sm">Carregando notas...</p>
        )}

        {data && data.subjects.length === 0 && (
          <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-8 text-center">
            <p className="text-[#8FA697] text-sm italic">Nenhuma nota lançada em {selectedYear} ainda.</p>
          </div>
        )}

        {data && data.subjects.length > 0 && (
          <div className="space-y-5">
            {data.subjects.map((subject) => (
              <SubjectCard key={subject.subjectName} subject={subject} />
            ))}
          </div>
        )}
      </main>
    </div>
  );
}

function SubjectCard({ subject }) {
  const gradesByQuarter = {};
  subject.grades.forEach((g) => {
    gradesByQuarter[g.quarter] = g;
  });

  return (
    <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-6">
      <div className="flex items-center gap-2 border-b border-[#4A6E5A] pb-4 mb-4">
        <BookOpen size={17} className="text-[#E8C468]" />
        <div>
          <p className="font-serif text-lg text-[#F5F3E7]">{subject.subjectName}</p>
          <p className="text-xs text-[#8FA697]">Prof. {subject.teacherName}</p>
        </div>
      </div>

      <div className="overflow-x-auto">
        <table className="w-full text-sm">
          <thead>
            <tr className="text-left">
              <th className="font-mono text-[10px] tracking-wide text-[#8FA697] uppercase pb-2 pr-4">Trimestre</th>
              <th className="font-mono text-[10px] tracking-wide text-[#8FA697] uppercase pb-2 pr-4">Prova 1</th>
              <th className="font-mono text-[10px] tracking-wide text-[#8FA697] uppercase pb-2 pr-4">Prova 2</th>
              <th className="font-mono text-[10px] tracking-wide text-[#8FA697] uppercase pb-2 pr-4">Trabalho</th>
              <th className="font-mono text-[10px] tracking-wide text-[#8FA697] uppercase pb-2">Média</th>
            </tr>
          </thead>
          <tbody>
            {quarterOrder.map((q) => {
              const grade = gradesByQuarter[q];
              return (
                <tr key={q} className="border-t border-[#4A6E5A]/50">
                  <td className="py-2.5 pr-4 text-[#F5F3E7]">{quarterLabels[q]}</td>
                  {grade ? (
                    <>
                      <td className="py-2.5 pr-4 text-[#C9D6C9]">{grade.exam1.toFixed(1)}</td>
                      <td className="py-2.5 pr-4 text-[#C9D6C9]">{grade.exam2.toFixed(1)}</td>
                      <td className="py-2.5 pr-4 text-[#C9D6C9]">{grade.assignment.toFixed(1)}</td>
                      <td className="py-2.5 font-medium text-[#E8C468]">{grade.average.toFixed(1)}</td>
                    </>
                  ) : (
                    <td colSpan={4} className="py-2.5 text-[#8FA697] italic text-xs">Ainda não lançada</td>
                  )}
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
}