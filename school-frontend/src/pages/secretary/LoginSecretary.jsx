import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { Mail, Lock, ArrowLeft, Building2 } from 'lucide-react';
import FormInput from '../../components/FormInput';
import { loginSecretary } from '../../services/authService';

export default function LoginSecretary() {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  async function handleSubmit(e) {
    e.preventDefault();
    setError('');
    setLoading(true);

    try {
      await loginSecretary(email, password);
      navigate('/secretary/dashboard');
    } catch{
      setError('E-mail ou senha inválidos.');
    } finally {
      setLoading(false);
    }
  }

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

      <div className="relative w-full max-w-sm">
        <Link
          to="/"
          className="inline-flex items-center gap-2 text-sm text-[#C9D6C9] hover:text-[#E8C468] transition-colors mb-8"
        >
          <ArrowLeft size={15} />
          Voltar
        </Link>

        <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-8" style={{ borderStyle: 'dashed' }}>
          <div className="flex items-center gap-3 mb-1">
            <Building2 size={20} className="text-[#E8C468]" />
            <span className="font-mono text-[11px] tracking-widest text-[#C9D6C9] uppercase">
              SEC · 01
            </span>
          </div>
          <h1 className="font-serif text-3xl text-[#F5F3E7] mb-8">
            Secretaria
          </h1>

          <form onSubmit={handleSubmit}>
            <FormInput
              label="E-mail"
              icon={Mail}
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="secretario@escola.com"
              required
            />

            <FormInput
              label="Senha"
              icon={Lock}
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="••••••••"
              required
            />

            {error && (
              <p className="text-sm text-[#E27D60] mb-4 -mt-1">{error}</p>
            )}

            <button
              type="submit"
              disabled={loading}
              className="w-full bg-[#E8C468] text-[#1F3D2E] font-medium py-3 rounded-sm mt-2
                         hover:bg-[#F0D385] transition-colors disabled:opacity-60 disabled:cursor-not-allowed"
            >
              {loading ? 'Entrando...' : 'Entrar'}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}