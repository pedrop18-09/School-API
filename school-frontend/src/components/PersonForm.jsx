export default function PersonForm({
    form,
    handleChange,
    classes = [],
    showPassword = false,
    showRegistration = false,
    showClass = false,
    disabledFields = {},
    onSubmit,
    submitText
}) {

    return (
        <form
            onSubmit={onSubmit}
            className="bg-[#2C5240] rounded-xl shadow-xl p-8 space-y-5"
        >

            <div>
                <label className="block text-[#F5F3E7] mb-2">
                    Nome
                </label>

                <input
                    type="text"
                    name="name"
                    value={form.name}
                    onChange={handleChange}
                    required
                    className="w-full rounded-lg bg-[#1F3D2E] border border-[#C9D6C9] text-[#F5F3E7] p-3"
                />
            </div>

            <div>
                <label className="block text-[#F5F3E7] mb-2">
                    CPF
                </label>

                <input
                    type="text"
                    name="cpf"
                    value={form.cpf}
                    onChange={handleChange}
                    disabled={disabledFields.cpf}
                    required
                    className="w-full rounded-lg bg-[#1F3D2E] border border-[#C9D6C9] text-[#F5F3E7] p-3 disabled:opacity-60"
                />
            </div>

            {showPassword && (

                <div>

                    <label className="block text-[#F5F3E7] mb-2">
                        Senha
                    </label>

                    <input
                        type="password"
                        name="password"
                        value={form.password}
                        onChange={handleChange}
                        required
                        className="w-full rounded-lg bg-[#1F3D2E] border border-[#C9D6C9] text-[#F5F3E7] p-3"
                    />

                </div>

            )}

            {showRegistration && (

                <div>

                    <label className="block text-[#F5F3E7] mb-2">
                        Matrícula
                    </label>

                    <input
                        type="text"
                        name="registration"
                        value={form.registration}
                        onChange={handleChange}
                        disabled={disabledFields.registration}
                        required
                        className="w-full rounded-lg bg-[#1F3D2E] border border-[#C9D6C9] text-[#F5F3E7] p-3 disabled:opacity-60"
                    />

                </div>

            )}

            <div>

                <label className="block text-[#F5F3E7] mb-2">
                    Data de nascimento
                </label>

                <input
                    type="date"
                    name="dateOfBirth"
                    value={form.dateOfBirth}
                    onChange={handleChange}
                    required
                    className="w-full rounded-lg bg-[#1F3D2E] border border-[#C9D6C9] text-[#F5F3E7] p-3"
                />

            </div>

            {showClass && (

                <div>

                    <label className="block text-[#F5F3E7] mb-2">
                        Turma
                    </label>

                    <select
                        name="classId"
                        value={form.classId}
                        onChange={handleChange}
                        required
                        className="w-full rounded-lg bg-[#1F3D2E] border border-[#C9D6C9] text-[#F5F3E7] p-3"
                    >
                        <option value="">
                            Selecione uma turma
                        </option>

                        {classes.map(c => (

                            <option
                                key={c.id}
                                value={c.id}
                            >
                                {c.name}
                            </option>

                        ))}

                    </select>

                </div>

            )}

            <button
                type="submit"
                className="w-full bg-[#E8C468] text-[#1F3D2E] font-bold py-3 rounded-lg hover:brightness-110 transition"
            >
                {submitText}
            </button>

        </form>
    );
}