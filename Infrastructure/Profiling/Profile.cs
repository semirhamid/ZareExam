using AutoMapper;
using ZareExam.DTOs;
using ZareExam.Models;
using ZareExam.Models.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentReadDTO>();
        CreateMap<StudentCreateDTO, Student>();
        CreateMap<Department, DepartmentReadDTO>()
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));
        CreateMap<DepartmentCreateDTO, Department>();
    }
}