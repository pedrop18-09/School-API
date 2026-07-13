import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { ClipboardList, ArrowRight } from 'lucide-react';
import { getAllAuditLogs } from '../services/auditService';

const actionLabels = {
  Created: 'criou',
  Updated: 'atualizou',
  Deleted: 'removeu',
};

export default function AuditPanel() {
  const [logs, setLogs] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getAllAuditLogs()
      .then((data) => setLogs(data.slice(0, 5)))
      .catch(() => setLogs([]))
      .finally(() => setLoading(false));
  }, []);

  return (
    <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-6">
      <div className="flex items-center justify-between border-b border-[#4A6E5A] pb-4 mb-4">
        <div className="flex items-center gap-2">
          <ClipboardList size={18} className="text-[#E8C468]" />
          <h2 className="font-serif text-xl text-[#F5F3E7]">Painel de Mudanças</h2>
        </div>
        <Link
          to="/secretary/audit"
          className="flex items-center gap-1 text-xs text-[#E8C468] hover:text-[#F0D385] transition-colors font-mono uppercase tracking-wide"
        >
          Ver tudo
          <ArrowRight size={13} />
        </Link>
      </div>

      {loading ? (
        <p className="text-center text-[#8FA697] text-sm py-6">Carregando...</p>
      ) : logs.length === 0 ? (
        <p className="text-center text-[#8FA697] text-sm italic py-6">
          Nenhuma ação foi feita ainda!
        </p>
      ) : (
        <ul className="space-y-3">
          {logs.map((log) => (
            <li key={log.id} className="text-sm">
              <p className="text-[#F5F3E7]">
                <span className="text-[#E8C468] font-medium">{log.performedBySecretaryName}</span>{' '}
                {actionLabels[log.action] || log.action.toLowerCase()}{' '}
                <span className="text-[#C9D6C9]">{log.entityName.toLowerCase()}</span>
              </p>
              <p className="text-xs text-[#8FA697] mt-0.5">
                {log.details} · {new Date(log.timestamp).toLocaleString('pt-BR')}
              </p>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}