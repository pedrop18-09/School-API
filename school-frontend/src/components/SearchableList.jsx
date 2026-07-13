import { useState, useMemo } from 'react';
import { Search, Trash2 } from 'lucide-react';

export default function SearchableList({ title, icon: Icon, items, onItemClick, onDelete, renderSubtitle }) {
  const [query, setQuery] = useState('');

  const filteredItems = useMemo(() => {
    if (!query.trim()) return items;
    return items.filter((item) =>
      item.name.toLowerCase().includes(query.trim().toLowerCase())
    );
  }, [items, query]);

  function handleDeleteClick(e, item) {
    e.stopPropagation(); // evita disparar o onItemClick (que abriria a edição)

    const confirmed = window.confirm(
      `Tem certeza que deseja excluir "${item.name}"? Essa ação não pode ser desfeita.`
    );

    if (confirmed) {
      onDelete(item);
    }
  }

  return (
    <div className="bg-[#2C5240] border border-[#4A6E5A] rounded-sm p-6">
      <div className="flex items-center gap-2 border-b border-[#4A6E5A] pb-4 mb-4">
        <Icon size={18} className="text-[#E8C468]" />
        <h2 className="font-serif text-xl text-[#F5F3E7]">{title}</h2>
        <span className="font-mono text-xs text-[#8FA697] ml-auto">
          {items.length} {items.length === 1 ? 'registro' : 'registros'}
        </span>
      </div>

      <div className="relative mb-4">
        <Search size={16} className="absolute left-3 top-1/2 -translate-y-1/2 text-[#8FA697]" />
        <input
          type="text"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder="Pesquisar por nome..."
          className="w-full bg-[#173226] border border-[#4A6E5A] rounded-sm py-2.5 pl-10 pr-3
                     text-sm text-[#F5F3E7] placeholder:text-[#5C7A68]
                     focus:outline-none focus:border-[#E8C468] transition-colors"
        />
      </div>

      {filteredItems.length === 0 ? (
        <p className="text-center text-[#8FA697] text-sm italic py-6">
          {query ? 'Nenhum resultado encontrado.' : 'Nenhum registro cadastrado ainda.'}
        </p>
      ) : (
        <ul className="divide-y divide-[#4A6E5A] max-h-80 overflow-y-auto">
          {filteredItems.map((item) => (
            <li key={item.id}>
              <div
                onClick={() => onItemClick(item)}
                className="w-full flex items-center justify-between group hover:bg-[#173226]/40 px-2 -mx-2 py-3 rounded-sm transition-colors cursor-pointer"
              >
                <div className="min-w-0">
                  <p className="text-sm text-[#F5F3E7] truncate">{item.name}</p>
                  {renderSubtitle && (
                    <p className="text-xs text-[#8FA697] mt-0.5 truncate">{renderSubtitle(item)}</p>
                  )}
                </div>

                <div className="flex items-center gap-3 shrink-0 ml-3">
                  <span className="text-xs text-[#E8C468] opacity-0 group-hover:opacity-100 transition-opacity">
                    Editar
                  </span>
                  {onDelete && (
                    <button
                      onClick={(e) => handleDeleteClick(e, item)}
                      title="Excluir"
                      className="text-[#8FA697] hover:text-[#E27D60] transition-colors p-1"
                    >
                      <Trash2 size={15} />
                    </button>
                  )}
                </div>
              </div>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}