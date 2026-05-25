
# 🚀 PlanoriaCapstone API

API REST desarrollada en **ASP.NET Core** para la gestión de aprendizaje inteligente basado en documentos, flashcards, quizzes y análisis con IA.

---

## 📌 Descripción del Proyecto

PlanoriaCapstone es una plataforma backend que permite:

- 📄 Subida y procesamiento de archivos
- 🧠 Generación de análisis con IA
- 📚 Creación automática de flashcards
- 📝 Generación y resolución de quizzes
- 📊 Seguimiento de progreso del usuario

---

## 🏗️ Arquitectura

El proyecto sigue una arquitectura en capas:

- **API (Controllers)**
- **BLL (Services)**
- **DAL (Repositories)**
- **DTOs**
- **Models**
- **Entity Framework Core**

---

## 🔐 Autenticación

La API utiliza **JWT Bearer Token**.

Todas las peticiones deben incluir:

```http
Authorization: Bearer {token}
📚 Endpoints principales
📄 Archivos
GET /api/Archivo/{id} → Obtener archivo

🧠 Flashcards
GET /api/Flashcards?idAnalisis={id}
GET /api/Flashcards/todos
GET /api/Flashcards/{id}
POST /api/Flashcards
POST /api/Flashcards/responder

🧪 Quizzes
GET /api/Quiz/todos
GET /api/Quiz?idArchivo={id}
GET /api/Quiz/{id}
POST /api/Quiz/{id}/resolver

📊 Progreso
GET /api/Progreso
GET /api/Progreso/{idArchivo}
GET /api/Progreso/{idArchivo}/promedio

📈 Progreso Flashcards
GET /api/ProgresoFlashcard
GET /api/ProgresoFlashcard/{idFlashcard}

📊 Progreso Quiz
GET /api/ProgresoQuiz
GET /api/ProgresoQuiz/{idQuiz}

📦 Tecnologías usadas
ASP.NET Core 8
Entity Framework Core
SQL Server
JWT Authentication
Swagger / OpenAPI

⚙️ Instalación local
git clone https://github.com/tuusuario/planoria-capstone.git
cd planoria-capstone
dotnet restore
dotnet run
🐳 Ejecución con Docker (opcional)
docker build -t planoria-api .
docker run -p 8080:80 planoria-api
