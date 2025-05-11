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
    public class SubjectService: ISubjectService
    {
        private readonly SchoolDbContext _context;
        public SubjectService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject?>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }
        public async Task<Subject?> GetByIdAsync(Guid id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<OperationResult> CreateAsync(Subject subject)
        {
            bool nameExists = await _context.Subjects.AnyAsync(s => s.Name == subject.Name);
            if (nameExists)
                return OperationResult.Fail("Ya existe una materia con ese nombre.");

            bool codeExists = await _context.Subjects.AnyAsync(s => s.Code == subject.Code);
            if (codeExists)
                return OperationResult.Fail("Ya existe una materia con ese código.");

            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return OperationResult.Ok("Materia registrada correctamente.");
        }

        public async Task<OperationResult> UpdateAsync(Subject subject)
        {
            bool nameExists = await _context.Subjects
            .AnyAsync(s => s.Id != subject.Id && s.Name == subject.Name);
            if (nameExists)
                return OperationResult.Fail("Otra materia ya tiene ese nombre.");

            bool codeExists = await _context.Subjects
                .AnyAsync(s => s.Id != subject.Id && s.Code == subject.Code);
            if (codeExists)
                return OperationResult.Fail("Otra materia ya tiene ese código.");

            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
            return OperationResult.Ok("Materia actualizada correctamente.");
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                return OperationResult.Fail("Materia no encontrada.");

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return OperationResult.Ok("Materia eliminada correctamente.");
        }
    }
}
