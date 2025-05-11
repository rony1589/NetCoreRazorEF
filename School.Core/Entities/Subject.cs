using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Credits { get; set; } 
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
