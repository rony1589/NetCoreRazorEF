using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Core.Entities;
using School.Application.DTOs;
using School.Core.Interface;
using School.Infrastructure.Data;
using AutoMapper;

namespace School.Web.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<CreateModel> _logger;
        private readonly IMapper _mapper;


        public CreateModel(IStudentService studentService, ILogger<CreateModel> logger, IMapper mapper)
        {
            _studentService = studentService;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StudentDto Student { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var student = _mapper.Map<Student>(Student);
                var result = await _studentService.CreateAsync(student);

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
                _logger.LogError(ex, "Error al crear el estudiante.");
                ModelState.AddModelError(string.Empty, $"Error al crear el estudiante: {ex.Message}");
                return Page();
            }


        }
    }
}
