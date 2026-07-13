import { useEffect, useState } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import { ArrowLeft, Link2 } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';
import { getClassSubjectById, updateClassSubject } from '../../services/classSubjectService';
import { getAllTeachers } from '../../services/teacherService';

export default function ClassSubjectEditPage() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [classSubject, setClassSubject] = useState(null);
  const [teachers, setTeachers] = useState([]);
  const [teacherId, setTeacherId] = useState('');

  const [error, setError] = useState('');
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    Promise.all([getClassSubjectById(id), getAllTeachers()])
      .then(([csData, teachersData]) => {
        setClassSubject(csData);
        setTeachers(teachersData);
        setTeacherId(csData.teacherId || '');
      })
      .catch(() => setError('Não foi possível carregar este vínculo.'))
      .finally(() => setLoading(false));
  }, [id]);

  async function handleSubmit(e) {
    e.preventDefault();
    setError('');
    setSaving(true);

    try {
      await updateClassSubject(id, { teacherId });
      navigate('/secretary/class-subjects');
    } catch (err) {
      setError(err.response?.data || 'Não foi possível atualizar o professor deste vínculo.');
    } finally {
      setSaving(false);
    }
  }

  return (
    <div className="min-h-screen bg-[#1F3D2E]">
      <DashboardHeader />

      <main className="max-w-md mx-auto px-6 py-8">
        <Link
          to="/secretary/class-subjects"
          className="inline-flex items-center gap-2 text-sm text-[#C9D6C9] hover:text-[#E8C468] transition-colors mb-6"
        >
          <ArrowLeft size={15} />
          Voltar
        </Link>

        <div className="flex items-center gap-2 mb-8">
          <Link2 size={20} className="text-[#E8C468]" />
          <h1 className="font-serif text-2xl text-[#F5F3E7]">Trocar Professor</h1>
        </div>

        {loading ? (
          <p className="text-[#8FA697] text-sm">Carregando...</p>
        ) : classSubject ? (
          <form onSubmit={handleSubmit} className="space-y-5">
            <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-4">
              <p className="text-sm text-[#F5F3E7]">
                {classSubject.className} · {classSubject.subjectName}
              </p>
              <p className="text-xs text-[#8FA697] mt-1">
                Professor atual: {classSubject.teacherName}
              </p>
            </div>

            <div>
              <label className="block font-mono text-[11px] tracking-widest text-[#C9D6C9] uppercase mb-2">
                Novo Professor
              </label>
              <select
                value={teacherId}
                onChange={(e) => setTeacherId(e.target.value)}
                required
                className="w-full bg-[#173226] border border-[#4A6E5A] rounded-sm py-3 px-3
                           text-[#F5F3E7] focus:outline-none focus:border-[#E8C468] transition-colors"
              >
                <option value="">Selecione...</option>
                {teachers.map((t) => (
                  <option key={t.id} value={t.id}>{t.name}</option>
                ))}
              </select>
            </div>

            {error && <p className="text-sm text-[#E27D60]">{error}</p>}

            <button
              type="submit"
              disabled={saving || !teacherId}
              className="w-full bg-[#E8C468] text-[#1F3D2E] font-medium py-3 rounded-sm
                         hover:bg-[#F0D385] transition-colors disabled:opacity-60 disabled:cursor-not-allowed"
            >
              {saving ? 'Salvando...' : 'Salvar'}
            </button>
          </form>
        ) : (
          <p className="text-[#E27D60] text-sm">{error}</p>
        )}
      </main>
    </div>
  );
}