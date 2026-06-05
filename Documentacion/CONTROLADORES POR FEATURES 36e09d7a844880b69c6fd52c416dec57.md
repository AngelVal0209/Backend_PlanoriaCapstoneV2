# CONTROLADORES POR FEATURES

## **FASE 1: AUTENTICACIÓN Y USUARIOS (Desarrollador A)**

### **Controladores de Autenticación**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 1 | `auth_register_controller` | register, verifyEmail, resendVerification | Registro de nuevos usuarios |
| 2 | `auth_login_controller` | login, logout, refreshToken | Autenticación y sesiones |
| 3 | `auth_password_controller` | forgotPassword, resetPassword, changePassword | Recuperación y cambio de contraseña |
| 4 | `auth_social_controller` | googleCallback, facebookCallback | Autenticación con redes sociales (opcional) |

### **Controladores de Usuario**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 5 | `user_profile_controller` | show, update, uploadAvatar, deleteAvatar | Perfil de usuario |
| 6 | `user_preferences_controller` | show, update, resetDefaults | Preferencias (tema, idioma, notificaciones) |
| 7 | `user_notification_settings_controller` | index, update, testNotification | Configuración de notificaciones |
| 8 | `user_account_controller` | deleteAccount, exportData, deactivate | Gestión de cuenta |

---

## **FASE 1: GESTIÓN DE CURSOS (Desarrollador B)**

### **Controladores de Cursos**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 9 | `course_controller` | index, show, store, update, destroy, archive, restore | CRUD completo de cursos |
| 10 | `course_exam_controller` | setExamDate, getExamDate, removeExamDate | Gestión de fecha de examen |
| 11 | `course_members_controller` | index, addMember, removeMember, changeRole | Usuarios compartidos en curso |
| 12 | `course_stats_controller` | overview, recentActivity, upcomingReviews | Estadísticas por curso |
| 13 | `course_search_controller` | search, filter, sort | Búsqueda y filtrado de cursos |

---

## **FASE 1: SUBIDA DE ARCHIVOS (Desarrollador C)**

### **Controladores de Archivos**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 14 | `file_upload_controller` | upload, getUploadStatus, getUploadHistory, deleteUpload | Subida de archivos (pdf, txt, docx) |
| 15 | `file_validation_controller` | validate, checkMimeType, scanVirus | Validación de archivos |
| 16 | `file_processing_controller` | processFile, getProcessingStatus, reprocess | Procesamiento y extracción de texto |
| 17 | `file_download_controller` | download, getFileUrl, streamFile | Descarga de archivos originales |

---

## **FASE 2: FLASHCARDS (Desarrollador A)**

### **Controladores de Mazos**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 18 | `deck_controller` | index, show, store, update, destroy, duplicate | CRUD de mazos de flashcards |
| 19 | `deck_card_controller` | indexByDeck, addCards, removeCards, reorder | Gestión de tarjetas dentro de mazo |
| 20 | `deck_stats_controller` | completionRate, masteryLevel, studyHeatmap | Estadísticas del mazo |
| 21 | `deck_share_controller` | share, unshare, getSharedDecks, importShared | Compartir mazos entre usuarios |

### **Controladores de Flashcards**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 22 | `flashcard_controller` | index, show, store, update, destroy, bulkStore, bulkUpdate | CRUD de flashcards individuales |
| 23 | `flashcard_tag_controller` | addTag, removeTag, getByTag, suggestTags | Etiquetado de flashcards |
| 24 | `flashcard_search_controller` | searchByQuestion, searchByAnswer, fullTextSearch | Búsqueda en flashcards |
| 25 | `flashcard_import_controller` | importFromCsv, importFromAnki, importFromJson | Importación externa |

### **Controladores de Estudio Flashcards**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 26 | `flashcard_study_controller` | startSession, getNextCard, submitAnswer, endSession | Sesión de estudio |
| 27 | `flashcard_review_controller` | getDueCards, getOverdueCards, scheduleReview | Gestión de repasos |
| 28 | `flashcard_session_history_controller` | index, show, getSummary, getPerformance | Historial de sesiones |

---

## **FASE 2: QUIZZES (Desarrollador B)**

### **Controladores de Quizzes**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 29 | `quiz_controller` | index, show, store, update, destroy, duplicate | CRUD de quizzes |
| 30 | `quiz_question_controller` | index, show, store, update, destroy, reorder | Gestión de preguntas del quiz |
| 31 | `quiz_option_controller` | store, update, destroy (anidado a preguntas) | Opciones de respuesta |
| 32 | `quiz_settings_controller` | updateSettings, getSettings, resetSettings | Configuración (tiempo, puntaje) |
| 33 | `quiz_preview_controller` | preview, simulate, validateQuestions | Vista previa del quiz |

### **Controladores de Intentos de Quiz**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 34 | `quiz_attempt_controller` | start, submit, getResult, getAttempts | Realizar quiz |
| 35 | `quiz_answer_controller` | saveAnswer, updateAnswer, bulkSave | Guardar respuestas |
| 36 | `quiz_grading_controller` | autoGrade, manualGrade (si aplica), regrade | Corrección automática |
| 37 | `quiz_attempt_history_controller` | index, show, compareAttempts, getBestAttempt | Historial de intentos |

---

## **FASE 2: GENERACIÓN IA (Desarrollador C)**

### **Controladores de Generación**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 38 | `ai_generation_controller` | generateFlashcards, generateQuiz, getGenerationStatus | Generación desde archivo |
| 39 | `ai_generation_config_controller` | setConfig, getConfig, testConnection | Configuración de IA |
| 40 | `ai_regeneration_controller` | regenerate, improveQuestions, adjustDifficulty | Regenerar o mejorar contenido |
| 41 | `ai_content_history_controller` | index, show, getGeneratedContent, deleteHistory | Historial de generaciones |

---

## **FASE 3: PROGRESO INDIVIDUAL (Desarrollador B y C)**

### **Controladores de Progreso Flashcards**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 42 | `progress_flashcard_deck_controller` | getByDeck, getByCourse, getOverall | Progreso por mazo |
| 43 | `progress_flashcard_mastery_controller` | getMasteryLevel, getMasteryTrend, getPredictions | Nivel de dominio |
| 44 | `progress_flashcard_timeline_controller` | getTimeline, getWeeklyProgress, getMonthlyReport | Progreso temporal |

### **Controladores de Progreso Quizzes**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 45 | `progress_quiz_controller` | getByQuiz, getByCourse, getOverall | Progreso por quiz |
| 46 | `progress_quiz_performance_controller` | getAverageScore, getWeakTopics, getImprovement | Análisis de rendimiento |
| 47 | `progress_quiz_comparison_controller` | compareQuizzes, compareCourses, compareTimeframes | Comparativas |

### **Controladores de Progreso Curso-Examen**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 48 | `progress_course_exam_controller` | getExamProgress, getReadinessScore, getRecommendations | Progreso hacia examen |
| 49 | `progress_course_readiness_history_controller` | getHistory, getTrend, getPredictions | Histórico de readiness |
| 50 | `progress_course_weaknesses_controller` | identifyWeaknesses, getPriorityTopics, suggestFocus | Identificación de debilidades |

---

## **FASE 4: CRONOGRAMA (Desarrollador A)**

### **Controladores de Sesiones**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 51 | `schedule_controller` | index, show, store, update, destroy, getByDateRange | CRUD de sesiones de estudio |
| 52 | `schedule_calendar_controller` | getMonthView, getWeekView, getDayView, getAgenda | Vistas de calendario |
| 53 | `schedule_recurring_controller` | createRecurring, updateRecurring, deleteRecurring | Sesiones recurrentes |
| 54 | `schedule_completion_controller` | markComplete, markIncomplete, bulkComplete | Marcado de sesiones completadas |

### **Controladores de Intervalos**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 55 | `schedule_interval_controller` | store, update, destroy, reorder, getActiveInterval | Gestión de intervalos (Pomodoro) |
| 56 | `schedule_timer_controller` | startTimer, pauseTimer, resumeTimer, stopTimer | Temporizador de estudio |
| 57 | `schedule_interval_template_controller` | index, show, store, update, destroy, applyTemplate | Plantillas de intervalos |

### **Controladores de Contenido Programado**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 58 | `schedule_content_controller` | attachContent, detachContent, reorderContent, getAssignedContent | Asignar contenido a sesión |
| 59 | `schedule_priority_controller` | autoAssign, prioritizeByExam, prioritizeByWeakness | Asignación automática priorizada |
| 60 | `schedule_suggestion_controller` | suggestSession, suggestContent, optimizeSchedule | Sugerencias inteligentes |

---

## **FASE 4: PROGRESO GENERAL (Desarrollador B)**

### **Controladores de Dashboard**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 61 | `dashboard_overview_controller` | getSummary, getRecentActivity, getUpcomingDeadlines | Dashboard principal |
| 62 | `dashboard_metrics_controller` | getStudyTime, getCardsReviewed, getQuizzesCompleted | Métricas agregadas |
| 63 | `dashboard_charts_controller` | getProgressChart, getHeatmapData, getDistributionData | Datos para gráficas |
| 64 | `dashboard_export_controller` | exportToPDF, exportToCSV, generateReport | Exportación de progreso |

### **Controladores de Rendimiento General**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 65 | `performance_overall_controller` | getGlobalStats, getRanking, getAchievements | Estadísticas globales |
| 66 | `performance_trend_controller` | getWeeklyTrend, getMonthlyTrend, getYearlyReport | Tendencias temporales |
| 67 | `performance_goals_controller` | setGoals, getGoals, updateProgress, checkAchievement | Metas de estudio |

---

## **FASE 4: NOTIFICACIONES (Desarrollador C)**

### **Controladores de Notificaciones**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 68 | `notification_controller` | index, show, markAsRead, markAllRead, delete | CRUD de notificaciones |
| 69 | `notification_schedule_controller` | createReminder, getPendingReminders, cancelReminder | Notificaciones programadas |
| 70 | `notification_email_controller` | sendTestEmail, getEmailLogs, retryFailed | Notificaciones por email |
| 71 | `notification_push_controller` | registerDevice, unregisterDevice, sendPush | Notificaciones push (web/mobile) |

---

## **FASE 5: SISTEMA Y CONFIGURACIÓN (Todos)**

### **Controladores de Sistema**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 72 | `system_config_controller` | index, show, update, reset | Configuración del sistema |
| 73 | `system_health_controller` | healthCheck, getStatus, getMetrics | Monitoreo de salud |
| 74 | `system_cache_controller` | clearCache, getCacheStats, warmupCache | Gestión de caché |
| 75 | `system_logs_controller` | getLogs, searchLogs, exportLogs | Auditoría y logs |

### **Controladores de Reportes**

| **Orden** | **Controlador** | **Métodos** | **Descripción** |
| --- | --- | --- | --- |
| 76 | `report_study_controller` | generateStudyReport, getStudyInsights | Reporte de estudio |
| 77 | `report_performance_controller` | generatePerformanceReport, getPerformanceSummary | Reporte de rendimiento |
| 78 | `report_custom_controller` | createCustomReport, saveTemplate, scheduleReport | Reportes personalizados |