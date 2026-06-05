# MODULOS DE DESARROLLO

### **Reglas de oro**

1. **Nunca trabajar directamente en develop o main**
2. **Hacer merge a develop SOLO cuando el módulo está funcional y testeado**
3. **Cada desarrollador trabaja en su feature branch**
4. **Reunión diaria de 15 min para sincronizar integraciones**
5. **Usar Conventional Commits para mensajes claros**

---

## **FASE 0: FUNDACIÓN (Día 1-2) - TODOS JUNTOS**

### **Configuración inicial (Trabajo colaborativo)**

- Configurar repositorio, CI/CD, base de datos
- Definir estándares de código (linter, formatter)
- Crear esquema base de datos (solo tablas core sin FK complejas)
- Configurar entorno de desarrollo compartido

**Entregable:** Proyecto base con conexión a DB y estructura de carpetas

---

## **FASE 1: SPRINT 1 (Día 3-8) - Módulos Base**

### **Desarrollador A: Autenticación y Usuarios**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | Registro/login (JWT o sesión) | users | Ninguna |
| 3-4 | Perfil de usuario, edición de datos | users, system_configurations | Ninguna |
| 5-6 | Recuperación contraseña, verificación email | users | Ninguna |
| 7-8 | Middleware de autenticación, rutas protegidas | - | users |

### **Desarrollador B: Gestión de Cursos**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | CRUD de cursos | courses | users (FK) |
| 3-4 | Asignación usuario-curso | user_courses | courses, users |
| 5-6 | Fechas de examen, archivar cursos | courses | users |
| 7-8 | UI de listado y detalle de cursos | - | courses, user_courses |

### **Desarrollador C: Subida de Archivos**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-3 | Sistema de subida de archivos (pdf,txt,docx) | file_uploads | users |
| 4-5 | Almacenamiento en cloud/local y validaciones | file_uploads | users |
| 6-7 | Procesamiento básico (extraer texto) | file_uploads | Ninguna |
| 8-9 | API de integración con IA (mock primero) | generated_contents | file_uploads, courses |

### **Integración FASE 1 (Día 9-10)**

- Merge de las 3 ramas a develop
- Resolver conflictos (principalmente en rutas y modelos)
- Pruebas de integración: usuario logueado → crea curso → sube archivo

---

## **FASE 2: SPRINT 2 (Día 11-18) - Flashcards y Quizzes Base**

### **Preparación: El día 10, todos actualizan develop y crean nuevas feature branches**

### **Desarrollador A: Flashcards Base**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | CRUD de mazos | flashcards_decks | courses |
| 3-4 | CRUD de flashcards individuales | flashcards | flashcards_decks |
| 5-6 | Vista de estudio básica (mostrar/ocultar respuesta) | flashcards | courses, flashcards |
| 7-8 | Marcador "Lo sabía/No lo sabía" | flashcard_study_sessions, flashcard_reviews | flashcards, users |

### **Desarrollador B: Quizzes Base + Generación IA**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | CRUD de quizzes | quizzes | courses |
| 3-4 | CRUD de preguntas y opciones | quiz_questions, quiz_options | quizzes |
| 5-6 | Integración real con IA (OpenAI/alternativa) | generated_contents | file_uploads, courses |
| 7-8 | Generación flashcards/quizzes desde archivo | flashcards_decks, flashcards, quizzes | IA integrada |

### **Desarrollador C: Sistema de Repetición Espaciada**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | Algoritmo SM-2 básico | flashcard_reviews | flashcards, users |
| 3-4 | Configuración de intervalos | spaced_repetition_settings | users, flashcards_decks |
| 5-6 | Lógica "repetir solo las que NO sabía" | flashcard_study_sessions | flashcard_reviews |
| 7-8 | Programador de repasos (next_review_date) | flashcard_reviews | flashcard_study_sessions |

### **Integración FASE 2 (Día 19-20)**

- Merge a develop con feature flags para módulos incompletos
- Probar flujo completo: subir archivo → generar flashcards → estudiar → repaso espaciado

---

## **FASE 3: SPRINT 3 (Día 21-28) - Quizzes y Progreso Individual**

### **Desarrollador A: Sistema de Quizzes (Usuario)**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | Tomar quiz (frontend interactivo) | quiz_questions, quiz_options | quizzes |
| 3-4 | Guardar intentos y respuestas | quiz_attempts, quiz_answers | quizzes, users |
| 5-6 | Corrección automática y puntaje | quiz_attempts | quiz_answers |
| 7-8 | Historial de intentos del usuario | quiz_attempts | users |

### **Desarrollador B: Progreso de Flashcards**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | Métricas por mazo (total, estudiadas, dominadas) | user_progress_flashcards | users, flashcards_decks |
| 3-4 | Cálculo de tarjetas en aprendizaje vs dominadas | flashcard_reviews | user_progress_flashcards |
| 5-6 | Dashboard de progreso individual de flashcards | user_progress_flashcards | flashcards_decks |
| 7-8 | Actualización en tiempo real al finalizar sesión | flashcard_study_sessions | user_progress_flashcards |

### **Desarrollador C: Progreso de Quizzes + UI**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | Métricas por quiz (intentos, mejor puntaje) | user_progress_quizzes | users, quizzes |
| 3-4 | Cálculo de promedio y aprobados | user_progress_quizzes | quiz_attempts |
| 5-6 | Componentes UI de progreso (gráficas simples) | user_progress_quizzes, user_progress_flashcards | - |
| 7-8 | Vista general de progreso por curso | user_course_exam_progress | courses |

### **Integración FASE 3 (Día 29-30)**

- Merge y prueba de todos los módulos de progreso
- Verificar consistencia de datos entre flashcards y quizzes

---

## **FASE 4: SPRINT 4 (Día 31-38) - Cronograma y Progreso General**

### **Desarrollador A: Cronograma de Estudios**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-3 | CRUD de sesiones de estudio | study_schedules | users, courses |
| 4-5 | Intervalos Pomodoro o personalizados | schedule_intervals | study_schedules |
| 6-7 | Asignar contenido a sesiones | schedule_contents | study_schedules, flashcards_decks, quizzes |
| 8-10 | Sistema de notificaciones básico | notifications | users, study_schedules |

### **Desarrollador B: Progreso General y Readiness**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | Cálculo de readiness por curso | exam_readiness_scores | user_course_exam_progress |
| 3-4 | Algoritmo de preparación (0-100) ponderado | user_course_exam_progress | courses (exam_date) |
| 5-6 | Dashboard general de todos los cursos | exam_readiness_scores | user_course_exam_progress |
| 7-8 | Gráficas de progreso vs tiempo hasta examen | exam_readiness_scores | courses (exam_date) |

### **Desarrollador C: Sistema de Recomendaciones**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | Sugerir qué estudiar según fecha examen | flashcards_decks, quizzes | courses, exam_readiness_scores |
| 3-4 | Priorización de contenido con bajo progreso | user_progress_flashcards, user_progress_quizzes | study_schedules |
| 5-6 | Auto-programación de sesiones recomendadas | study_schedules, schedule_contents | - |
| 7-8 | UI de plan de estudio inteligente | study_schedules | notifications |

### **Integración FASE 4 (Día 39-40)**

- Merge final de funcionalidades core
- Prueba de integración completa: curso con examen → progreso → cronograma automático → notificaciones

---

## **FASE 5: SPRINT 5 (Día 41-48) - Reportes y Refinamiento**

### **Desarrollador A: Reportes y Exportación**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | Exportar progreso a PDF/CSV | user_course_exam_progress | - |
| 3-4 | Reporte de actividad por tiempo | activity_logs | users |
| 5-6 | Gráficas avanzadas (Chart.js/D3) | - | user_progress_* |
| 7-8 | Logs de auditoría completos | activity_logs | todas |

### **Desarrollador B: Edición Avanzada y UX**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | Edición masiva de flashcards | flashcards | flashcards_decks |
| 3-4 | Import/export de mazos (CSV/Anki) | flashcards, flashcards_decks | - |
| 5-6 | Modo oscuro y persistencia de preferencias | users (theme, config) | - |
| 7-8 | Drag & drop para cronograma | schedule_contents | study_schedules |

### **Desarrollador C: Optimización y Testing**

| **Días** | **Tareas** | **Tablas involucradas** | **Dependencias** |
| --- | --- | --- | --- |
| 1-2 | Tests unitarios (Jest/PHPUnit según stack) | todas | todas |
| 3-4 | Tests de integración (flujos completos) | todas | todas |
| 5-6 | Optimización de consultas (índices, caché) | todas | - |
| 7-8 | Pruebas de carga y rendimiento | - | - |

### **Integración Final (Día 49-50)**

- QA completo
- Corrección de bugs críticos
- Merge a main y deploy