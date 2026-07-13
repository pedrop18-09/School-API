import { Routes, Route } from "react-router-dom";

import Home from "./pages/Home";
import LoginSecretary from "./pages/secretary/LoginSecretary";
import LoginTeacherStudent from "./pages/LoginTeacherStudent";
import SecretaryDashboard from "./pages/secretary/SecretaryDashboard";

import TeacherDashboard from "./pages/teacher/TeacherDashboard";


// IMPORTS DOS ALUNOS
import CreateStudentPage from "./pages/student/CreateStudentPage";
import EditStudentPage from "./pages/student/EditStudentPage";


import CreateTeacherPage from "./pages/teacher/CreateTeacherPage";
import EditTeacherPage from "./pages/teacher/EditTeacherPage";

import CreateSubjectPage from "./pages/CreateSubjectPage";

import AuditLogsPage from "./pages/AuditLogsPage";

import TeacherClassPerformance from "./pages/teacher/TeacherClassPerformance";

import TeacherGrades from "./pages/teacher/TeacherGrades";
import StudentDashboard from "./pages/student/StudentDashBoard";

import TeacherClassDetail from "./pages/teacher/TeacherClassDetail";

// IMPORT DO VÍNCULO TURMA-DISCIPLINA-PROFESSOR
import ClassSubjectForm from "./pages/secretary/ClassSubjectForm";

import InactiveTeachersPage from "./pages/secretary/InactiveTeachersPage";
import ClassSubjectsListPage from "./pages/secretary/ClassSubjectsListPage";
import ClassSubjectEditPage from "./pages/secretary/ClassSubjectEditPage";
import SubjectsListPage from "./pages/secretary/SubjectsListPage";
import InactiveStudentsPage from "./pages/secretary/InactiveStudentsPage";
import CreateClassPage from "./pages/secretary/CreateClassPage";
import ClassesListPage from "./pages/secretary/ClassesListPage";

import ProtectedRoute from "./routes/ProtectedRoute";

function App() {
    return (
        <Routes>
            <Route path="/" element={<Home />} />

            <Route
                path="/login/secretary"
                element={<LoginSecretary />}
            />

            <Route
                path="/login/teacher"
                element={<LoginTeacherStudent role="teacher" />}
            />

            <Route
                path="/login/student"
                element={<LoginTeacherStudent role="student" />}
            />

            {/* DASHBOARD DA SECRETÁRIA */}
            <Route
                path="/secretary/dashboard"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <SecretaryDashboard />
                    </ProtectedRoute>
                }
            />

            {/* ROTAS DE ALUNOS */}
            <Route
                path="/secretary/students/new"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <CreateStudentPage />
                    </ProtectedRoute>
                }
            />

            <Route
                path="/secretary/students/:id/edit"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <EditStudentPage />
                    </ProtectedRoute>
                }
            />

            {/* ROTAS DE PROFESSORES */}
            <Route
                path="/secretary/teachers/new"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <CreateTeacherPage />
                    </ProtectedRoute>
                }
            />

            <Route
                path="/secretary/teachers/:id/edit"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <EditTeacherPage />
                    </ProtectedRoute>
                }
            />

            <Route
                path="/secretary/subjects/new"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <CreateSubjectPage />
                    </ProtectedRoute>
                }
            />

            {/* VÍNCULO TURMA-DISCIPLINA-PROFESSOR */}
            <Route
                path="/secretary/class-subjects/new"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <ClassSubjectForm />
                    </ProtectedRoute>
                }
            />

            <Route
                path="/secretary/audit"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <AuditLogsPage />
                    </ProtectedRoute>
                }
            />
            <Route
                path="/teacher/dashboard"
                element={
                    <ProtectedRoute allowedRoles={["Teacher"]}>
                        <TeacherDashboard />
                    </ProtectedRoute>
                }
            />
            <Route
            path="/teacher/class/:classSubjectId"
            element={
                <ProtectedRoute allowedRoles={["Teacher"]}>
                <TeacherClassDetail />
                </ProtectedRoute>
            }
            />
            <Route
                path="/teacher/class/:classSubjectId/performance"
                element={
                    <ProtectedRoute allowedRoles={["Teacher"]}>
                        <TeacherClassPerformance />
                    </ProtectedRoute>
                }
            />
            <Route
                path="/teacher/class/:classSubjectId/grades"
                element={
                    <ProtectedRoute allowedRoles={["Teacher"]}>
                        <TeacherGrades />
                    </ProtectedRoute>
                }
            />
            <Route
                path="/student/dashboard"
                element={
                    <ProtectedRoute allowedRoles={["Student"]}>
                        <StudentDashboard />
                    </ProtectedRoute>
                }
            />
            <Route
                path="/secretary/teachers/inactive"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <InactiveTeachersPage />
                    </ProtectedRoute>
                }
            />
            <Route
                path="/secretary/class-subjects"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <ClassSubjectsListPage />
                    </ProtectedRoute>
                }
            />
            <Route
                path="/secretary/class-subjects/:id/edit"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <ClassSubjectEditPage />
                    </ProtectedRoute>
                }
            />
            <Route
                path="/secretary/subjects"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <SubjectsListPage />
                    </ProtectedRoute>
                }
            />
            <Route
                path="/secretary/students/inactive"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <InactiveStudentsPage />
                    </ProtectedRoute>
                }
            />
            <Route
                path="/secretary/classes/new"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <CreateClassPage />
                    </ProtectedRoute>
                }
            />
            <Route
                path="/secretary/classes"
                element={
                    <ProtectedRoute allowedRoles={["Secretary"]}>
                        <ClassesListPage />
                    </ProtectedRoute>
                }
            />
        </Routes>
    );
}

export default App;