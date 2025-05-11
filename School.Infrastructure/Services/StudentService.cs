using Microsoft.EntityFrameworkCore;
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
    public class StudentService : IStudentService
    {
        private readonly SchoolDbContext _context;
        public StudentService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student?>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<OperationResult> CreateAsync(Student student)
        {
            bool exists = await _context.Students.AnyAsync(s => s.Documento == student.Documento);
            if (exists)
                return OperationResult.Fail("Ya existe un estudiante con ese documento.");

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return OperationResult.Ok("Estudiante registrado correctamente.");
        }

        public async Task<OperationResult> UpdateAsync(Student student)
        {
            bool exists = await _context.Students
            .AnyAsync(s => s.Id != student.Id && s.Documento == student.Documento);

            if (exists)
                return OperationResult.Fail("Otro estudiante ya tiene ese documento.");

            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return OperationResult.Ok("Estudiante actualizado correctamente.");

        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return OperationResult.Fail("Estudiante no encontrado.");

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return OperationResult.Ok("Estudiante eliminado correctamente.");

        }
    }
}
