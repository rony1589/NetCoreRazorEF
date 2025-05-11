using School.Core.Entities;
using School.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<Student?>> GetAllAsync();
        Task<Student?> GetByIdAsync(Guid id);
        Task<OperationResult> CreateAsync(Student student);
        Task<OperationResult> UpdateAsync(Student student);
        Task<OperationResult> DeleteAsync(Guid id);
    }
}
