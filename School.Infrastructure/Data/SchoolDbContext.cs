using Microsoft.EntityFrameworkCore;
using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.Data
{
    public class SchoolDbContext: DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options): base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Solo configuramos el ensamblaje de migraciones porque la cadena de conexión ya está configurada en otro lado
                optionsBuilder.UseSqlServer("your_connection_string", options =>
                {
                    options.MigrationsAssembly("School.Infrastructure");
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad Enrollment (relación muchos a muchos entre Student y Subject)
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Subject)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.SubjectId);

            // Evitar inscripciones duplicadas (relación única entre estudiante y materia)
            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.StudentId, e.SubjectId })
                .IsUnique();

            // Configuración de la entidad Student
            modelBuilder.Entity<Student>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Student>()
                .Property(s => s.FullName)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.Documento)
                .HasColumnType("varchar(11)")
                .HasMaxLength(11)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.Email)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired();

            // Configuración de la entidad Subject
            modelBuilder.Entity<Subject>()
               .HasKey(e => e.Id);

            modelBuilder.Entity<Subject>()
                .Property(s => s.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Subject>()
                .Property(s => s.Code)
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsRequired();

            modelBuilder.Entity<Subject>()
                .Property(s => s.Credits)
                .HasColumnType("smallint")
                .IsRequired();

        }


    }
}
