using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Entities
{
    public class Enrollment
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid SubjectId { get; set; }
        public Student? Student { get; set; }
        public Subject? Subject { get; set; }
    }
}
