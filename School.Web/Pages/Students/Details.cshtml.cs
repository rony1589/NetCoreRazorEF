using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using School.Core.Entities;
using School.Core.Interface;
using School.Infrastructure.Data;

namespace School.Web.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<DetailsModel> _logger;
        private readonly IMapper _mapper;


        public DetailsModel(IStudentService studentService, ILogger<DetailsModel> logger, IMapper mapper)
        {
            _studentService = studentService;
            _logger = logger;
            _mapper = mapper;
        }

        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var student = await _studentService.GetByIdAsync(id);
                if (student == null)
                {
                    TempData["OperationResult"] = "Estudiante no encontrado.";
                    return RedirectToPage("Index");
                }

                Student = _mapper.Map<Student>(student);

                return Page();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, $"Error al cargar el detalle del estudiante: {ex.Message}");
                return Page();
            }
           
        }
    }
}
