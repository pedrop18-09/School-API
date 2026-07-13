import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { createSubject } from "../services/subjectService.js";

export default function CreateSubjectPage() {
    const navigate = useNavigate();

    const [form, setForm] = useState({
        name: ""
    });

    function handleChange(e) {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    }

    async function handleSubmit(e) {
        e.preventDefault();

        try {
            await createSubject(form);
            alert("Matéria cadastrada com sucesso!");
            navigate("/secretary/dashboard");
        } catch (error) {
            console.error(error);
            alert("Erro ao cadastrar matéria.");
        }
    }

    return (
        <div className="min-h-screen bg-[#1F3D2E] flex justify-center items-center p-8">
            <div className="w-full max-w-xl">
                <h1 className="text-4xl text-[#E8C468] font-bold mb-8 text-center">
                    Cadastrar Matéria
                </h1>

                <form
                    onSubmit={handleSubmit}
                    className="bg-[#2C5240] rounded-xl shadow-xl p-8 space-y-6"
                >
                    <div>
                        <label className="block text-[#F5F3E7] mb-2 font-medium">
                            Nome da Matéria
                        </label>
                        <input
                            type="text"
                            name="name"
                            value={form.name}
                            onChange={handleChange}
                            placeholder="Ex: Matemática, História..."
                            required
                            className="w-full rounded-lg bg-[#1F3D2E] border border-[#C9D6C9] text-[#F5F3E7] p-3 placeholder:opacity-40 focus:outline-none focus:border-[#E8C468]"
                        />
                    </div>

                    <button
                        type="submit"
                        className="w-full bg-[#E8C468] text-[#1F3D2E] font-bold py-3 rounded-lg hover:brightness-110 transition mt-4"
                    >
                        Cadastrar Matéria
                    </button>
                </form>
            </div>
        </div>
    );
}