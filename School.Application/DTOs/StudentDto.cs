using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(150, ErrorMessage = "El tamaño máximo son 150 caracteres")]
        [DisplayName("Nombre completo")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El documento es obligatorio")]
        [MaxLength(11, ErrorMessage = "El tamaño máximo son 11 caracteres")]
        [DisplayName("Documento de identidad")]
        public string Documento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        [MaxLength(150, ErrorMessage = "El tamaño máximo son 150 caracteres")]
        [DisplayName("Correo electrónico")]
        public string Email { get; set; } = string.Empty;
    }
}
