import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import PersonForm from "../../components/PersonForm";
import { getTeacherById, updateTeacher } from "../../services/teacherService.js";

export default function EditTeacherPage() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [form, setForm] = useState({
        name: "",
        cpf: "",
        registration: "",
        dateOfBirth: ""
    });

    useEffect(() => {
        async function loadData() {
            try {
                const teacher = await getTeacherById(id);
                setForm({
                    name: teacher.name || "",
                    cpf: teacher.cpf || "",
                    registration: teacher.registration || "", // Se vier nulo, vira ""
                    dateOfBirth: teacher.dateOfBirth ? teacher.dateOfBirth.split("T")[0] : ""
                });
            } catch (error) {
                console.error(error);
                alert("Erro ao carregar professor.");
            }
        }
        loadData();
    }, [id]);

    function handleChange(e) {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    }

    async function handleSubmit(e) {
        e.preventDefault();

        try {
            await updateTeacher(id, {
                name: form.name,
                dateOfBirth: form.dateOfBirth
            });

            alert("Professor atualizado com sucesso!");
            navigate("/secretary/dashboard");
        } catch (error) {
            console.error(error);
            alert("Erro ao atualizar professor.");
        }
    }

    return (
        <div className="min-h-screen bg-[#1F3D2E] flex justify-center items-center p-8">
            <div className="w-full max-w-xl">
                <h1 className="text-4xl text-[#E8C468] font-bold mb-8 text-center">
                    Editar Professor
                </h1>

                <PersonForm
                    form={form}
                    handleChange={handleChange}
                    showRegistration
                    showClass={false}
                    submitText="Salvar Alterações"
                    onSubmit={handleSubmit}
                    disabledFields={{
                        cpf: true,
                        registration: true
                    }}
                />
            </div>
        </div>
    );
}