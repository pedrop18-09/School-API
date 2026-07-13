import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { ArrowLeft, School } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';
import { createClass } from '../../services/classService';

const schoolGrades = [
  { value: 'SextoAno', label: '6º Ano' },
  { value: 'SetimoAno', label: '7º Ano' },
  { value: 'OitavoAno', label: '8º Ano' },
  { value: 'NonoAno', label: '9º Ano' },
  { value: 'PrimeiroAnoEnsinoMedio', label: '1º Ano - Ensino Médio' },
  { value: 'SegundoAnoEnsinoMedio', label: '2º Ano - Ensino Médio' },
  { value: 'TerceiroAnoEnsinoMedio', label: '3º Ano - Ensino Médio' },
];

export default function CreateClassPage() {
  const navigate = useNavigate();

  const [name, setName] = useState('');
  const [schoolGrade, setSchoolGrade] = useState('');
  const [academicYear, setAcademicYear] = useState(new Date().getFullYear());

  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  async function handleSubmit(e) {
    e.preventDefault();
    setError('');
    setLoading(true);

    try {
      await createClass({
        name,
        schoolGrade,
        academicYear: Number(academicYear),
      });
      navigate('/secretary/classes');
    } catch (err) {
      setError(err.response?.data || 'Não foi possível criar a turma.');
    } finally {
      setLoading(false);
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
          <School size={20} className="text-[#E8C468]" />
          <h1 className="font-serif text-2xl text-[#F5F3E7]">Nova Turma</h1>
        </div>

        <form onSubmit={handleSubmit} className="space-y-5">
          <div>
            <label className="block font-mono text-[11px] tracking-widest text-[#C9D6C9] uppercase mb-2">
              Nome
            </label>
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="Turma A"
              required
              className="w-full bg-[#173226] border border-[#4A6E5A] rounded-sm py-3 px-3
                         text-[#F5F3E7] placeholder:text-[#5C7A68] focus:outline-none focus:border-[#E8C468] transition-colors"
            />
          </div>

          <div>
            <label className="block font-mono text-[11px] tracking-widest text-[#C9D6C9] uppercase mb-2">
              Série
            </label>
            <select
              value={schoolGrade}
              onChange={(e) => setSchoolGrade(e.target.value)}
              required
              className="w-full bg-[#173226] border border-[#4A6E5A] rounded-sm py-3 px-3
                         text-[#F5F3E7] focus:outline-none focus:border-[#E8C468] transition-colors"
            >
              <option value="">Selecione...</option>
              {schoolGrades.map((g) => (
                <option key={g.value} value={g.value}>{g.label}</option>
              ))}
            </select>
          </div>

          <div>
            <label className="block font-mono text-[11px] tracking-widest text-[#C9D6C9] uppercase mb-2">
              Ano Letivo
            </label>
            <input
              type="number"
              value={academicYear}
              onChange={(e) => setAcademicYear(e.target.value)}
              min="2000"
              max="2100"
              required
              className="w-full bg-[#173226] border border-[#4A6E5A] rounded-sm py-3 px-3
                         text-[#F5F3E7] focus:outline-none focus:border-[#E8C468] transition-colors"
            />
          </div>

          {error && <p className="text-sm text-[#E27D60]">{error}</p>}

          <button
            type="submit"
            disabled={loading}
            className="w-full bg-[#E8C468] text-[#1F3D2E] font-medium py-3 rounded-sm
                       hover:bg-[#F0D385] transition-colors disabled:opacity-60 disabled:cursor-not-allowed"
          >
            {loading ? 'Criando...' : 'Criar Turma'}
          </button>
        </form>
      </main>
    </div>
  );
}