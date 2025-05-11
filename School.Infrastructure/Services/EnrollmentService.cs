using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using School.Application.DTOs;
using School.Core.Entities;
using School.Core.Interface;
using School.Infrastructure.Data;
using School.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.Services
{
    public class EnrollmentService: IEnrollmentService
    {
        private readonly SchoolDbContext _context;
        public EnrollmentService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment?>> GetAllAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .Select(e => new Enrollment
                {
                    Id = e.Id,
                    StudentId = e.StudentId,
                    SubjectId = e.SubjectId,
                    Student = e.Student != null ? new Student
                    {
                        Id = e.Student.Id,
                        Documento = e.Student.Documento,
                        FullName = e.Student.FullName
                    } : null,
                    Subject = e.Subject != null ? new Subject
                    {
                        Id = e.Subject.Id,
                        Name = e.Subject.Name,
                        Credits = e.Subject.Credits
                    } : null
                }).ToListAsync();
        }

        public async Task<Enrollment?> GetByIdAsync(Guid id)
        {
            return await _context.Enrollments
            .Where(e => e.Id == id)
            .Select(e => new Enrollment
            {
                Id = e.Id,
                StudentId = e.StudentId,
                SubjectId = e.SubjectId,
                Student = e.Student != null ? new Student
                {
                    Id = e.Student.Id,
                    Documento = e.Student.Documento,
                    FullName = e.Student.FullName
                } : null,
                Subject = e.Subject != null ? new Subject
                {
                    Id = e.Subject.Id,
                    Name = e.Subject.Name,
                    Credits = e.Subject.Credits
                } : null
            })
            .FirstOrDefaultAsync();
        }

        public async Task<OperationResult> CreateAsync(Enrollment enrollment)
        {
            var exists = await _context.Enrollments
            .AnyAsync(e => e.StudentId == enrollment.StudentId && e.SubjectId == enrollment.SubjectId);

            if (exists)
                return OperationResult.Fail("El estudiante ya está inscrito en esta materia.");

            //Obtener la materia que se quiere inscribir
            var subject = await _context.Subjects.FindAsync(enrollment.SubjectId);
            if (subject == null)
                return OperationResult.Fail("La materia no existe.");

            //Obtener las materias actualmente inscritos por el estudiante
            var currentSubjects = await _context.Enrollments
                .Where(e => e.StudentId == enrollment.StudentId)
                .Join(
                    _context.Subjects,
                    e => e.SubjectId,
                    s => s.Id,
                    (e, s) => new { s.Credits }
                ).ToListAsync();

            // 4. Contar cuántas materias inscritas tienen >4 créditos
            int heavySubjectsCount = currentSubjects.Count(s => s.Credits >= 4);

            // 5. Validar:
            // - Si la nueva materia tiene >=4 créditos
            // - Y si ya tiene 3 materias con >=4 créditos
            if (subject.Credits >= 4 && heavySubjectsCount >= 3)
                return OperationResult.Fail("No se pueden inscribir más de 3 materias con más de 4 créditos.");

            // 6.Si pasa las validaciones, registrar la inscripción

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return OperationResult.Ok("Inscripción realizada correctamente.");

        }


        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
                return OperationResult.Fail("Inscripción no encontrada.");

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return OperationResult.Ok("Inscripción eliminada correctamente.");
        }
    }
}
