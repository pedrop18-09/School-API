export default function ActionButton({ icon: Icon, label, onClick }) {
  return (
    <button
      onClick={onClick}
      className="flex-1 flex items-center justify-center gap-2 bg-[#E8C468] text-[#1F3D2E]
                 font-medium text-sm py-3 rounded-sm hover:bg-[#F0D385] transition-colors"
    >
      <Icon size={17} />
      {label}
    </button>
  );
}