using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.DTOs
{
    public class EnrollmentDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Debe selecionar un estudiante")]
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una materia")]
        public Guid SubjectId { get; set; }

        public StudentDto Student { get; set; } = null!;

        public SubjectDto Subject { get; set; } = null!;

    }
}
