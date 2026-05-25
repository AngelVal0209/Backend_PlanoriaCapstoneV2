# PlanoriaCapstone API

API REST desarrollada en **ASP.NET Core** para la gestión de aprendizaje inteligente basado en documentos, flashcards, quizzes y análisis con IA.

---

## Descripción del Proyecto

PlanoriaCapstone es una plataforma backend que permite:

- 📄 Subida y procesamiento de archivos
- 🧠 Generación de análisis con IA
- 📚 Creación automática de flashcards
- 📝 Generación y resolución de quizzes
- 📊 Seguimiento de progreso del usuario

---

## Arquitectura

El proyecto sigue una arquitectura en capas:

- **API (Controllers)**
- **BLL (Services)**
- **DAL (Repositories)**
- **DTOs**
- **Models**
- **Entity Framework Core**

---

## Autenticación

La API utiliza **JWT Bearer Token**.

Todas las peticiones deben incluir:

```http
Authorization: Bearer {token}
```
## 📚 Endpoints principales

📄 Archivos
- GET /api/Archivo/{id} → Obtener archivo por ID
  
🧠 Flashcards
- GET /api/Flashcards?idAnalisis={id} → Obtener flashcards por análisis
- GET /api/Flashcards/todos → Listar todas las flashcards
- GET /api/Flashcards/{id} → Obtener flashcard por ID
- POST /api/Flashcards → Crear flashcard
- POST /api/Flashcards/responder → Registrar respuesta de flashcard
  
🧪 Quizzes
- GET /api/Quiz/todos → Listar todos los quizzes
- GET /api/Quiz?idArchivo={id} → Obtener quizzes por archivo
- GET /api/Quiz/{id} → Obtener quiz por ID
- POST /api/Quiz/{id}/resolver → Resolver quiz
  
📊 Progreso
- GET /api/Progreso → Obtener progreso general del usuario
- GET /api/Progreso/{idArchivo} → Progreso por archivo
- GET /api/Progreso/{idArchivo}/promedio → Promedio de quiz por archivo
  
📈 Progreso Flashcards
- GET /api/ProgresoFlashcard → Listar progreso de flashcards
- GET /api/ProgresoFlashcard/{idFlashcard} → Progreso por flashcard
  
📊 Progreso Quiz
- GET /api/ProgresoQuiz → Listar progreso de quizzes
- GET /api/ProgresoQuiz/{idQuiz} → Progreso por quiz

## Tecnologías usadas
- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger / OpenAPI

## Instalación local

```http
git clone https://github.com/tuusuario/planoria-capstone.git
cd planoria-capstone
dotnet restore
dotnet run
```
## Ejecución con Docker (opcional)

```http
docker build -t planoria-api .
docker run -p 8080:80 planoria-api
```
