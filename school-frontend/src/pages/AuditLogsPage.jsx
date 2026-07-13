import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { ArrowLeft, ClipboardList } from "lucide-react";
import { getAllAuditLogs } from "../services/auditService.js"; // Ajuste o nome da função conforme seu service

export default function AuditLogsPage() {
    const navigate = useNavigate();
    const [logs, setLogs] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        // Substitua pelo método real do seu auditService.js
        getAllAuditLogs()
            .then((data) => {
                setLogs(data);
                setLoading(false);
            })
            .catch((err) => {
                console.error(err);
                alert("Erro ao carregar os logs de auditoria.");
                setLoading(false);
            });
    }, []);

    return (
        <div className="min-h-screen bg-[#1F3D2E] p-6 sm:p-8">
            <div className="max-w-5xl mx-auto">
                
                {/* Botão de Voltar */}
                <button
                    onClick={() => navigate("/secretary/dashboard")}
                    className="flex items-center gap-2 text-[#E8C468] hover:text-[#F5F3E7] transition mb-6 font-medium"
                >
                    <ArrowLeft size={20} />
                    Voltar para a Dashboard
                </button>

                {/* Cabeçalho */}
                <div className="flex items-center gap-3 mb-8">
                    <ClipboardList className="text-[#E8C468]" size={32} />
                    <h1 className="text-3xl font-bold text-[#E8C468]">
                        Histórico de Auditoria Completo
                    </h1>
                </div>

                {/* Tabela de Logs */}
                <div className="bg-[#2C5240] rounded-xl shadow-xl overflow-hidden">
                    {loading ? (
                        <p className="text-[#F5F3E7] text-center p-8">Carregando histórico...</p>
                    ) : logs.length === 0 ? (
                        <p className="text-[#F5F3E7] text-center p-8">Nenhum log registrado até o momento.</p>
                    ) : (
                        <div className="overflow-x-auto">
                            <table className="w-full text-left border-collapse">
                                <thead>
                                    <tr className="bg-[#1F3D2E] text-[#E8C468] border-b border-[#2C5240]">
                                        <th className="p-4 font-semibold">Data / Hora</th>
                                        <th className="p-4 font-semibold">Usuário</th>
                                        <th className="p-4 font-semibold">Ação</th>
                                        <th className="p-4 font-semibold">Descrição</th>
                                    </tr>
                                </thead>
                                <tbody className="text-[#F5F3E7] divide-y divide-[#1F3D2E]">
                                    {logs.map((log) => (
                                        <tr key={log.id} className="hover:bg-[#345E4A] transition">
                                            <td className="p-4 whitespace-nowrap text-sm opacity-80">
                                                {new Date(log.timestamp || log.createdAt).toLocaleString("pt-BR")}
                                            </td>
                                            <td className="p-4 font-medium">
                                                {log.userEmail || log.username || "Sistema"}
                                            </td>
                                            <td className="p-4">
                                                <span className="px-2 py-1 rounded text-xs font-bold bg-[#1F3D2E] text-[#E8C468]">
                                                    {log.action}
                                                </span>
                                            </td>
                                            <td className="p-4 text-sm opacity-90">
                                                {log.description || log.details}
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    )}
                </div>

            </div>
        </div>
    );
}