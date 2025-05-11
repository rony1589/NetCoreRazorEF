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
    public class DetailsModel : PageModel
    {
        private readonly ISubjectService _subjectService;
        private readonly ILogger<DetailsModel> _logger;
        private readonly IMapper _mapper;

        public DetailsModel(ISubjectService subjectService, ILogger<DetailsModel> logger, IMapper mapper)
        {
            _subjectService = subjectService;
            _logger = logger;
            _mapper = mapper;
        }

        public SubjectDto Subject { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
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
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, $"Error al cargar el detalle de la materia: {ex.Message}");
                return Page();
            }
        }
    }
}
