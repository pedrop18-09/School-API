import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import PersonForm from "../../components/PersonForm";

import {
    getStudentById,
    updateStudent
} from "../../services/studentService.js";

import { getAllClasses } from "../../services/classService.js";

export default function EditStudentPage() {
    const { id } = useParams();

    const navigate = useNavigate();

    const [classes, setClasses] = useState([]);

    const [form, setForm] = useState({
        name: "",
        cpf: "",
        registration: "",
        dateOfBirth: "",
        classId: ""
    });

    useEffect(() => {
        async function loadData() {
            try {
                const student = await getStudentById(id);

                const classesData = await getAllClasses();

                setClasses(classesData);

                setForm({
                    name: student.name,
                    cpf: student.cpf,
                    registration: student.registration,
                    dateOfBirth: student.dateOfBirth.split("T")[0],
                    classId: student.classId
                });
            } catch (error) {
                console.error(error);
                alert("Erro ao carregar aluno.");
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
            await updateStudent(id, {
                name: form.name,
                dateOfBirth: form.dateOfBirth,
                classId: form.classId
            });

            alert("Aluno atualizado com sucesso!");

            navigate("/secretary/dashboard");
        } catch (error) {
            console.error(error);
            alert("Erro ao atualizar aluno.");
        }
    }

    return (
        <div className="min-h-screen bg-[#1F3D2E] flex items-center justify-center p-8">
            <div className="w-full max-w-xl">
                <h1 className="text-4xl font-bold text-[#E8C468] text-center mb-8">
                    Editar Aluno
                </h1>

                <PersonForm
                    form={form}
                    handleChange={handleChange}
                    classes={classes}
                    showRegistration
                    showClass
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