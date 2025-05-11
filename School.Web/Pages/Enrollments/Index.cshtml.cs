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
    public class IndexModel : PageModel
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ILogger<IndexModel> _logger;
        private readonly IMapper _mapper;

        public IndexModel(IEnrollmentService enrollmentService, ILogger<IndexModel> logger, IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _logger = logger;
            _mapper = mapper;
        }

        public IList<EnrollmentDto> Enrollments { get;set; } = default!;

        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                var enrollment = await _enrollmentService.GetAllAsync();
                Enrollments = _mapper.Map<List<EnrollmentDto>>(enrollment);
                return Page();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, $"Error al cargar la lista de los materias registradas a los estudiantes: {ex.Message}");
                return Page();
            }

        }
    }
}
