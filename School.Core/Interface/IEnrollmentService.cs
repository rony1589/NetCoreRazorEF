using School.Core.Entities;
using School.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Interface
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<Enrollment?>> GetAllAsync();
        Task<Enrollment?> GetByIdAsync(Guid id);
        Task<OperationResult> CreateAsync(Enrollment enrollment);
        Task<OperationResult>DeleteAsync(Guid id);

    }
}
