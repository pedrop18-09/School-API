import { useNavigate } from 'react-router-dom';
import { Building2, BookOpen, GraduationCap } from 'lucide-react';
import RoleCard from '../components/RoleCard';

const roles = [
  {
    code: 'SEC · 01',
    label: 'Secretaria',
    description: 'Gerencia matrículas, professores e turmas.',
    icon: Building2,
    path: '/login/secretary',
  },
  {
    code: 'PRO · 02',
    label: 'Professor',
    description: 'Lança notas e acompanha o desempenho da turma.',
    icon: BookOpen,
    path: '/login/teacher',
  },
  {
    code: 'ALU · 03',
    label: 'Aluno',
    description: 'Consulta notas em todas as disciplinas.',
    icon: GraduationCap,
    path: '/login/student',
  },
];

export default function Home() {
  const navigate = useNavigate();

  return (
    <div className="min-h-screen bg-[#1F3D2E] relative overflow-hidden flex items-center justify-center px-6 py-16">
      <div
        className="absolute inset-0 opacity-[0.07] pointer-events-none"
        style={{
          backgroundImage:
            'radial-gradient(circle at 20% 30%, #F5F3E7 0.5px, transparent 0.5px), radial-gradient(circle at 70% 65%, #F5F3E7 0.5px, transparent 0.5px)',
          backgroundSize: '3px 3px, 4px 4px',
        }}
      />

      <div className="relative w-full max-w-4xl">
        <div className="text-center mb-14">
          <p className="font-mono text-xs tracking-[0.3em] text-[#E8C468] mb-4 uppercase">
            Sistema Escolar
          </p>
          <h1 className="font-serif text-5xl sm:text-6xl text-[#F5F3E7] mb-4 leading-tight">
            Quem está entrando<br />na sala hoje?
          </h1>
          <svg width="180" height="12" viewBox="0 0 180 12" className="mx-auto text-[#E8C468]" fill="none">
            <path d="M2 8C40 2 90 10 178 4" stroke="currentColor" strokeWidth="2.5" strokeLinecap="round" />
          </svg>
        </div>

        <div className="grid sm:grid-cols-3 gap-5">
          {roles.map((role) => (
            <RoleCard
              key={role.code}
              code={role.code}
              label={role.label}
              description={role.description}
              icon={role.icon}
              onClick={() => navigate(role.path)}
            />
          ))}
        </div>
      </div>
    </div>
  );
}