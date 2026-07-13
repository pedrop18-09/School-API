import { useEffect, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { ArrowLeft, Link2 } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';
import { getAllClasses } from '../../services/classService';
import { getAllSubjects } from '../../services/subjectService';
import { getAllTeachers } from '../../services/teacherService';
import { createClassSubject } from '../../services/classSubjectService';

export default function ClassSubjectForm() {
  const navigate = useNavigate();

  const [classes, setClasses] = useState([]);
  const [subjects, setSubjects] = useState([]);
  const [teachers, setTeachers] = useState([]);

  const [classId, setClassId] = useState('');
  const [subjectId, setSubjectId] = useState('');
  const [teacherId, setTeacherId] = useState('');

  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [loadingOptions, setLoadingOptions] = useState(true);

  useEffect(() => {
    Promise.all([getAllClasses(), getAllSubjects(), getAllTeachers()])
      .then(([classesData, subjectsData, teachersData]) => {
        setClasses(classesData);
        setSubjects(subjectsData);
        setTeachers(teachersData);
      })
      .catch(() => setError('Não foi possível carregar as opções.'))
      .finally(() => setLoadingOptions(false));
  }, []);

  async function handleSubmit(e) {
    e.preventDefault();
    setError('');
    setLoading(true);

    try {
      await createClassSubject({ classId, subjectId, teacherId });
      navigate('/secretary/dashboard');
    } catch (err) {
      setError(
        err.response?.data ||
        'Não foi possível criar o vínculo. Confira se já não existe um professor para essa disciplina nessa turma.'
      );
    } finally {
      setLoading(false);
    }
  }

  return (
    <div className="min-h-screen bg-[#1F3D2E]">
      <DashboardHeader />

      <main className="max-w-md mx-auto px-6 py-8">
        <Link
          to="/secretary/dashboard"
          className="inline-flex items-center gap-2 text-sm text-[#C9D6C9] hover:text-[#E8C468] transition-colors mb-6"
        >
          <ArrowLeft size={15} />
          Voltar
        </Link>

        <div className="flex items-center gap-2 mb-8">
          <Link2 size={20} className="text-[#E8C468]" />
          <h1 className="font-serif text-2xl text-[#F5F3E7]">Vincular Professor</h1>
        </div>

        {loadingOptions ? (
          <p className="text-[#8FA697] text-sm">Carregando...</p>
        ) : (
          <form onSubmit={handleSubmit} className="space-y-5">
            <SelectField
              label="Turma"
              value={classId}
              onChange={setClassId}
              options={classes.map((c) => ({ value: c.id, label: `${c.name} — ${c.schoolGrade}` }))}
            />

            <SelectField
              label="Disciplina"
              value={subjectId}
              onChange={setSubjectId}
              options={subjects.map((s) => ({ value: s.id, label: s.name }))}
            />

            <SelectField
              label="Professor"
              value={teacherId}
              onChange={setTeacherId}
              options={teachers.map((t) => ({ value: t.id, label: t.name }))}
            />

            {error && (
              <p className="text-sm text-[#E27D60]">{error}</p>
            )}

            <button
              type="submit"
              disabled={loading || !classId || !subjectId || !teacherId}
              className="w-full bg-[#E8C468] text-[#1F3D2E] font-medium py-3 rounded-sm
                         hover:bg-[#F0D385] transition-colors disabled:opacity-60 disabled:cursor-not-allowed"
            >
              {loading ? 'Vinculando...' : 'Vincular'}
            </button>
          </form>
        )}
      </main>
    </div>
  );
}

function SelectField({ label, value, onChange, options }) {
  return (
    <div>
      <label className="block font-mono text-[11px] tracking-widest text-[#C9D6C9] uppercase mb-2">
        {label}
      </label>
      <select
        value={value}
        onChange={(e) => onChange(e.target.value)}
        required
        className="w-full bg-[#173226] border border-[#4A6E5A] rounded-sm py-3 px-3
                   text-[#F5F3E7] focus:outline-none focus:border-[#E8C468] transition-colors"
      >
        <option value="">Selecione...</option>
        {options.map((opt) => (
          <option key={opt.value} value={opt.value}>{opt.label}</option>
        ))}
      </select>
    </div>
  );
}