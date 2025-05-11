using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using School.Application.DTOs;
using School.Core.Entities;
using School.Core.Interface;
using School.Infrastructure.Data;
using School.Infrastructure.Services;

namespace School.Web.Pages.Enrollments
{
    public class DeleteModel : PageModel
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ILogger<IndexModel> _logger;
        private readonly IMapper _mapper;

        public DeleteModel(IEnrollmentService enrollmentService, ILogger<IndexModel> logger, IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _logger = logger;
            _mapper = mapper;
        }

        [BindProperty]
        public EnrollmentDto Enrollment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var enrollment = await _enrollmentService.GetByIdAsync(id);

            if (enrollment == null)
            {
                TempData["OperationResult"] = "Registro no encontrado.";
                return RedirectToPage("Index");
            }

            Enrollment = _mapper.Map<EnrollmentDto>(enrollment);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {
                var result = await _enrollmentService.DeleteAsync(id);
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
                ModelState.AddModelError(string.Empty, $"Error al cargar el estudiante: {ex.Message}");
                return Page();
            }
        }
    }
}
