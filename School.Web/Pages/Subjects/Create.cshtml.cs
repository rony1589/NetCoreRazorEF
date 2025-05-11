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
using School.Infrastructure.Services;

namespace School.Web.Pages.Subjects
{
    public class CreateModel : PageModel
    {
        private readonly ISubjectService _subjectService;
        private readonly ILogger<CreateModel> _logger;
        private readonly IMapper _mapper;

        public CreateModel(ISubjectService subjectService, ILogger<CreateModel> logger, IMapper mapper)
        {
            _subjectService = subjectService;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SubjectDto Subject { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var subject = _mapper.Map<Subject>(Subject);
                var result = await _subjectService.CreateAsync(subject);

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
                _logger.LogError(ex, "Error al crear la materia.");
                ModelState.AddModelError(string.Empty, $"Error al crear la materia: {ex.Message}");
                return Page();
            }
        }
    }
}
