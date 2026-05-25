using PlanoriaCapstone.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Dal
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<ArchivoSubido> ArchivosSubidos { get; set; }

        public DbSet<AnalisisIA> AnalisisIA { get; set; }

        public DbSet<Flashcard> Flashcards { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<ProgresoFlashcard> ProgresoFlashcards { get; set; }

        public DbSet<ProgresoQuiz> ProgresoQuizzes { get; set; }

        public DbSet<PreguntaQuiz> PreguntasQuiz { get; set; }

        public DbSet<HistorialFlashcard> HistorialFlashcards { get; set; }

        public DbSet<HistorialQuiz> HistorialQuizzes { get; set; }

        public DbSet<ProgresoArchivo> ProgresoArchivos { get; set; }

        public DbSet<RachaUsuario> RachasUsuario { get; set; }

        public DbSet<EstadisticaIA> EstadisticasIA { get; set; }

        public DbSet<Auditoria> Auditorias { get; set; }

        public DbSet<Curso> Cursos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================================
            // ROL
            // =========================================

            modelBuilder.Entity<Rol>()
                .HasKey(r => r.IdRol);

            modelBuilder.Entity<Rol>()
                .Property(r => r.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            // =========================================
            // USUARIO
            // =========================================

            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Apellido)
                .HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Correo)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.PasswordHash)
                .HasMaxLength(255);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.GoogleId)
                .HasMaxLength(255);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Provider)
                .HasMaxLength(20)
                .HasDefaultValue("LOCAL");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.FotoPerfilUrl)
                .HasMaxLength(1000);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.RachaDias)
                .HasDefaultValue(0);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Puntos)
                .HasDefaultValue(0);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nivel)
                .HasDefaultValue(1);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================================
            // ARCHIVOS SUBIDOS
            // =========================================

            modelBuilder.Entity<ArchivoSubido>()
    .HasKey(a => a.IdArchivo);

            modelBuilder.Entity<ArchivoSubido>()
                .Property(a => a.NombreArchivo)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<ArchivoSubido>()
                .Property(a => a.UrlArchivo)
                .HasMaxLength(1000)
                .IsRequired();

            modelBuilder.Entity<ArchivoSubido>()
                .Property(a => a.TipoArchivo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<ArchivoSubido>()
                .Property(a => a.Estado)
                .HasMaxLength(50)
                .HasDefaultValue("ACTIVO");

            modelBuilder.Entity<ArchivoSubido>()
                .Property(a => a.TamanoMB)
                .HasPrecision(10, 2);

            // Relación Archivo -> Usuario
            modelBuilder.Entity<ArchivoSubido>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.ArchivosSubidos)
                .HasForeignKey(a => a.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Archivo -> Curso
            modelBuilder.Entity<ArchivoSubido>()
                .HasOne(a => a.Curso)
                .WithMany(c => c.Archivos)
                .HasForeignKey(a => a.IdCursos)
                .OnDelete(DeleteBehavior.NoAction);

            // =========================================
            // ANALISIS IA
            // =========================================

            modelBuilder.Entity<AnalisisIA>()
                .HasKey(a => a.IdAnalisis);

            modelBuilder.Entity<AnalisisIA>()
                .Property(a => a.EstadoProceso)
                .HasMaxLength(50)
                .HasDefaultValue("PROCESANDO");

            modelBuilder.Entity<AnalisisIA>()
                .HasOne(a => a.ArchivoSubido)
                .WithMany(a => a.AnalisisIA)
                .HasForeignKey(a => a.IdArchivo)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================
            // FLASHCARD
            // =========================================

            modelBuilder.Entity<Flashcard>()
                .HasKey(f => f.IdFlashcard);

            modelBuilder.Entity<Flashcard>()
                .Property(f => f.NivelDificultad)
                .HasMaxLength(20);

            modelBuilder.Entity<Flashcard>()
                .Property(f => f.VecesEstudiada)
                .HasDefaultValue(0);

            modelBuilder.Entity<Flashcard>()
                .HasOne(f => f.AnalisisIA)
                .WithMany(a => a.Flashcards)
                .HasForeignKey(f => f.IdAnalisis)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================
            // QUIZ
            // =========================================

            modelBuilder.Entity<Quiz>()
                .HasKey(q => q.IdQuiz);

            modelBuilder.Entity<Quiz>()
                .Property(q => q.Titulo)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.AnalisisIA)
                .WithMany(a => a.Quizzes)
                .HasForeignKey(q => q.IdAnalisis)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================
            // PREGUNTA QUIZ
            // =========================================

            modelBuilder.Entity<PreguntaQuiz>()
                .HasKey(p => p.IdPreguntaQuiz);

            modelBuilder.Entity<PreguntaQuiz>()
                .Property(p => p.OpcionA)
                .HasMaxLength(300);

            modelBuilder.Entity<PreguntaQuiz>()
                .Property(p => p.OpcionB)
                .HasMaxLength(300);

            modelBuilder.Entity<PreguntaQuiz>()
                .Property(p => p.OpcionC)
                .HasMaxLength(300);

            modelBuilder.Entity<PreguntaQuiz>()
                .Property(p => p.OpcionD)
                .HasMaxLength(300);

            modelBuilder.Entity<PreguntaQuiz>()
                .HasOne(p => p.Quiz)
                .WithMany(q => q.PreguntasQuiz)
                .HasForeignKey(p => p.IdQuiz)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================
            // HISTORIAL FLASHCARD
            // =========================================

            modelBuilder.Entity<HistorialFlashcard>()
                .HasKey(h => h.IdHistorialFlashcard);

            modelBuilder.Entity<HistorialFlashcard>()
                .HasOne(h => h.Usuario)
                .WithMany(u => u.HistorialFlashcards)
                .HasForeignKey(h => h.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HistorialFlashcard>()
                .HasOne(h => h.Flashcard)
                .WithMany(f => f.HistorialFlashcards)
                .HasForeignKey(h => h.IdFlashcard)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================
            // HISTORIAL QUIZ
            // =========================================

            modelBuilder.Entity<HistorialQuiz>()
                .HasKey(h => h.IdHistorialQuiz);

            modelBuilder.Entity<HistorialQuiz>()
                .Property(h => h.Puntaje)
                .HasPrecision(5, 2);

            modelBuilder.Entity<HistorialQuiz>()
                .HasOne(h => h.Usuario)
                .WithMany(u => u.HistorialQuizzes)
                .HasForeignKey(h => h.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HistorialQuiz>()
                .HasOne(h => h.Quiz)
                .WithMany(q => q.HistorialQuizzes)
                .HasForeignKey(h => h.IdQuiz)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================
            // PROGRESO ARCHIVO
            // =========================================

            modelBuilder.Entity<ProgresoArchivo>()
                .HasKey(p => p.IdProgresoArchivo);

            modelBuilder.Entity<ProgresoArchivo>()
                .Property(p => p.PorcentajeProgreso)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ProgresoArchivo>()
                .Property(p => p.Completado)
                .HasDefaultValue(false);

            modelBuilder.Entity<ProgresoArchivo>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.ProgresoArchivos)
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProgresoArchivo>()
                .HasOne(p => p.ArchivoSubido)
                .WithMany()
                .HasForeignKey(p => p.IdArchivo)
                .OnDelete(DeleteBehavior.Cascade);

            // =====================================
            // PROGRESO FLASHCARD
            // =====================================

            modelBuilder.Entity<ProgresoFlashcard>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgresoFlashcard>()
                .HasOne(p => p.Flashcard)
                .WithMany()
                .HasForeignKey(p => p.IdFlashcard)
                .OnDelete(DeleteBehavior.Cascade);

            // =====================================
            // PROGRESO QUIZ
            // =====================================

            modelBuilder.Entity<ProgresoQuiz>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgresoQuiz>()
                .HasOne(p => p.Quiz)
                .WithMany()
                .HasForeignKey(p => p.IdQuiz)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================
            // RACHA USUARIO
            // =========================================

            modelBuilder.Entity<RachaUsuario>()
                .HasKey(r => r.IdRacha);

            modelBuilder.Entity<RachaUsuario>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.RachasUsuario)
                .HasForeignKey(r => r.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================
            // ESTADISTICA IA
            // =========================================

            modelBuilder.Entity<EstadisticaIA>()
                .HasKey(e => e.IdEstadistica);

            modelBuilder.Entity<EstadisticaIA>()
                .Property(e => e.PromedioPuntaje)
                .HasPrecision(5, 2);

            modelBuilder.Entity<EstadisticaIA>()
                .HasOne(e => e.Usuario)
                .WithOne(u => u.EstadisticaIA)
                .HasForeignKey<EstadisticaIA>(e => e.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================
            // AUDITORIA
            // =========================================

            modelBuilder.Entity<Auditoria>()
                .HasKey(a => a.IdAuditoria);

            modelBuilder.Entity<Auditoria>()
                .Property(a => a.Accion)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Auditoria>()
                .Property(a => a.Descripcion)
                .HasMaxLength(500);

            modelBuilder.Entity<Auditoria>()
                .Property(a => a.IpAddress)
                .HasMaxLength(100);

            modelBuilder.Entity<Auditoria>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Auditorias)
                .HasForeignKey(a => a.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull);

            // =========================================
            // CURSO
            // =========================================

            modelBuilder.Entity<Curso>()
                .HasKey(c => c.IdCursos);

            modelBuilder.Entity<Curso>()
                .Property(c => c.Nombre)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Curso>()
                .Property(c => c.Descripcion)
                .HasMaxLength(1000);

            modelBuilder.Entity<Curso>()
                .Property(c => c.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            // Relación Curso -> Usuario
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Cursos)
                .HasForeignKey(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================================
            // SEED ROLES
            // =========================================

            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    IdRol = 1,
                    Nombre = "ADMIN"
                },
                new Rol
                {
                    IdRol = 2,
                    Nombre = "USER"
                }
            );
        }
    }
}
