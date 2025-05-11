using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Application.DTOs;
using School.Core.Entities;
using School.Core.Interface;
using School.Infrastructure.Data;

namespace School.Web.Pages.Enrollments
{
    public class CreateModel : PageModel
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;
        private readonly ILogger<CreateModel> _logger;
        private readonly IMapper _mapper;

        public CreateModel(IEnrollmentService enrollmentService, IStudentService studentService, ISubjectService subjectService, ILogger<CreateModel> logger, IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _subjectService = subjectService;
            _logger = logger;
            _mapper = mapper;
        }

        [BindProperty]
        public EnrollmentDto Enrollment { get; set; } = default!;

        public SelectList Students { get; set; } = null!;
        public SelectList Subjects { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            
            await LoadSelectListAsync();
            return Page();

        }

        private async Task LoadSelectListAsync()
        {
            try
            {
                var students = await _studentService.GetAllAsync();
                var subjects = await _subjectService.GetAllAsync();

                Students = new SelectList(students, "Id", "FullName");
                var subjectItems = subjects.Select(s => new
                {
                    Id = s.Id,
                    DisplayText = $"{s.Name} - Créditos: {s.Credits}"
                });

                Subjects = new SelectList(subjectItems, "Id", "DisplayText");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error al carga la lista de Estudiante o Materias.");
                ModelState.AddModelError(string.Empty, $"Error al carga la lista de Estudiante o Materias: {ex.Message}");
            }
           
        }

        public async Task<IActionResult> OnPostAsync()
        {


            if (!ModelState.IsValid)
            {
                await LoadSelectListAsync();
            }

            try
            {
                
                var enrollment = new Enrollment { 
                    Id = Enrollment.Id ,
                    StudentId = Enrollment.StudentId ,
                    SubjectId = Enrollment.SubjectId 
                };
      
                var result = await _enrollmentService.CreateAsync(enrollment);

                if (!result.Success)
                {
                    ModelState.AddModelError(string.Empty, result.Message ?? "Ha ocurrido un error.");
                    return Page();
                }

                TempData["OperationResult"] = result.Message;

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la incripción de la materia.");
                ModelState.AddModelError(string.Empty, $"Error al crear la incripción de la materia: {ex.Message}");
                return Page();
            }
        }
    }
}
