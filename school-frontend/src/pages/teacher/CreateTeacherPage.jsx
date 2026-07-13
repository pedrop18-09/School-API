import { useState } from "react";
import { useNavigate } from "react-router-dom";

import PersonForm from "../../components/PersonForm";
import { createTeacher } from "../../services/teacherService.js";

export default function CreateTeacherPage() {
    const navigate = useNavigate();

    const [form, setForm] = useState({
        name: "",
        cpf: "",
        password: "",
        dateOfBirth: ""
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
            await createTeacher(form);
            alert("Professor cadastrado com sucesso!");
            navigate("/secretary/dashboard");
        } catch (error) {
            console.error(error);
            alert("Erro ao cadastrar professor.");
        }
    }

    return (
        <div className="min-h-screen bg-[#1F3D2E] flex justify-center items-center p-8">
            <div className="w-full max-w-xl">
                <h1 className="text-4xl text-[#E8C468] font-bold mb-8 text-center">
                    Cadastrar Professor
                </h1>

                <PersonForm
                    form={form}
                    handleChange={handleChange}
                    showPassword
                    showRegistration={false}
                    showClass={false}
                    submitText="Cadastrar"
                    onSubmit={handleSubmit}
                />
            </div>
        </div>
    );
}