import { ArrowRight } from 'lucide-react';

export default function RoleCard({ code, label, description, icon: Icon, onClick }) {
  return (
    <button
      onClick={onClick}
      className="group relative bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-6 text-left
                 transition-all duration-200 hover:border-[#E8C468] hover:-translate-y-1
                 focus:outline-none focus-visible:ring-2 focus-visible:ring-[#E8C468] focus-visible:ring-offset-2 focus-visible:ring-offset-[#1F3D2E]"
      style={{ borderStyle: 'dashed' }}
    >
      <div className="flex items-center justify-between mb-8">
        <span className="font-mono text-[11px] tracking-widest text-[#C9D6C9]">
          {code}
        </span>
        <Icon
          size={22}
          className="text-[#E8C468] opacity-80 group-hover:opacity-100 transition-opacity"
        />
      </div>

      <h2 className="font-serif text-2xl text-[#F5F3E7] mb-2">
        {label}
      </h2>
      <p className="text-sm text-[#C9D6C9] leading-relaxed mb-6">
        {description}
      </p>

      <div className="flex items-center gap-2 text-[#E8C468] text-sm font-medium">
        Entrar
        <ArrowRight
          size={15}
          className="transition-transform group-hover:translate-x-1"
        />
      </div>
    </button>
  );
}