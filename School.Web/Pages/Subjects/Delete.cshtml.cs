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

namespace School.Web.Pages.Subjects
{
    public class DeleteModel : PageModel
    {
        private readonly ISubjectService _subjectService;
        private readonly ILogger<DeleteModel> _logger;
        private readonly IMapper _mapper;

        public DeleteModel(ISubjectService subjectService, ILogger<DeleteModel> logger, IMapper mapper)
        {
            _subjectService = subjectService;
            _logger = logger;
            _mapper = mapper;
        }

        [BindProperty]
        public SubjectDto Subject { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var subject = await _subjectService.GetByIdAsync(id);

            if (subject == null)
            {
                TempData["OperationResult"] = "Materia no encontrada.";
                return RedirectToPage("Index");
            }

            Subject = _mapper.Map<SubjectDto>(subject);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {
                var result = await _subjectService.DeleteAsync(id);
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
                ModelState.AddModelError(string.Empty, $"Error al cargar la materia: {ex.Message}");
                return Page();
            }
        }
    }
}
