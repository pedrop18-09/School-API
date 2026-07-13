import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import PersonForm from "../../components/PersonForm";

import { createStudent } from "../../services/studentService";
import { getAllClasses } from "../../services/classService.js";

export default function CreateStudentPage() {
    const navigate = useNavigate();

    const [classes, setClasses] = useState([]);

    const [form, setForm] = useState({
        name: "",
        cpf: "",
        password: "",
        registration: "",
        dateOfBirth: "",
        classId: ""
    });

    useEffect(() => {
        async function loadClasses() {
            try {
                const data = await getAllClasses();
                setClasses(data);
            } catch (error) {
                console.error(error);
                alert("Erro ao carregar as turmas.");
            }
        }

        loadClasses();
    }, []);

    function handleChange(e) {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    }

    async function handleSubmit(e) {
        e.preventDefault();

        try {
            await createStudent(form);

            alert("Aluno cadastrado com sucesso!");

            navigate("/secretary/dashboard");
        } catch (error) {
            console.error(error);
            alert("Erro ao cadastrar aluno.");
        }
    }

    return (
        <div className="min-h-screen bg-[#1F3D2E] flex items-center justify-center p-8">
            <div className="w-full max-w-xl">
                <h1 className="text-4xl font-bold text-[#E8C468] text-center mb-8">
                    Cadastrar Aluno
                </h1>

                <PersonForm
                    form={form}
                    handleChange={handleChange}
                    classes={classes}
                    showPassword
                    showRegistration
                    showClass
                    submitText="Cadastrar"
                    onSubmit={handleSubmit}
                />
            </div>
        </div>
    );
}