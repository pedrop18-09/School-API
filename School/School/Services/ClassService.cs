using AutoMapper;
using School.Dtos.ClassDtos;
using School.DTOs;
using School.Enums;
using School.Models;
using School.Repositories.Interfaces;
using School.Services.Interfaces;

namespace School.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;

        public ClassService(
            IClassRepository classRepository,
            IAuditService auditService,
            IMapper mapper)
        {
            _classRepository = classRepository;
            _auditService = auditService;
            _mapper = mapper;
        }

        public async Task<List<ClassResponseDto>> GetAllAsync()
        {
            var classes = await _classRepository.GetAllAsync();
            return _mapper.Map<List<ClassResponseDto>>(classes);
        }

        public async Task<ClassResponseDto?> GetByIdAsync(Guid id)
        {
            var schoolClass = await _classRepository.GetByIdAsync(id);
            return schoolClass == null ? null : _mapper.Map<ClassResponseDto>(schoolClass);
        }

        public async Task<ClassResponseDto> CreateAsync(CreateClassDto dto, Guid performedBySecretaryId)
        {
            if (!Enum.TryParse<SchoolGrade>(dto.SchoolGrade, ignoreCase: true, out var schoolGrade))
                throw new ArgumentException($"Série inválida: '{dto.SchoolGrade}'.");

            var schoolClass = new Class
            {
                Name = dto.Name,
                SchoolGrade = schoolGrade,
                AcademicYear = dto.AcademicYear
            };

            await _classRepository.AddAsync(schoolClass);

            await _auditService.LogAsync(
                entityName: "Class",
                entityId: schoolClass.Id,
                action: Actions.Created,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Turma '{schoolClass.Name}' ({schoolGrade}, {schoolClass.AcademicYear}) foi criada."
            );

            return _mapper.Map<ClassResponseDto>(schoolClass);
        }

        public async Task<ClassResponseDto> UpdateAsync(Guid id, UpdateClassDto dto, Guid performedBySecretaryId)
        {
            var schoolClass = await _classRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Turma não encontrada.");

            if (!Enum.TryParse<SchoolGrade>(dto.SchoolGrade, ignoreCase: true, out var schoolGrade))
                throw new ArgumentException($"Série inválida: '{dto.SchoolGrade}'.");

            var oldName = schoolClass.Name;
            var oldGrade = schoolClass.SchoolGrade;
            var oldYear = schoolClass.AcademicYear;

            schoolClass.Name = dto.Name;
            schoolClass.SchoolGrade = schoolGrade;
            schoolClass.AcademicYear = dto.AcademicYear;

            await _classRepository.UpdateAsync(schoolClass);

            await _auditService.LogAsync(
                entityName: "Class",
                entityId: id,
                action: Actions.Updated,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Turma atualizada. '{oldName}' ({oldGrade}, {oldYear}) -> '{schoolClass.Name}' ({schoolGrade}, {schoolClass.AcademicYear})."
            );

            return _mapper.Map<ClassResponseDto>(schoolClass);
        }

        public async Task DeleteAsync(Guid id, Guid performedBySecretaryId)
        {
            var schoolClass = await _classRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Turma não encontrada.");

            await _classRepository.DeleteAsync(id);

            await _auditService.LogAsync(
                entityName: "Class",
                entityId: id,
                action: Actions.Deleted,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Turma '{schoolClass.Name}' foi deletada."
            );
        }
    }
}