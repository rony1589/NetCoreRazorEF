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

namespace School.Web.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<IndexModel> _logger;
        private readonly IMapper _mapper;

        public IndexModel(IStudentService studentService, ILogger<IndexModel> logger, IMapper mapper)
        {
            _studentService = studentService;
            _logger = logger;
            _mapper = mapper;
        }

        public IList<StudentDto> Students { get;set; } = default!;

        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                var students = await _studentService.GetAllAsync();
                Students = _mapper.Map<List<StudentDto>>(students);
                return Page();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, $"Error al cargar la lista de los estudiantes: {ex.Message}");
                return Page();
            }
            
        }
    }
}
