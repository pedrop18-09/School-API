import { useState } from 'react';
import { Eye, EyeOff } from 'lucide-react';

export default function FormInput({ label, icon: Icon, error, type, ...inputProps }) {
  const [showPassword, setShowPassword] = useState(false);
  const isPassword = type === 'password';
  const actualType = isPassword && showPassword ? 'text' : type;

  return (
    <div className="mb-5">
      <label className="block font-mono text-[11px] tracking-widest text-[#C9D6C9] uppercase mb-2">
        {label}
      </label>
      <div className="relative">
        {Icon && (
          <Icon
            size={17}
            className="absolute left-3 top-1/2 -translate-y-1/2 text-[#8FA697]"
          />
        )}
        <input
          {...inputProps}
          type={actualType}
          className={`w-full bg-[#173226] border rounded-sm py-3 text-[#F5F3E7] placeholder:text-[#5C7A68]
                      focus:outline-none focus:border-[#E8C468] transition-colors
                      ${Icon ? 'pl-10' : 'pl-3'}
                      ${isPassword ? 'pr-10' : 'pr-3'}
                      ${error ? 'border-[#E27D60]' : 'border-[#4A6E5A]'}`}
        />
        {isPassword && (
          <button
            type="button"
            onClick={() => setShowPassword((prev) => !prev)}
            className="absolute right-3 top-1/2 -translate-y-1/2 text-[#8FA697] hover:text-[#E8C468] transition-colors"
            tabIndex={-1}
          >
            {showPassword ? <EyeOff size={17} /> : <Eye size={17} />}
          </button>
        )}
      </div>
      {error && (
        <p className="mt-1.5 text-xs text-[#E27D60]">{error}</p>
      )}
    </div>
  );
}