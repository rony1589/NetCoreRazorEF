using School.Core.Entities;
using School.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Interface
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject?>> GetAllAsync();
        Task<Subject?> GetByIdAsync(Guid id);
        Task<OperationResult> CreateAsync(Subject subject);
        Task<OperationResult> UpdateAsync(Subject subject);
        Task<OperationResult> DeleteAsync(Guid id);
    }
}
