using AutoMapper;
using School.Dtos.ClassDtos;
using School.Dtos.ClassSubjectDtos;
using School.Dtos.GradeDtos;
using School.Dtos.SecretaryDtos;
using School.Dtos.StudentDtos;
using School.Dtos.SubjectDtos;
using School.Dtos.TeacherDtos;
using School.DTOs;
using School.Models;

namespace School.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ===================== SECRETARY =====================
            CreateMap<Secretary, SecretaryResponseDto>();

            // ===================== TEACHER =====================
            CreateMap<CreateTeacherDto, Teacher>();
            CreateMap<Teacher, TeacherResponseDto>();

            // ===================== STUDENT =====================
            CreateMap<CreateStudentDto, Student>();
            CreateMap<Student, StudentResponseDto>()
                .ForMember(dest => dest.ClassName,
                           opt => opt.MapFrom(src => src.Class.Name));

            // ===================== CLASS =====================
            CreateMap<CreateClassDto, Class>();
            CreateMap<Class, ClassResponseDto>()
                .ForMember(dest => dest.StudentCount,
                           opt => opt.MapFrom(src => src.Students.Count));

            // ===================== SUBJECT =====================
            CreateMap<CreateSubjectDto, Subject>();
            CreateMap<Subject, SubjectResponseDto>();

            // ===================== CLASSSUBJECT =====================
            CreateMap<CreateClassSubjectDto, ClassSubject>();
            CreateMap<ClassSubject, ClassSubjectResponseDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.Name));

            // ===================== GRADE =====================
            CreateMap<CreateGradeDto, Grade>();
            CreateMap<Grade, GradeResponseDto>()
                .ForMember(dest => dest.SubjectName,
                           opt => opt.MapFrom(src => src.ClassSubject.Subject.Name))
                .ForMember(dest => dest.Average,
                           opt => opt.MapFrom(src => (src.Exam1 + src.Exam2 + src.Assignment) / 3));

            // ===================== AUDITLOG =====================
            CreateMap<AuditLog, AuditLogResponseDto>()
                .ForMember(dest => dest.Action,
                    opt => opt.MapFrom(src => src.Actions.ToString()))
                .ForMember(dest => dest.PerformedBySecretaryName,
                    opt => opt.MapFrom(src => src.PerformedBySecretary.Name))
                .ForMember(dest => dest.Timestamp,
                    opt => opt.MapFrom(src => src.TimeStamp));
        }
    }
}