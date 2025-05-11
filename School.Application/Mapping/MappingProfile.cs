using AutoMapper;
using School.Application.DTOs;
using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile() { 

        //Student
        CreateMap<Student, StudentDto>().ReverseMap();

        //Subject
        CreateMap<Subject, SubjectDto>().ReverseMap();

        //Enrollment
        CreateMap<Enrollment, EnrollmentDto>().ReverseMap()
            .ForMember(dest => dest.Student, opt => opt.Ignore())
            .ForMember(dest => dest.Subject, opt => opt.Ignore());

        }
    }
}
