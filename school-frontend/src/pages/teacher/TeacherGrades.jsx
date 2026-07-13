import { useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { ArrowLeft, Check, Loader2 } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';
import { getEntrySheet, createGrade, updateGrade } from '../../services/gradeService';

const quarters = [
  { value: 'FirstQuarter', label: '1º Trimestre' },
  { value: 'SecondQuarter', label: '2º Trimestre' },
  { value: 'ThirdQuarter', label: '3º Trimestre' },
];

export default function TeacherGrades() {
  const { classSubjectId } = useParams();
  const [quarter, setQuarter] = useState('');
  const [sheet, setSheet] = useState(null);
  const [entries, setEntries] = useState({});
  const [savingId, setSavingId] = useState(null);
  const [savedId, setSavedId] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  function handleQuarterChange(value) {
    setQuarter(value);
    setSheet(null);
    setError('');

    if (!value) return;

    setLoading(true);
    getEntrySheet(classSubjectId, value)
      .then((data) => {
        setSheet(data);
        const initial = {};
        data.students.forEach((s) => {
          initial[s.studentId] = {
            gradeId: s.gradeId,
            exam1: s.exam1 ?? '',
            exam2: s.exam2 ?? '',
            assignment: s.assignment ?? '',
          };
        });
        setEntries(initial);
      })
      .catch(() => setError('Não foi possível carregar os alunos desta turma.'))
      .finally(() => setLoading(false));
  }

  function handleFieldChange(studentId, field, value) {
    setEntries((prev) => ({
      ...prev,
      [studentId]: { ...prev[studentId], [field]: value },
    }));
  }

  async function handleSave(studentId) {
    const entry = entries[studentId];
    setSavingId(studentId);
    setSavedId(null);

    const payload = {
      exam1: Number(entry.exam1),
      exam2: Number(entry.exam2),
      assignment: Number(entry.assignment),
    };

    try {
      if (entry.gradeId) {
        await updateGrade(entry.gradeId, payload);
      } else {
        const created = await createGrade({
          studentId,
          classSubjectId,
          quarter,
          ...payload,
        });
        setEntries((prev) => ({
          ...prev,
          [studentId]: { ...prev[studentId], gradeId: created.id },
        }));
      }
      setSavedId(studentId);
    } catch {
      setError('Não foi possível salvar esta nota. Confira os valores.');
    } finally {
      setSavingId(null);
    }
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

        <h1 className="font-serif text-2xl text-[#F5F3E7] mb-6">Lançar Notas</h1>

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
        {error && <p className="text-[#E27D60] text-sm mb-4">{error}</p>}

        {sheet && (
          <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-6">
            <div className="border-b border-[#4A6E5A] pb-4 mb-4">
              <p className="font-serif text-xl text-[#F5F3E7]">{sheet.subjectName}</p>
              <p className="text-sm text-[#C9D6C9]">{sheet.className}</p>
            </div>

            {sheet.students.length === 0 ? (
              <p className="text-center text-[#8FA697] text-sm italic py-6">
                Nenhum aluno matriculado nesta turma.
              </p>
            ) : (
              <div className="space-y-4">
                {sheet.students.map((s) => {
                  const entry = entries[s.studentId] || {};
                  return (
                    <div
                      key={s.studentId}
                      className="border border-[#4A6E5A] rounded-sm p-4 flex flex-col sm:flex-row sm:items-end gap-3"
                    >
                      <div className="flex-1 min-w-0">
                        <p className="text-sm text-[#F5F3E7] truncate">{s.studentName}</p>
                      </div>

                      <div className="flex gap-2">
                        <NumberField
                          label="Prova 1"
                          value={entry.exam1}
                          onChange={(v) => handleFieldChange(s.studentId, 'exam1', v)}
                        />
                        <NumberField
                          label="Prova 2"
                          value={entry.exam2}
                          onChange={(v) => handleFieldChange(s.studentId, 'exam2', v)}
                        />
                        <NumberField
                          label="Trabalho"
                          value={entry.assignment}
                          onChange={(v) => handleFieldChange(s.studentId, 'assignment', v)}
                        />
                      </div>

                      <button
                        onClick={() => handleSave(s.studentId)}
                        disabled={savingId === s.studentId}
                        className="flex items-center justify-center gap-1.5 bg-[#E8C468] text-[#1F3D2E]
                                   text-sm font-medium px-4 py-2.5 rounded-sm hover:bg-[#F0D385]
                                   transition-colors disabled:opacity-60 shrink-0"
                      >
                        {savingId === s.studentId ? (
                          <Loader2 size={15} className="animate-spin" />
                        ) : savedId === s.studentId ? (
                          <Check size={15} />
                        ) : (
                          'Salvar'
                        )}
                      </button>
                    </div>
                  );
                })}
              </div>
            )}
          </div>
        )}
      </main>
    </div>
  );
}

function NumberField({ label, value, onChange }) {
  return (
    <div className="w-20">
      <label className="block font-mono text-[10px] tracking-wide text-[#8FA697] uppercase mb-1">
        {label}
      </label>
      <input
        type="number"
        min="0"
        max="10"
        step="0.1"
        value={value}
        onChange={(e) => onChange(e.target.value)}
        className="w-full bg-[#173226] border border-[#4A6E5A] rounded-sm py-2 px-2 text-sm
                   text-[#F5F3E7] focus:outline-none focus:border-[#E8C468] transition-colors"
      />
    </div>
  );
}