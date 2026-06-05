# ARQUITECTURA COMPLETA - Planoria API

## Stack Tecnológico

- **Framework:** ASP.NET Core 8 (.NET 8)
- **Lenguaje:** C# 12
- **ORM:** Entity Framework Core 8.0
- **Base de Datos:** SQL Server
- **Autenticación:** JWT Bearer
- **API Docs:** Swagger (Swashbuckle)
- **Patrones:** Repository, Inyección de Dependencias

---

## Estructura de Capas

```
Cliente (Frontend)
      ↓  HTTP (JSON)
─────────────────────────────
   API Layer (Controllers)
      ↓  Inyección de Dependencias
   BLL Layer (Services)
      ↓  Inyección de Dependencias
   DAL Layer (Repositories)
      ↓
   EF Core (AppDbContext)
      ↓
   SQL Server
```

### 1. DAL (`PlanoriaCapstone.Dal`) — Data Access Layer

13 repositorios (Interface + Implementation) registrados en DI:

| Repositorio | Métodos Clave |
|---|---|
| `IUserRepository` | GetById, GetByEmail, Create, Update, Delete |
| `ICourseRepository` | GetById, GetByUserId, Create, Update, Delete |
| `IUserCourseExamProgressRepository` | GetByUserAndCourse, CreateOrUpdate, GetReadinessHistory |
| `IFlashcardDeckRepository` | GetById, GetByCourseId, Create, Update, Delete |
| `IFlashcardRepository` | GetById, GetByDeckId, Create, Update, Delete, AddReview, GetDueReviews |
| `IUserProgressFlashcardRepository` | GetByUserAndDeck, GetByUser, CreateOrUpdate |
| `IQuizRepository` | GetById, GetByCourseId, Create, Update, Delete, GetAll |
| `IQuizAttemptRepository` | GetById, GetByUser, GetByQuizId, Create, Update, AddAnswer |
| `IUserProgressQuizRepository` | GetByUserAndQuiz, GetByUser, CreateOrUpdate |
| `IFileUploadRepository` | GetById, GetByUserId, Create, Update, Delete, GetGeneratedContent |
| `INotificationRepository` | GetByUser, Create, MarkAsRead, MarkAllAsRead, GetUnreadCount |
| `IStudyScheduleRepository` | GetById, GetByUser, GetByDateRange, Create, Update, Delete, AddInterval, AddContent |
| `IActivityLogRepository` | LogAsync, GetByUser, GetByEntity |

### 2. BLL (`PlanoriaCapstone.DLL`) — Business Logic Layer

21 servicios (Interface + Implementation) registrados en DI:

### 3. API (`Backend_PlanoriaCapstone`) — Controladores REST

20 controladores con ~200+ endpoints.

---

## Inventario Completo BLL

### 1. Auth — `IAuthService` / `AuthService`
| Método | Descripción |
|---|---|
| `RegisterAsync(RegisterRequestDto)` | Registro con hash BCrypt + JWT |
| `LoginAsync(LoginRequestDto)` | Login con verificación BCrypt + JWT |
| `LogoutAsync(int userId)` | Cierre de sesión (log) |
| `RefreshTokenAsync(RefreshTokenRequestDto)` | Refresh token (stub) |
| `VerifyEmailAsync(VerifyEmailRequestDto)` | Verificación email (stub) |
| `ResendVerificationAsync(ResendVerificationRequestDto)` | Reenviar verificación (stub) |
| `ForgotPasswordAsync(ForgotPasswordRequestDto)` | Olvido contraseña (stub) |
| `ResetPasswordAsync(ResetPasswordRequestDto)` | Reset contraseña (stub) |
| `ChangePasswordAsync(int, ChangePasswordRequestDto)` | Cambio contraseña |

### 2. Users — `IUserService` / `UserService`
| Método | Descripción |
|---|---|
| `GetProfileAsync(int userId)` | Obtener perfil |
| `UpdateProfileAsync(int, UpdateProfileRequestDto)` | Actualizar perfil |
| `UploadAvatarAsync(int, Stream, string)` | Subir avatar (stub) |
| `DeleteAvatarAsync(int userId)` | Eliminar avatar (stub) |
| `GetPreferencesAsync(int userId)` | Obtener preferencias |
| `UpdatePreferencesAsync(int, UpdatePreferencesRequestDto)` | Actualizar preferencias |
| `ResetDefaultsAsync(int userId)` | Resetear defaults |
| `GetNotificationSettingsAsync(int userId)` | Config notificaciones |
| `UpdateNotificationSettingsAsync(int, UpdateNotificationSettingsRequestDto)` | Actualizar config notif. |
| `TestNotificationAsync(int userId)` | Test notificación (stub) |
| `DeleteAccountAsync(int, DeleteAccountRequestDto)` | Eliminar cuenta (soft delete) |
| `ExportDataAsync(int, ExportDataRequestDto)` | Exportar datos (stub) |
| `DeactivateAsync(int userId)` | Desactivar cuenta |

### 3. Courses — `ICourseService` / `CourseService`
| Método | Descripción |
|---|---|
| `GetByIdAsync(int id)` | Obtener curso |
| `GetByUserIdAsync(int userId)` | Listar cursos del usuario |
| `CreateAsync(int, CreateCourseRequestDto)` | Crear curso (+ UserCourse owner) |
| `UpdateAsync(int, UpdateCourseRequestDto)` | Actualizar curso |
| `DeleteAsync(int id)` | Eliminar curso |
| `ArchiveAsync(int id)` | Archivar curso |
| `RestoreAsync(int id)` | Restaurar curso |
| `SetExamDateAsync(int, SetExamDateRequestDto)` | Fijar fecha examen |
| `GetExamDateAsync(int id)` | Obtener fecha examen |
| `RemoveExamDateAsync(int id)` | Eliminar fecha examen |
| `GetMembersAsync(int courseId)` | Miembros del curso |
| `AddMemberAsync(int, AddCourseMemberRequestDto)` | Agregar miembro |
| `RemoveMemberAsync(int, int)` | Eliminar miembro |
| `ChangeMemberRoleAsync(int, int, UpdateMemberRoleRequestDto)` | Cambiar rol |
| `GetStatsAsync(int courseId)` | Estadísticas del curso |
| `SearchAsync(CourseSearchRequestDto)` | Buscar cursos |

### 4. Files — `IFileService` / `FileService`
| Método | Descripción |
|---|---|
| `UploadAsync(int, int, Stream, string, string, long)` | Subir archivo a wwwroot |
| `GetUploadStatusAsync(int fileId)` | Estado de subida |
| `GetUploadHistoryAsync(int userId)` | Historial de subidas |
| `DeleteUploadAsync(int fileId)` | Eliminar archivo |
| `ProcessFileAsync(int, int, string)` | Procesar archivo (crea GeneratedContent) |
| `GetProcessingStatusAsync(int fileId)` | Estado de procesamiento |
| `ReprocessAsync(int fileId)` | Reprocesar |
| `DownloadAsync(int fileId)` | Descargar archivo |
| `GetFileUrlAsync(int fileId)` | Obtener URL (stub) |
| `StreamFileAsync(int fileId)` | Stream de archivo |

### 5. Flashcard Decks — `IFlashcardDeckService` / `FlashcardDeckService`
| Método | Descripción |
|---|---|
| `GetByIdAsync(int id)` | Obtener mazo |
| `GetByCourseIdAsync(int courseId)` | Mazos por curso |
| `CreateAsync(int, CreateDeckRequestDto)` | Crear mazo |
| `UpdateAsync(int, UpdateDeckRequestDto)` | Actualizar mazo |
| `DeleteAsync(int id)` | Eliminar mazo |
| `DuplicateAsync(int, DuplicateDeckRequestDto)` | Duplicar mazo |
| `GetCardsAsync(int deckId)` | Flashcards del mazo |
| `AddCardsAsync(int, BulkCreateFlashcardsRequestDto)` | Agregar flashcards |
| `RemoveCardsAsync(int, List<int>)` | Remover flashcards |
| `ReorderCardsAsync(int, List<ReorderFlashcardsRequestDto>)` | Reordenar |
| `GetStatsAsync(int deckId)` | Estadísticas del mazo |

### 6. Flashcards — `IFlashcardService` / `FlashcardService`
| Método | Descripción |
|---|---|
| `GetByIdAsync(int id)` | Obtener flashcard |
| `GetByDeckIdAsync(int deckId)` | Flashcards por mazo |
| `CreateAsync(CreateFlashcardRequestDto)` | Crear flashcard |
| `UpdateAsync(int, UpdateFlashcardRequestDto)` | Actualizar flashcard |
| `DeleteAsync(int id)` | Eliminar flashcard |
| `BulkCreateAsync(BulkCreateFlashcardsRequestDto)` | Creación masiva |
| `BulkUpdateAsync(List<BulkUpdateFlashcardsRequestDto>)` | Actualización masiva |
| `SearchAsync(SearchFlashcardRequestDto)` | Buscar flashcards |
| `ImportFromCsvAsync(int, Stream)` | Importar CSV (stub) |
| `ImportFromJsonAsync(int, Stream)` | Importar JSON (stub) |

### 7. Flashcard Study — `IFlashcardStudyService` / `FlashcardStudyService`

| Método | Descripción |
|---|---|
| `StartSessionAsync(int, StartStudySessionRequestDto)` | Iniciar sesión estudio |
| `GetNextCardAsync(int sessionId)` | Siguiente flashcard (SM-2) |
| `SubmitAnswerAsync(int, SubmitFlashcardAnswerRequestDto)` | Responder flashcard |
| `EndSessionAsync(int, EndStudySessionRequestDto)` | Finalizar sesión + actualizar progreso |
| `GetDueCardsAsync(int, int)` | Flashcards pendientes de repaso |
| `GetOverdueCardsAsync(int, int)` | Flashcards vencidas |
| `ScheduleReviewAsync(int, ScheduleReviewRequestDto)` | Programar repaso |
| `GetSessionHistoryAsync(int, int?)` | Historial de sesiones |
| `GetSessionAsync(int sessionId)` | Obtener sesión |
| `GetSessionSummaryAsync(int sessionId)` | Resumen de sesión |
| `GetPerformanceAsync(int, int)` | Rendimiento por mazo |

### 8. Quizzes — `IQuizService` / `QuizService`
| Método | Descripción |
|---|---|
| `GetByIdAsync(int id)` | Obtener quiz |
| `GetByCourseIdAsync(int courseId)` | Quizzes por curso |
| `GetAllAsync()` | Todos los quizzes |
| `CreateAsync(int, CreateQuizRequestDto)` | Crear quiz |
| `UpdateAsync(int, UpdateQuizRequestDto)` | Actualizar quiz |
| `DeleteAsync(int id)` | Eliminar quiz |
| `DuplicateAsync(int, DuplicateQuizRequestDto)` | Duplicar quiz |
| `GetQuestionsAsync(int quizId)` | Preguntas del quiz |
| `CreateQuestionAsync(int, CreateQuestionRequestDto)` | Crear pregunta |
| `UpdateQuestionAsync(int, UpdateQuestionRequestDto)` | Actualizar pregunta |
| `DeleteQuestionAsync(int questionId)` | Eliminar pregunta |
| `ReorderQuestionsAsync(int, List<ReorderQuestionsRequestDto>)` | Reordenar preguntas |
| `CreateOptionAsync(int, CreateOptionRequestDto)` | Crear opción |
| `UpdateOptionAsync(int, UpdateOptionRequestDto)` | Actualizar opción |
| `DeleteOptionAsync(int optionId)` | Eliminar opción |
| `UpdateSettingsAsync(int, object)` | Actualizar configuración |
| `GetSettingsAsync(int quizId)` | Obtener configuración |
| `ResetSettingsAsync(int quizId)` | Resetear configuración |
| `PreviewAsync(int quizId)` | Vista previa |
| `SimulateAsync(int quizId)` | Simular quiz |

### 9. Quiz Attempts — `IQuizAttemptService` / `QuizAttemptService`
| Método | Descripción |
|---|---|
| `StartAsync(int, StartQuizAttemptRequestDto)` | Iniciar intento |
| `SubmitAsync(int, SubmitQuizRequestDto)` | Enviar intento (calcular puntaje) |
| `GetResultAsync(int attemptId)` | Obtener resultado |
| `GetAttemptsAsync(int, int?)` | Intentos del usuario |
| `SaveAnswerAsync(int, SubmitAnswerRequestDto)` | Guardar respuesta |
| `UpdateAnswerAsync(int, SubmitAnswerRequestDto)` | Actualizar respuesta |
| `BulkSaveAnswersAsync(int, List<SubmitAnswerRequestDto>)` | Guardar respuestas masivo |
| `AutoGradeAsync(int attemptId)` | Corregir automáticamente |
| `RegradeAsync(int attemptId)` | Re-corregir |
| `GetHistoryAsync(int, int)` | Historial por quiz |
| `GetBestAttemptAsync(int, int)` | Mejor intento |
| `CompareAttemptsAsync(int, int)` | Comparar intentos |

### 10. AI Generation — `IAiGenerationService` / `AiGenerationService`
| Método | Descripción |
|---|---|
| `GenerateFlashcardsAsync(int, GenerateContentRequestDto)` | Generar flashcards (stub) |
| `GenerateQuizAsync(int, GenerateContentRequestDto)` | Generar quiz (stub) |
| `GetGenerationStatusAsync(int generationId)` | Estado de generación |
| `SetConfigAsync(AIConfigRequestDto)` | Configurar IA |
| `GetConfigAsync()` | Obtener config IA |
| `TestConnectionAsync()` | Probar conexión (stub) |
| `RegenerateAsync(ImproveContentRequestDto)` | Regenerar contenido |
| `ImproveQuestionsAsync(ImproveContentRequestDto)` | Mejorar preguntas |
| `AdjustDifficultyAsync(int, string)` | Ajustar dificultad |
| `GetHistoryAsync(int, int?)` | Historial de generaciones |
| `GetGeneratedContentAsync(int id)` | Contenido generado |
| `DeleteHistoryAsync(int id)` | Eliminar historial |

### 11. Flashcard Progress — `IFlashcardProgressService` / `FlashcardProgressService`
| Método | Descripción |
|---|---|
| `GetByDeckAsync(int, int)` | Progreso por mazo |
| `GetByCourseAsync(int, int)` | Progreso por curso |
| `GetOverallAsync(int userId)` | Progreso general |
| `GetMasteryLevelAsync(int, int)` | Nivel de dominio |
| `GetMasteryTrendAsync(int, int)` | Tendencia de dominio |
| `GetPredictionsAsync(int, int)` | Predicciones (stub) |
| `GetTimelineAsync(int, int)` | Timeline de progreso |
| `GetWeeklyProgressAsync(int userId)` | Progreso semanal |
| `GetMonthlyReportAsync(int, int, int)` | Reporte mensual |

### 12. Quiz Progress — `IQuizProgressService` / `QuizProgressService`
| Método | Descripción |
|---|---|
| `GetByQuizAsync(int, int)` | Progreso por quiz |
| `GetByCourseAsync(int, int)` | Progreso por curso |
| `GetOverallAsync(int userId)` | Progreso general |
| `GetAverageScoreAsync(int, int?)` | Puntaje promedio |
| `GetWeakTopicsAsync(int, int)` | Temas débiles |
| `GetImprovementAsync(int, int)` | Mejora |
| `CompareQuizzesAsync(int, int, int)` | Comparar quizzes |
| `CompareCoursesAsync(int, int, int)` | Comparar cursos |
| `CompareTimeframesAsync(int, DateTime, DateTime, DateTime, DateTime)` | Comparar períodos |

### 13. Course Exam Progress — `ICourseProgressService` / `CourseProgressService`
| Método | Descripción |
|---|---|
| `GetExamProgressAsync(int, int)` | Progreso hacia examen |
| `GetReadinessScoreAsync(int, int)` | Readiness score |
| `GetRecommendationsAsync(int, int)` | Recomendaciones |
| `GetReadinessHistoryAsync(int, int)` | Historial readiness |
| `GetReadinessTrendAsync(int, int)` | Tendencia readiness |
| `GetPredictionsAsync(int, int)` | Predicciones |
| `IdentifyWeaknessesAsync(int, int)` | Identificar debilidades |
| `GetPriorityTopicsAsync(int, int)` | Temas prioritarios |
| `SuggestFocusAsync(int, int)` | Sugerir enfoque |

### 14. Schedule — `IScheduleService` / `ScheduleService`
| Método | Descripción |
|---|---|
| `GetByIdAsync(int id)` | Obtener sesión |
| `GetByUserAsync(int userId)` | Sesiones del usuario |
| `GetByDateRangeAsync(int, DateTime, DateTime)` | Por rango fechas |
| `CreateAsync(int, CreateScheduleRequestDto)` | Crear sesión |
| `UpdateAsync(int, UpdateScheduleRequestDto)` | Actualizar sesión |
| `DeleteAsync(int id)` | Eliminar sesión |
| `GetMonthViewAsync(int, int, int)` | Vista mensual |
| `GetWeekViewAsync(int, int, int)` | Vista semanal |
| `GetDayViewAsync(int, DateTime)` | Vista diaria |
| `GetAgendaAsync(int, DateTime, DateTime)` | Agenda |
| `CreateRecurringAsync(int, CreateScheduleRequestDto, string)` | Crear recurrente |
| `UpdateRecurringAsync(int, UpdateScheduleRequestDto)` | Actualizar recurrente |
| `DeleteRecurringAsync(int scheduleId)` | Eliminar recurrente |
| `MarkCompleteAsync(int scheduleId)` | Marcar completada |
| `MarkIncompleteAsync(int scheduleId)` | Marcar incompleta |
| `BulkCompleteAsync(List<int>)` | Completar masivo |

### 15. Intervals — `IIntervalService` / `IntervalService`
| Método | Descripción |
|---|---|
| `CreateAsync(int, IntervalResponseDto)` | Crear intervalo |
| `UpdateAsync(int, IntervalResponseDto)` | Actualizar intervalo |
| `DeleteAsync(int intervalId)` | Eliminar intervalo |
| `ReorderAsync(int, List<int>)` | Reordenar intervalos |
| `GetActiveIntervalAsync(int scheduleId)` | Intervalo activo |
| `StartTimerAsync(int intervalId)` | Iniciar temporizador |
| `PauseTimerAsync(int intervalId)` | Pausar temporizador |
| `ResumeTimerAsync(int intervalId)` | Reanudar temporizador |
| `StopTimerAsync(int intervalId)` | Detener temporizador |
| `GetTemplatesAsync()` | Plantillas de intervalo |
| `CreateTemplateAsync(IntervalResponseDto)` | Crear plantilla |
| `DeleteTemplateAsync(int templateId)` | Eliminar plantilla |
| `ApplyTemplateAsync(int, int)` | Aplicar plantilla |

### 16. Schedule Content — `IScheduleContentService` / `ScheduleContentService`
| Método | Descripción |
|---|---|
| `AttachContentAsync(ScheduleContentRequestDto)` | Asignar contenido |
| `DetachContentAsync(int, int)` | Desasignar contenido |
| `ReorderContentAsync(int, List<int>)` | Reordenar contenido |
| `GetAssignedContentAsync(int scheduleId)` | Contenido asignado |
| `AutoAssignAsync(int, int)` | Auto-asignar |
| `PrioritizeByExamAsync(int, int, int)` | Priorizar por examen |
| `PrioritizeByWeaknessAsync(int, int, int)` | Priorizar por debilidad |
| `SuggestSessionAsync(int, int)` | Sugerir sesión |
| `SuggestContentAsync(int, int)` | Sugerir contenido |
| `OptimizeScheduleAsync(int userId)` | Optimizar cronograma |

### 17. Dashboard — `IDashboardService` / `DashboardService`
| Método | Descripción |
|---|---|
| `GetSummaryAsync(int userId)` | Resumen dashboard |
| `GetRecentActivityAsync(int, int)` | Actividad reciente |
| `GetUpcomingDeadlinesAsync(int, int)` | Próximas fechas |
| `GetStudyTimeAsync(int, string)` | Tiempo estudio |
| `GetCardsReviewedAsync(int, string)` | Tarjetas revisadas |
| `GetQuizzesCompletedAsync(int, string)` | Quizzes completados |
| `GetProgressChartAsync(int, int?, string)` | Gráfico progreso |
| `GetHeatmapDataAsync(int, int?)` | Heatmap |
| `GetDistributionDataAsync(int, int?)` | Distribución |
| `ExportToPdfAsync(int, ExportDashboardRequestDto)` | Exportar PDF (stub) |
| `ExportToCsvAsync(int, ExportDashboardRequestDto)` | Exportar CSV (stub) |
| `GenerateReportAsync(int, ExportDashboardRequestDto)` | Generar reporte (stub) |

### 18. Performance — `IPerformanceService` / `PerformanceService`
| Método | Descripción |
|---|---|
| `GetGlobalStatsAsync(int userId)` | Stats globales |
| `GetRankingAsync(int userId)` | Ranking (stub) |
| `GetAchievementsAsync(int userId)` | Logros (stub) |
| `GetWeeklyTrendAsync(int userId)` | Tendencia semanal |
| `GetMonthlyTrendAsync(int userId)` | Tendencia mensual |
| `GetYearlyReportAsync(int, int)` | Reporte anual |
| `SetGoalsAsync(int, SetGoalRequestDto)` | Fijar metas |
| `GetGoalsAsync(int userId)` | Obtener metas |
| `UpdateGoalProgressAsync(int, UpdateGoalProgressRequestDto)` | Actualizar progreso meta |
| `CheckAchievementAsync(int userId)` | Verificar logros |

### 19. Notifications — `INotificationService` / `NotificationService`
| Método | Descripción |
|---|---|
| `GetNotificationsAsync(int, bool?)` | Listar notificaciones |
| `GetNotificationAsync(int id)` | Obtener notificación |
| `MarkAsReadAsync(int id)` | Marcar como leída |
| `MarkAllAsReadAsync(int userId)` | Marcar todas leídas |
| `DeleteAsync(int id)` | Eliminar notificación |
| `CreateReminderAsync(ScheduleReminderRequestDto)` | Crear recordatorio |
| `GetPendingRemindersAsync()` | Recordatorios pendientes |
| `CancelReminderAsync(int notificationId)` | Cancelar recordatorio |
| `SendTestEmailAsync(int userId)` | Email test |
| `GetEmailLogsAsync()` | Logs de email |
| `RetryFailedEmailAsync(int emailLogId)` | Reintentar email |
| `RegisterPushDeviceAsync(int, RegisterPushDeviceRequestDto)` | Registrar push |
| `UnregisterPushDeviceAsync(int deviceId)` | Desregistrar push |
| `SendPushAsync(int, string, string)` | Enviar push |

### 20. System — `ISystemService` / `SystemService`
| Método | Descripción |
|---|---|
| `GetConfigsAsync()` | Configuraciones del sistema |
| `GetConfigAsync(string key)` | Config por key |
| `UpdateConfigAsync(int, UpdateSystemConfigRequestDto)` | Actualizar config |
| `ResetConfigAsync(string key)` | Resetear config |
| `HealthCheckAsync()` | Health check |
| `GetStatusAsync()` | Status del sistema |
| `GetMetricsAsync()` | Métricas |
| `ClearCacheAsync(string?)` | Limpiar caché |
| `GetCacheStatsAsync()` | Stats de caché |
| `WarmupCacheAsync()` | Warmup caché |
| `GetLogsAsync(GetLogsRequestDto)` | Obtener logs |
| `SearchLogsAsync(string query)` | Buscar logs |
| `ExportLogsAsync(GetLogsRequestDto)` | Exportar logs |

### 21. Reports — `IReportService` / `ReportService`
| Método | Descripción |
|---|---|
| `GenerateStudyReportAsync(int, DateTime, DateTime)` | Reporte estudio |
| `GetStudyInsightsAsync(int userId)` | Insights estudio |
| `GeneratePerformanceReportAsync(int, DateTime, DateTime)` | Reporte rendimiento |
| `GetPerformanceSummaryAsync(int userId)` | Resumen rendimiento |
| `CreateCustomReportAsync(int, CreateCustomReportRequestDto)` | Reporte personalizado |
| `SaveTemplateAsync(int, ReportTemplateResponseDto)` | Guardar plantilla |
| `GetTemplatesAsync(int userId)` | Plantillas guardadas |
| `ScheduleReportAsync(int, CreateCustomReportRequestDto)` | Programar reporte |

---

## Inventario Completo de Controladores

### AuthController — `api/auth`
| Método | Ruta | Auth |
|---|---|---|
| POST | `api/auth/register` | No |
| POST | `api/auth/login` | No |
| POST | `api/auth/logout` | JWT |
| POST | `api/auth/refresh` | No |
| POST | `api/auth/verify-email` | No |
| POST | `api/auth/resend-verification` | No |
| POST | `api/auth/forgot-password` | No |
| POST | `api/auth/reset-password` | No |
| POST | `api/auth/change-password` | JWT |

### UserController — `api/user`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/user/profile` | JWT |
| PUT | `api/user/profile` | JWT |
| POST | `api/user/avatar` | JWT |
| DELETE | `api/user/avatar` | JWT |
| GET | `api/user/preferences` | JWT |
| PUT | `api/user/preferences` | JWT |
| POST | `api/user/preferences/reset` | JWT |
| GET | `api/user/notification-settings` | JWT |
| PUT | `api/user/notification-settings` | JWT |
| POST | `api/user/notification-settings/test` | JWT |
| DELETE | `api/user/account` | JWT |
| POST | `api/user/export` | JWT |
| POST | `api/user/deactivate` | JWT |

### CourseController — `api/courses`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/courses` | JWT |
| GET | `api/courses/{id}` | JWT |
| POST | `api/courses` | JWT |
| PUT | `api/courses/{id}` | JWT |
| DELETE | `api/courses/{id}` | JWT |
| PATCH | `api/courses/{id}/archive` | JWT |
| PATCH | `api/courses/{id}/restore` | JWT |
| GET | `api/courses/{id}/exam` | JWT |
| PUT | `api/courses/{id}/exam` | JWT |
| DELETE | `api/courses/{id}/exam` | JWT |
| GET | `api/courses/{id}/members` | JWT |
| POST | `api/courses/{id}/members` | JWT |
| DELETE | `api/courses/{id}/members/{userId}` | JWT |
| PUT | `api/courses/{id}/members/{userId}/role` | JWT |
| GET | `api/courses/{id}/stats` | JWT |
| GET | `api/courses/search` | JWT |

### FilesController — `api/files`
| Método | Ruta | Auth |
|---|---|---|
| POST | `api/files/upload` | JWT |
| GET | `api/files/{id}/status` | JWT |
| GET | `api/files/history` | JWT |
| DELETE | `api/files/{id}` | JWT |
| POST | `api/files/{id}/process` | JWT |
| GET | `api/files/{id}/processing-status` | JWT |
| POST | `api/files/{id}/reprocess` | JWT |
| GET | `api/files/{id}/download` | JWT |
| GET | `api/files/{id}/stream` | JWT |

### DecksController — `api/decks`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/decks?courseId=` | JWT |
| GET | `api/decks/{id}` | JWT |
| POST | `api/decks` | JWT |
| PUT | `api/decks/{id}` | JWT |
| DELETE | `api/decks/{id}` | JWT |
| POST | `api/decks/{id}/duplicate` | JWT |
| GET | `api/decks/{id}/cards` | JWT |
| POST | `api/decks/{id}/cards` | JWT |
| DELETE | `api/decks/{id}/cards` | JWT |
| PUT | `api/decks/{id}/cards/reorder` | JWT |

### FlashcardsController — `api/flashcards`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/flashcards?deckId=` | JWT |
| GET | `api/flashcards/{id}` | JWT |
| POST | `api/flashcards` | JWT |
| PUT | `api/flashcards/{id}` | JWT |
| DELETE | `api/flashcards/{id}` | JWT |
| POST | `api/flashcards/bulk` | JWT |
| PUT | `api/flashcards/bulk` | JWT |
| GET | `api/flashcards/search?query=&deckId=` | JWT |
| POST | `api/flashcards/import/csv` | JWT |
| POST | `api/flashcards/import/json` | JWT |

### StudyController — `api/study`
| Método | Ruta | Auth |
|---|---|---|
| POST | `api/study/sessions` | JWT |
| GET | `api/study/sessions/{id}/next` | JWT |
| POST | `api/study/sessions/{id}/answer` | JWT |
| POST | `api/study/sessions/{id}/end` | JWT |
| GET | `api/study/decks/{deckId}/due` | JWT |
| GET | `api/study/decks/{deckId}/overdue` | JWT |
| POST | `api/study/reviews/schedule` | JWT |
| GET | `api/study/sessions?deckId=` | JWT |
| GET | `api/study/sessions/{id}` | JWT |
| GET | `api/study/sessions/{id}/summary` | JWT |
| GET | `api/study/decks/{deckId}/performance` | JWT |

### QuizzesController — `api/quizzes`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/quizzes?courseId=` | JWT |
| GET | `api/quizzes/{id}` | JWT |
| POST | `api/quizzes` | JWT |
| PUT | `api/quizzes/{id}` | JWT |
| DELETE | `api/quizzes/{id}` | JWT |
| POST | `api/quizzes/{id}/duplicate` | JWT |
| GET | `api/quizzes/{id}/questions` | JWT |
| POST | `api/quizzes/{id}/questions` | JWT |
| PUT | `api/quizzes/{id}/questions/{questionId}` | JWT |
| DELETE | `api/quizzes/{id}/questions/{questionId}` | JWT |
| PUT | `api/quizzes/{id}/questions/reorder` | JWT |
| POST | `api/quizzes/{id}/questions/{questionId}/options` | JWT |
| PUT | `api/quizzes/{id}/questions/{questionId}/options/{optionId}` | JWT |
| DELETE | `api/quizzes/{id}/questions/{questionId}/options/{optionId}` | JWT |
| GET | `api/quizzes/{id}/settings` | JWT |
| PUT | `api/quizzes/{id}/settings` | JWT |
| POST | `api/quizzes/{id}/settings/reset` | JWT |
| GET | `api/quizzes/{id}/preview` | JWT |
| POST | `api/quizzes/{id}/simulate` | JWT |

### QuizAttemptsController — `api/quiz-attempts`
| Método | Ruta | Auth |
|---|---|---|
| POST | `api/quiz-attempts/start` | JWT |
| POST | `api/quiz-attempts/{id}/submit` | JWT |
| GET | `api/quiz-attempts/{id}/result` | JWT |
| GET | `api/quiz-attempts?quizId=` | JWT |
| POST | `api/quiz-attempts/answer` | JWT |
| PUT | `api/quiz-attempts/answer` | JWT |
| POST | `api/quiz-attempts/answers/bulk` | JWT |
| POST | `api/quiz-attempts/{id}/grade` | JWT |
| POST | `api/quiz-attempts/{id}/regrade` | JWT |
| GET | `api/quiz-attempts/history?quizId=` | JWT |
| GET | `api/quiz-attempts/best?quizId=` | JWT |
| GET | `api/quiz-attempts/compare?ids=` | JWT |

### AiGenerationController — `api/ai`
| Método | Ruta | Auth |
|---|---|---|
| POST | `api/ai/generate/flashcards` | JWT |
| POST | `api/ai/generate/quiz` | JWT |
| GET | `api/ai/generate/{id}/status` | JWT |
| PUT | `api/ai/config` | JWT |
| GET | `api/ai/config` | JWT |
| POST | `api/ai/config/test` | JWT |
| POST | `api/ai/regenerate` | JWT |
| POST | `api/ai/improve` | JWT |
| POST | `api/ai/adjust-difficulty` | JWT |
| GET | `api/ai/history?fileId=` | JWT |
| GET | `api/ai/history/{id}` | JWT |
| DELETE | `api/ai/history/{id}` | JWT |

### FlashcardProgressController — `api/progress/flashcards`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/progress/flashcards/decks/{deckId}` | JWT |
| GET | `api/progress/flashcards/courses/{courseId}` | JWT |
| GET | `api/progress/flashcards` | JWT |
| GET | `api/progress/flashcards/decks/{deckId}/mastery` | JWT |
| GET | `api/progress/flashcards/decks/{deckId}/mastery/trend` | JWT |
| GET | `api/progress/flashcards/decks/{deckId}/predictions` | JWT |
| GET | `api/progress/flashcards/decks/{deckId}/timeline` | JWT |
| GET | `api/progress/flashcards/weekly` | JWT |
| GET | `api/progress/flashcards/monthly?month=&year=` | JWT |

### QuizProgressController — `api/progress/quizzes`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/progress/quizzes/{quizId}` | JWT |
| GET | `api/progress/quizzes/courses/{courseId}` | JWT |
| GET | `api/progress/quizzes` | JWT |
| GET | `api/progress/quizzes/average?quizId=` | JWT |
| GET | `api/progress/quizzes/courses/{courseId}/weak-topics` | JWT |
| GET | `api/progress/quizzes/improvement?quizId=` | JWT |
| GET | `api/progress/quizzes/compare?quizId1=&quizId2=` | JWT |
| GET | `api/progress/quizzes/compare-courses` | JWT |
| GET | `api/progress/quizzes/compare-timeframes` | JWT |

### CourseExamProgressController — `api/progress/exam`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/progress/exam/courses/{courseId}` | JWT |
| GET | `api/progress/exam/courses/{courseId}/readiness` | JWT |
| GET | `api/progress/exam/courses/{courseId}/recommendations` | JWT |
| GET | `api/progress/exam/courses/{courseId}/readiness/history` | JWT |
| GET | `api/progress/exam/courses/{courseId}/readiness/trend` | JWT |
| GET | `api/progress/exam/courses/{courseId}/predictions` | JWT |
| GET | `api/progress/exam/courses/{courseId}/weaknesses` | JWT |
| GET | `api/progress/exam/courses/{courseId}/weaknesses/priority` | JWT |
| GET | `api/progress/exam/courses/{courseId}/suggest-focus` | JWT |

### SchedulesController — `api/schedules`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/schedules` | JWT |
| GET | `api/schedules/range?from=&to=` | JWT |
| GET | `api/schedules/{id}` | JWT |
| POST | `api/schedules` | JWT |
| PUT | `api/schedules/{id}` | JWT |
| DELETE | `api/schedules/{id}` | JWT |
| GET | `api/schedules/calendar/month?year=&month=` | JWT |
| GET | `api/schedules/calendar/week?year=&week=` | JWT |
| GET | `api/schedules/calendar/day?date=` | JWT |
| GET | `api/schedules/calendar/agenda?from=&to=` | JWT |
| POST | `api/schedules/recurring` | JWT |
| PUT | `api/schedules/recurring/{id}` | JWT |
| DELETE | `api/schedules/recurring/{id}` | JWT |
| PATCH | `api/schedules/{id}/complete` | JWT |
| PATCH | `api/schedules/{id}/incomplete` | JWT |
| POST | `api/schedules/bulk-complete` | JWT |

### ScheduleContentsController — `api/schedules/{scheduleId}/contents`
| Método | Ruta | Auth |
|---|---|---|
| POST | `api/schedules/{scheduleId}/contents` | JWT |
| DELETE | `api/schedules/{scheduleId}/contents?contentId=` | JWT |
| PUT | `api/schedules/{scheduleId}/contents/reorder` | JWT |
| GET | `api/schedules/{scheduleId}/contents` | JWT |
| POST | `api/schedules/{scheduleId}/contents/auto-assign` | JWT |
| POST | `api/schedules/{scheduleId}/contents/prioritize-exam` | JWT |
| POST | `api/schedules/{scheduleId}/contents/prioritize-weakness` | JWT |
| GET | `api/schedules/{scheduleId}/contents/suggest-session` | JWT |
| GET | `api/schedules/{scheduleId}/contents/suggest-content` | JWT |
| GET | `api/schedules/{scheduleId}/contents/optimize` | JWT |

### DashboardController — `api/dashboard`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/dashboard/overview` | JWT |
| GET | `api/dashboard/activity?limit=` | JWT |
| GET | `api/dashboard/deadlines?days=` | JWT |
| GET | `api/dashboard/metrics/study-time?period=` | JWT |
| GET | `api/dashboard/metrics/cards-reviewed?period=` | JWT |
| GET | `api/dashboard/metrics/quizzes-completed?period=` | JWT |
| GET | `api/dashboard/charts/progress` | JWT |
| GET | `api/dashboard/charts/heatmap` | JWT |
| GET | `api/dashboard/charts/distribution` | JWT |
| GET | `api/dashboard/export/pdf` | JWT |
| GET | `api/dashboard/export/csv` | JWT |
| POST | `api/dashboard/export/report` | JWT |

### PerformanceController — `api/performance`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/performance/global` | JWT |
| GET | `api/performance/ranking` | JWT |
| GET | `api/performance/achievements` | JWT |
| GET | `api/performance/trends/weekly` | JWT |
| GET | `api/performance/trends/monthly` | JWT |
| GET | `api/performance/trends/yearly?year=` | JWT |
| POST | `api/performance/goals` | JWT |
| GET | `api/performance/goals` | JWT |
| PUT | `api/performance/goals/progress` | JWT |
| GET | `api/performance/goals/check` | JWT |

### NotificationsController — `api/notifications`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/notifications?unreadOnly=` | JWT |
| GET | `api/notifications/{id}` | JWT |
| PATCH | `api/notifications/{id}/read` | JWT |
| PATCH | `api/notifications/read-all` | JWT |
| DELETE | `api/notifications/{id}` | JWT |
| POST | `api/notifications/reminders` | JWT |
| GET | `api/notifications/reminders/pending` | JWT |
| DELETE | `api/notifications/reminders/{id}` | JWT |
| POST | `api/notifications/email/test` | JWT |
| GET | `api/notifications/email/logs` | JWT |
| POST | `api/notifications/email/retry/{id}` | JWT |
| POST | `api/notifications/push/register` | JWT |
| POST | `api/notifications/push/unregister` | JWT |
| POST | `api/notifications/push/send` | JWT |

### SystemController — `api/system`
| Método | Ruta | Auth |
|---|---|---|
| GET | `api/system/config` | JWT |
| GET | `api/system/config/{key}` | JWT |
| PUT | `api/system/config` | JWT |
| POST | `api/system/config/{key}/reset` | JWT |
| GET | `api/system/health` | JWT |
| GET | `api/system/status` | JWT |
| GET | `api/system/metrics` | JWT |
| POST | `api/system/cache/clear` | JWT |
| GET | `api/system/cache/stats` | JWT |
| POST | `api/system/cache/warmup` | JWT |
| GET | `api/system/logs` | JWT |
| GET | `api/system/logs/search` | JWT |
| GET | `api/system/logs/export` | JWT |

### ReportsController — `api/reports`
| Método | Ruta | Auth |
|---|---|---|
| POST | `api/reports/study` | JWT |
| GET | `api/reports/study/insights` | JWT |
| POST | `api/reports/performance` | JWT |
| GET | `api/reports/performance/summary` | JWT |
| POST | `api/reports/custom` | JWT |
| POST | `api/reports/templates` | JWT |
| GET | `api/reports/templates` | JWT |
| POST | `api/reports/schedule` | JWT |

---

## Inyección de Dependencias (Program.cs)

### Repositorios (13)
```csharp
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IUserCourseExamProgressRepository, UserCourseExamProgressRepository>();
builder.Services.AddScoped<IFlashcardRepository, FlashcardRepository>();
builder.Services.AddScoped<IFlashcardDeckRepository, FlashcardDeckRepository>();
builder.Services.AddScoped<IUserProgressFlashcardRepository, UserProgressFlashcardRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizAttemptRepository, QuizAttemptRepository>();
builder.Services.AddScoped<IUserProgressQuizRepository, UserProgressQuizRepository>();
builder.Services.AddScoped<IFileUploadRepository, FileUploadRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IStudyScheduleRepository, StudyScheduleRepository>();
builder.Services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
```

### Servicios BLL (21)
```csharp
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFlashcardDeckService, FlashcardDeckService>();
builder.Services.AddScoped<IFlashcardService, FlashcardService>();
builder.Services.AddScoped<IFlashcardStudyService, FlashcardStudyService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuizAttemptService, QuizAttemptService>();
builder.Services.AddScoped<IAiGenerationService, AiGenerationService>();
builder.Services.AddScoped<IFlashcardProgressService, FlashcardProgressService>();
builder.Services.AddScoped<IQuizProgressService, QuizProgressService>();
builder.Services.AddScoped<ICourseProgressService, CourseProgressService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IIntervalService, IntervalService>();
builder.Services.AddScoped<IScheduleContentService, ScheduleContentService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ISystemService, SystemService>();
builder.Services.AddScoped<IReportService, ReportService>();
```

---

## Middleware Pipeline (orden de ejecución)

```
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();         // Solo en Development
app.UseStaticFiles();              // wwwroot/assets/uploads/
app.UseCors("AllowAll");           // CORS abierto
app.UseAuthentication();           // JWT
app.UseAuthorization();
app.MapControllers();
```

---

## Base de Datos

- **Provider:** SQL Server
- **ConnectionString:** `appsettings.json` → `ConnectionStrings:DefaultConnection`
- **Auto-Migrate:** Al iniciar, con 10 reintentos para esperar a SQL Server
- **Migration inicial:** `InitialCreate` (20260605125923) con todas las tablas:

| Tabla | Descripción |
|---|---|
| `Users` | Usuarios del sistema |
| `Courses` | Cursos creados por usuarios |
| `UserCourses` | Relación usuario-curso (rol member/owner) |
| `UserCourseExamProgresses` | Progreso de curso hacia examen |
| `ExamReadinessScores` | Histórico de readiness scores |
| `FlashcardDecks` | Mazos de flashcards por curso |
| `Flashcards` | Tarjetas individuales por mazo |
| `FlashcardStudySessions` | Sesiones de estudio |
| `FlashcardReviews` | Reviews individuales (SM-2) |
| `SpacedRepetitionSettings` | Config SM-2 por usuario/mazo |
| `UserProgressFlashcards` | Progreso agregado por mazo |
| `Quizzes` | Quizzes por curso |
| `QuizQuestions` | Preguntas por quiz |
| `QuizOptions` | Opciones por pregunta |
| `QuizAttempts` | Intentos de quiz por usuario |
| `QuizAnswers` | Respuestas por intento |
| `UserProgressQuizzes` | Progreso agregado por quiz |
| `FileUploads` | Archivos subidos |
| `GeneratedContents` | Contenido generado por IA |
| `StudySchedules` | Sesiones de estudio programadas |
| `ScheduleIntervals` | Intervalos Pomodoro |
| `ScheduleContents` | Contenido asignado a sesiones |
| `Notifications` | Notificaciones del sistema |
| `SystemConfigurations` | Configuraciones del sistema |
| `ActivityLogs` | Logs de auditoría |

---

## Cómo Ejecutar

```bash
# 1. Restaurar paquetes
dotnet restore

# 2. Compilar
dotnet build

# 3. Ejecutar (auto-migra la BD al iniciar)
dotnet run --project Backend_PlanoriaCapstone

# 4. Swagger UI
# http://localhost:5232/swagger
# https://localhost:7075/swagger

# 5. Migrations (si se necesitan cambios)
cd PlanoriaCapstone.Dal
dotnet ef migrations add NombreMigration --startup-project ../Backend_PlanoriaCapstone
dotnet ef database update --startup-project ../Backend_PlanoriaCapstone
```

---

## Notas Importantes

1. **IDs:** Todos los IDs en modelos son `int`. Los DTOs fueron corregidos de `Guid` a `int` para mantener consistencia.
2. **Autenticación:** JWT Bearer. El userId se extrae del token via `User.ObtenerUserId()` extension method.
3. **Soft Delete:** Usuarios tienen `DeletedAt` nullable para soft delete.
4. **Archivos:** Se almacenan en `wwwroot/assets/uploads/{userId}/`.
5. **IA Generación:** Los métodos de IA son stubs que crean registros `GeneratedContent` pero no llaman a APIs externas. Pendiente integrar con Gemini/OpenAI.
6. **SM-2:** El algoritmo de repetición espaciada está implementado en `FlashcardStudyService` con cálculo de ease factor y next review date.
7. **Auditoría:** Todas las operaciones importantes se registran via `IActivityLogRepository.LogAsync()`.
