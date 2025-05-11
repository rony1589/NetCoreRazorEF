using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.DTOs
{
    public class SubjectDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Nombre de la materia obligatorio")]
        [MaxLength(100, ErrorMessage = "El tamaño máximo son 100 caracteres")]
        [DisplayName("Nombre de la materia")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Código de la materia obligatorio")]
        [MaxLength(10, ErrorMessage = "El tamaño máximo son 10 caracteres")]
        [DisplayName("Código de la materia")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los créditos son obligatorios")]
        [Range(1, 6, ErrorMessage = "Los créditos deben estar entre 1 y 6")]
        [DisplayName("Créditos")]
        public int Credits { get; set; }
    }
}
