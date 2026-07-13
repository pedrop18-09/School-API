import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { ArrowLeft, School, Pencil, Check, X } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';
import { getAllClasses, updateClass } from '../../services/classService';

const schoolGrades = [
  { value: 'SextoAno', label: '6º Ano' },
  { value: 'SetimoAno', label: '7º Ano' },
  { value: 'OitavoAno', label: '8º Ano' },
  { value: 'NonoAno', label: '9º Ano' },
  { value: 'PrimeiroAnoEnsinoMedio', label: '1º Ano - Ensino Médio' },
  { value: 'SegundoAnoEnsinoMedio', label: '2º Ano - Ensino Médio' },
  { value: 'TerceiroAnoEnsinoMedio', label: '3º Ano - Ensino Médio' },
];

const gradeLabel = (value) => schoolGrades.find((g) => g.value === value)?.label || value;

export default function ClassesListPage() {
  const [classes, setClasses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [editingId, setEditingId] = useState(null);
  const [editValues, setEditValues] = useState({ name: '', schoolGrade: '', academicYear: '' });
  const [savingId, setSavingId] = useState(null);
  const [error, setError] = useState('');

  useEffect(() => {
    getAllClasses()
      .then(setClasses)
      .catch(() => setClasses([]))
      .finally(() => setLoading(false));
  }, []);

  function startEditing(schoolClass) {
    setEditingId(schoolClass.id);
    setEditValues({
      name: schoolClass.name,
      schoolGrade: schoolClass.schoolGrade,
      academicYear: schoolClass.academicYear,
    });
    setError('');
  }

  function cancelEditing() {
    setEditingId(null);
  }

  async function saveEditing(id) {
    if (!editValues.name.trim() || !editValues.schoolGrade || !editValues.academicYear) return;

    setSavingId(id);
    setError('');

    try {
      await updateClass(id, {
        name: editValues.name.trim(),
        schoolGrade: editValues.schoolGrade,
        academicYear: Number(editValues.academicYear),
      });
      setClasses((prev) =>
        prev.map((c) =>
          c.id === id
            ? { ...c, name: editValues.name.trim(), schoolGrade: editValues.schoolGrade, academicYear: Number(editValues.academicYear) }
            : c
        )
      );
      setEditingId(null);
    } catch (err) {
      setError(err.response?.data || 'Não foi possível salvar esta turma.');
    } finally {
      setSavingId(null);
    }
  }

  return (
    <div className="min-h-screen bg-[#1F3D2E]">
      <DashboardHeader />

      <main className="max-w-2xl mx-auto px-6 py-8">
        <Link
          to="/secretary/class-subjects"
          className="inline-flex items-center gap-2 text-sm text-[#C9D6C9] hover:text-[#E8C468] transition-colors mb-6"
        >
          <ArrowLeft size={15} />
          Voltar
        </Link>

        <div className="flex items-center justify-between mb-6">
          <div className="flex items-center gap-2">
            <School size={20} className="text-[#E8C468]" />
            <h1 className="font-serif text-2xl text-[#F5F3E7]">Turmas</h1>
          </div>
          <Link
            to="/secretary/classes/new"
            className="text-sm text-[#E8C468] hover:text-[#F0D385] transition-colors font-medium"
          >
            + Nova Turma
          </Link>
        </div>

        {error && <p className="text-sm text-[#E27D60] mb-4">{error}</p>}

        {loading ? (
          <p className="text-[#8FA697] text-sm">Carregando...</p>
        ) : classes.length === 0 ? (
          <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-8 text-center">
            <p className="text-[#8FA697] text-sm italic">Nenhuma turma cadastrada ainda.</p>
          </div>
        ) : (
          <ul className="space-y-2">
            {classes.map((schoolClass) => (
              <li
                key={schoolClass.id}
                className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-4"
              >
                {editingId === schoolClass.id ? (
                  <div className="space-y-3">
                    <div className="grid sm:grid-cols-3 gap-2">
                      <input
                        type="text"
                        value={editValues.name}
                        onChange={(e) => setEditValues((v) => ({ ...v, name: e.target.value }))}
                        placeholder="Nome"
                        className="bg-[#173226] border border-[#E8C468] rounded-sm py-1.5 px-2
                                   text-sm text-[#F5F3E7] focus:outline-none"
                      />
                      <select
                        value={editValues.schoolGrade}
                        onChange={(e) => setEditValues((v) => ({ ...v, schoolGrade: e.target.value }))}
                        className="bg-[#173226] border border-[#E8C468] rounded-sm py-1.5 px-2
                                   text-sm text-[#F5F3E7] focus:outline-none"
                      >
                        {schoolGrades.map((g) => (
                          <option key={g.value} value={g.value}>{g.label}</option>
                        ))}
                      </select>
                      <input
                        type="number"
                        value={editValues.academicYear}
                        onChange={(e) => setEditValues((v) => ({ ...v, academicYear: e.target.value }))}
                        placeholder="Ano"
                        className="bg-[#173226] border border-[#E8C468] rounded-sm py-1.5 px-2
                                   text-sm text-[#F5F3E7] focus:outline-none"
                      />
                    </div>
                    <div className="flex items-center gap-2">
                      <button
                        onClick={() => saveEditing(schoolClass.id)}
                        disabled={savingId === schoolClass.id}
                        className="flex items-center gap-1 text-[#7FB88F] hover:text-[#9FD4AC] transition-colors text-sm disabled:opacity-50"
                      >
                        <Check size={15} /> Salvar
                      </button>
                      <button
                        onClick={cancelEditing}
                        className="flex items-center gap-1 text-[#8FA697] hover:text-[#E27D60] transition-colors text-sm"
                      >
                        <X size={15} /> Cancelar
                      </button>
                    </div>
                  </div>
                ) : (
                  <div className="flex items-center justify-between">
                    <div>
                      <p className="text-sm text-[#F5F3E7]">{schoolClass.name}</p>
                      <p className="text-xs text-[#8FA697] mt-0.5">
                        {gradeLabel(schoolClass.schoolGrade)} · {schoolClass.academicYear}
                      </p>
                    </div>
                    <button
                      onClick={() => startEditing(schoolClass)}
                      title="Editar"
                      className="text-[#8FA697] hover:text-[#E8C468] transition-colors p-1.5"
                    >
                      <Pencil size={15} />
                    </button>
                  </div>
                )}
              </li>
            ))}
          </ul>
        )}
      </main>
    </div>
  );
}