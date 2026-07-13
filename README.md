# School API - Project Overview

This is a simple overview and documentation of the mock entities, credentials, UUIDs, and JWT tokens used for testing the local School API environment (`localhost`).

---

## Authentication & Credentials

### Secretary Login
* **Email:** `secretario@escola.com`
* **Password:** `123456`
* **Token:** ```text
  eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjExMTExMTExLTExMTEtMTExMS0xMTExLTExMTExMTExMTExMSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJTZWNyZXTDoXJpbyBUZXN0ZSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlNlY3JldGFyeSIsImV4cCI6MTc4MzUyOTA4MiwiaXNzIjoiU2Nob29sQXBpIiwiYXVkIjoiU2Nob29sQXBpVXNlcnMifQ.ZnRriX5hLtRPhO__lc_0z10xDfNdui_4gFeexmjtB10


  ---

### Credenciais, IDs e Tokens Atuais do Projeto

> * **Secretário:** **E-mail:** `secretario@escola.com` | **Senha:** `123456`
> * **Token do Secretário:** `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjExMTExMTExLTExMTEtMTExMS0xMTExLTExMTExMTExMTExMSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJTZWNyZXTDoXJpbyBUZXN0ZSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlNlY3JldGFyeSIsImV4cCI6MTc4MzUyOTA4MiwiaXNzIjoiU2Nob29sQXBpIiwiYXVkIjoiU2Nob29sQXBpVXNlcnMifQ.ZnRriX5hLtRPhO__lc_0z10xDfNdui_4gFeexmjtB10`
> * **Professor (Carlos Silva):** `2e9f4d39-ffd2-41cd-a4a4-f484f95a93b2` | **CPF:** `12345678901` | **Senha:** `ProfessorSecurePassword2026`
> * **Token do Professor:** `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjJlOWY0ZDM5LWZmZDItNDFjZC1hNGE0LWY0ODRmOTVhOTNiMiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJDYXJlobSBTaWx2YSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6JVRlYWNoZXIiLCJleHAiOjE3ODM1MzI3NzAsImlzcyI6IlNjaG9vbEFwaSIsImF1ZCI6IlNjaG9vbEFwaVVzZXJzIn0.sp7EVUVw2vfNYx4dDqbXjK16xlpDQ3L3tQfhBDJBd_I`
> * **Novo Professor (Marcos Oliveira):** *Aguardando ID* | **CPF:** `44455566622` | **Senha:** `TeacherSecurePassword2026`
> * **Professor (Pedro Augusto):** *Aguardando ID* | **CPF:** `98765432108` | **Senha:** `234`
> * **Nova Matéria:** *Aguardando ID*
> * **Sala (6º ano A):** `fcf73921-aa5c-41d9-a46f-27a26033ebaa`
> * **Matéria (Matemática):** `cb5d9930-d629-48e6-a6f8-eab986645a84`
> * **Vínculo (ClassSubject):** `b6690763-2ffd-4485-9694-354d673ad01b`
> * **Aluno (Ana Souza):** `135732a3-37a0-4ffb-9c40-240a360bdf43` | **CPF:** `98765432100` | **Senha:** `AlunoSecurePassword2026`
> * **Token do Aluno (Ana):** `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEzNTczMmEzLTM3YTAtNGZmYi05YzQwLTI0MGEzNjBiZGY0MyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJBbmEgU291emEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdHVkZW50IiwiZXhwIjoxNzgzNTMzMzIyLCJpc3MiOiJTY2hvb2xBcGkiLCJhdWQiOiJTY2hvb2xBcGlVc2VycyJ9.XiBDzRvQAp7eg3YwdAt6YY_rJj9j_Qk9b0d94yCyQ6k`
> * **Aluno (Pedro Santos):** *Aguardando ID* | **CPF:** `11122233344` | **Senha:** `PedroSecurePassword2026`
> * **Aluno (Mariana Oliveira):** *Aguardando ID* | **CPF:** `55566677788` | **Senha:** `MariSecurePassword2026`
> * **Aluno (Lucas Almeida):** *Aguardando ID* | **CPF:** `22233344455` | **Senha:** `LucasSecurePassword2026`
> * **Grade Criada (Grid):** `7eef4a4b-f27b-443a-a0a9-ab2280509994`
