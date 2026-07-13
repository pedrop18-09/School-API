import { useEffect, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { UserPlus, BookPlus, GraduationCap, Users, Link2 } from 'lucide-react';
import DashboardHeader from '../../components/DashboardHeader';
import ActionButton from '../../components/ActionButton';
import AuditPanel from '../../components/AuditPanel';
import SearchableList from '../../components/SearchableList';
import { getAllStudents, deleteStudent } from '../../services/studentService';
import { getAllTeachers, deleteTeacher } from '../../services/teacherService';

export default function SecretaryDashboard() {
  const navigate = useNavigate();
  const [students, setStudents] = useState([]);
  const [teachers, setTeachers] = useState([]);

  useEffect(() => {
    getAllStudents().then(setStudents).catch(() => setStudents([]));
    getAllTeachers().then(setTeachers).catch(() => setTeachers([]));
  }, []);

  async function handleDeleteStudent(student) {
    try {
      await deleteStudent(student.id);
      setStudents((prev) => prev.filter((s) => s.id !== student.id));
    } catch {
      alert('Não foi possível excluir este aluno.');
    }
  }

  async function handleDeleteTeacher(teacher) {
    try {
      await deleteTeacher(teacher.id);
      setTeachers((prev) => prev.filter((t) => t.id !== teacher.id));
    } catch {
      alert('Não foi possível excluir este professor. Confira se ele ainda não está vinculado a alguma turma.');
    }
  }

  return (
    <div className="min-h-screen bg-[#1F3D2E]">
      <DashboardHeader />

      <main className="max-w-6xl mx-auto px-6 py-8">
        <div className="flex flex-col sm:flex-row gap-3 mb-8">
          <ActionButton
            icon={GraduationCap}
            label="Cadastrar Aluno"
            onClick={() => navigate('/secretary/students/new')}
          />
          <ActionButton
            icon={UserPlus}
            label="Cadastrar Professor"
            onClick={() => navigate('/secretary/teachers/new')}
          />
          <ActionButton
            icon={BookPlus}
            label="Cadastrar Matéria"
            onClick={() => navigate('/secretary/subjects/new')}
          />
          <ActionButton
            icon={Link2}
            label="Vinculos Turma-Disciplina"
            onClick={() => navigate('/secretary/class-subjects')}
          />
        </div>

        <div className="mb-8">
          <AuditPanel />
        </div>

        <div className="grid md:grid-cols-2 gap-6">
          <SearchableList
            title="Alunos"
            icon={GraduationCap}
            items={students}
            onItemClick={(student) =>
                navigate(`/secretary/students/${student.id}/edit`)
            }
            onDelete={handleDeleteStudent}
            renderSubtitle={(student) => `Matrícula ${student.registration} · ${student.className}`}
          />

          <SearchableList
            title="Professores"
            icon={Users}
            items={teachers}
            onItemClick={(teacher) => navigate(`/secretary/teachers/${teacher.id}/edit`)}
            onDelete={handleDeleteTeacher}
            renderSubtitle={(teacher) => `CPF ${teacher.cpf}`}
          />
        </div>
        
        {/* LINKS INFERIORES DE GERENCIAMENTO ALINHADOS */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mt-8 pt-4 border-t border-[#2C5240]">
          <div className="text-center">
            <Link
              to="/secretary/teachers/inactive"
              className="text-xs text-[#8FA697] hover:text-[#E8C468] transition-colors"
            >
              Ver professores desativados →
            </Link>
          </div>

          <div className="text-center">
            <Link
              to="/secretary/students/inactive"
              className="text-xs text-[#8FA697] hover:text-[#E8C468] transition-colors"
            >
              Ver alunos desativados →
            </Link>
          </div>

          <div className="text-center">
            <Link
              to="/secretary/subjects"
              className="text-xs text-[#8FA697] hover:text-[#E8C468] transition-colors"
            >
              Gerenciar matérias →
            </Link>
          </div>
        </div>
      </main>
    </div>
  );
}