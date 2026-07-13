import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { ArrowLeft, BookOpen, Pencil, Trash2, Check, X } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';
import { getAllSubjects, updateSubject, deleteSubject } from '../../services/subjectService';

export default function SubjectsListPage() {
  const [subjects, setSubjects] = useState([]);
  const [loading, setLoading] = useState(true);
  const [editingId, setEditingId] = useState(null);
  const [editValue, setEditValue] = useState('');
  const [savingId, setSavingId] = useState(null);
  const [error, setError] = useState('');

  useEffect(() => {
    getAllSubjects()
      .then(setSubjects)
      .catch(() => setSubjects([]))
      .finally(() => setLoading(false));
  }, []);

  function startEditing(subject) {
    setEditingId(subject.id);
    setEditValue(subject.name);
    setError('');
  }

  function cancelEditing() {
    setEditingId(null);
    setEditValue('');
  }

  async function saveEditing(id) {
    if (!editValue.trim()) return;

    setSavingId(id);
    setError('');

    try {
      await updateSubject(id, { name: editValue.trim() });
      setSubjects((prev) =>
        prev.map((s) => (s.id === id ? { ...s, name: editValue.trim() } : s))
      );
      setEditingId(null);
    } catch (err) {
      setError(err.response?.data || 'Não foi possível salvar esta disciplina.');
    } finally {
      setSavingId(null);
    }
  }

  async function handleDelete(subject) {
    const confirmed = window.confirm(
      `Tem certeza que deseja excluir "${subject.name}"? Essa ação não pode ser desfeita.`
    );
    if (!confirmed) return;

    try {
      await deleteSubject(subject.id);
      setSubjects((prev) => prev.filter((s) => s.id !== subject.id));
    } catch (err) {
      alert(err.response?.data || 'Não foi possível excluir esta disciplina. Confira se ela não está vinculada a alguma turma.');
    }
  }

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

        <div className="flex items-center gap-2 mb-6">
          <BookOpen size={20} className="text-[#E8C468]" />
          <h1 className="font-serif text-2xl text-[#F5F3E7]">Matérias</h1>
        </div>

        {error && <p className="text-sm text-[#E27D60] mb-4">{error}</p>}

        {loading ? (
          <p className="text-[#8FA697] text-sm">Carregando...</p>
        ) : subjects.length === 0 ? (
          <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-8 text-center">
            <p className="text-[#8FA697] text-sm italic">Nenhuma matéria cadastrada ainda.</p>
          </div>
        ) : (
          <ul className="space-y-2">
            {subjects.map((subject) => (
              <li
                key={subject.id}
                className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-4 flex items-center justify-between gap-3"
              >
                {editingId === subject.id ? (
                  <>
                    <input
                      type="text"
                      value={editValue}
                      onChange={(e) => setEditValue(e.target.value)}
                      onKeyDown={(e) => {
                        if (e.key === 'Enter') saveEditing(subject.id);
                        if (e.key === 'Escape') cancelEditing();
                      }}
                      autoFocus
                      className="flex-1 bg-[#173226] border border-[#E8C468] rounded-sm py-1.5 px-2
                                 text-sm text-[#F5F3E7] focus:outline-none"
                    />
                    <button
                      onClick={() => saveEditing(subject.id)}
                      disabled={savingId === subject.id}
                      title="Salvar"
                      className="text-[#7FB88F] hover:text-[#9FD4AC] transition-colors p-1 disabled:opacity-50"
                    >
                      <Check size={17} />
                    </button>
                    <button
                      onClick={cancelEditing}
                      title="Cancelar"
                      className="text-[#8FA697] hover:text-[#E27D60] transition-colors p-1"
                    >
                      <X size={17} />
                    </button>
                  </>
                ) : (
                  <>
                    <span className="text-sm text-[#F5F3E7]">{subject.name}</span>
                    <div className="flex items-center gap-1">
                      <button
                        onClick={() => startEditing(subject)}
                        title="Editar"
                        className="text-[#8FA697] hover:text-[#E8C468] transition-colors p-1.5"
                      >
                        <Pencil size={15} />
                      </button>
                      <button
                        onClick={() => handleDelete(subject)}
                        title="Excluir"
                        className="text-[#8FA697] hover:text-[#E27D60] transition-colors p-1.5"
                      >
                        <Trash2 size={15} />
                      </button>
                    </div>
                  </>
                )}
              </li>
            ))}
          </ul>
        )}
      </main>
    </div>
  );
}