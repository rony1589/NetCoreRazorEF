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

namespace School.Web.Pages.Subjects
{
    public class IndexModel : PageModel
    {
        private readonly ISubjectService _subjectService;
        private readonly ILogger<IndexModel> _logger;
        private readonly IMapper _mapper;

        public IndexModel(ISubjectService subjectService, ILogger<IndexModel> logger, IMapper mapper)
        {
            _subjectService = subjectService;
            _logger = logger;
            _mapper = mapper;
        }

        public IList<SubjectDto> Subjects { get; set; } = default!;

        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                var subject = await _subjectService.GetAllAsync();
                Subjects = _mapper.Map<List<SubjectDto>>(subject);
                return Page();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, $"Error al cargar la lista de los Materias: {ex.Message}");
                return Page();
            }

        }
    }
}
