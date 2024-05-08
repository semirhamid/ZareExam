using AutoMapper;
using ZareExam.DTOs;
using ZareExam.Models;
using ZareExam.Models.DTO;
using ZareExam.Models.Entity;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.RegisteredDate, opt => opt.MapFrom(src => src.RegisteredDate))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        CreateMap<UserRegistrationRequestDto, AppUser>();
        CreateMap<Student, StudentReadDTO>();
        CreateMap<StudentCreateDTO, Student>();
        CreateMap<StudentUpdateDTO, Student>();
        CreateMap<Department, DepartmentReadDTO>()
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));
        CreateMap<DepartmentUpdateDTO, Department>();
        CreateMap<DepartmentCreateDTO, Department>();
    }
}
