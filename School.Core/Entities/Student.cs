using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
