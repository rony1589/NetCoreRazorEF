using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using School.Core.Entities;
using School.Application.DTOs;
using School.Core.Interface;
using School.Infrastructure.Data;

namespace School.Web.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<DeleteModel> _logger;
        private readonly IMapper _mapper;

        public DeleteModel(IStudentService studentService, ILogger<DeleteModel> logger, IMapper mapper)
        {
            _studentService = studentService;
            _logger = logger;
            _mapper = mapper;
        }

        [BindProperty]
        public StudentDto Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            
            var student = await _studentService.GetByIdAsync(id);

            if (student == null)
            {
                TempData["OperationResult"] = "Estudiante no encontrado.";
                return RedirectToPage("Index");
            }
            
            Student = _mapper.Map<StudentDto>(student);
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {
                var result = await _studentService.DeleteAsync(id);
                if (!result.Success) {
                    ModelState.AddModelError(string.Empty, result.Message?? "Ha ocurrido un error.");
                    return Page();
                }
                TempData["OperationResult"] = result.Message;
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al cargar el estudiante: {ex.Message}");
                return Page();
            }

            
        }
    }
}
