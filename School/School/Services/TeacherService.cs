using AutoMapper;
using School.Dtos.TeacherDtos;
using School.DTOs;
using School.Enums;
using School.Models;
using School.Repositories.Interfaces;
using School.Services.Interfaces;

namespace School.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;

        public TeacherService(
            ITeacherRepository teacherRepository,
            IAuditService auditService,
            IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _auditService = auditService;
            _mapper = mapper;
        }

        public async Task<List<TeacherResponseDto>> GetAllAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return _mapper.Map<List<TeacherResponseDto>>(teachers);
        }

        public async Task<TeacherResponseDto?> GetByIdAsync(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            return teacher == null ? null : _mapper.Map<TeacherResponseDto>(teacher);
        }

        public async Task<List<TeacherResponseDto>> GetInactiveAsync()
        {
            var teachers = await _teacherRepository.GetInactiveAsync();
            return _mapper.Map<List<TeacherResponseDto>>(teachers);
        }

        public async Task<TeacherResponseDto> ReactivateAsync(Guid id, Guid performedBySecretaryId)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Professor não encontrado.");

            if (teacher.IsActive)
                throw new InvalidOperationException("Este professor já está ativo.");

            teacher.IsActive = true;
            await _teacherRepository.UpdateAsync(teacher);

            await _auditService.LogAsync(
                entityName: "Teacher",
                entityId: teacher.Id,
                action: Actions.Updated,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Professor '{teacher.Name}' foi reativado."
            );

            return _mapper.Map<TeacherResponseDto>(teacher);
        }

        public async Task<TeacherResponseDto> CreateAsync(CreateTeacherDto dto, Guid performedBySecretaryId)
        {
            var cpfEmUso = await _teacherRepository.ExistsByCpfAsync(dto.Cpf);
            if (cpfEmUso)
                throw new InvalidOperationException("Já existe um(a) professor(a) cadastrado com este CPF.");

            var teacher = _mapper.Map<Teacher>(dto);
            teacher.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _teacherRepository.AddAsync(teacher);

            await _auditService.LogAsync(
                entityName: "Teacher",
                entityId: teacher.Id,
                action: Actions.Created,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Professor(a) '{teacher.Name}' (CPF: {teacher.Cpf}) foi cadastrado(a)."
            );

            return _mapper.Map<TeacherResponseDto>(teacher);
        }

        public async Task<TeacherResponseDto> UpdateAsync(Guid id, UpdateTeacherDto dto, Guid performedBySecretaryId)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Professor(a) não encontrado(a).");

            var oldName = teacher.Name;

            teacher.Name = dto.Name;

            await _teacherRepository.UpdateAsync(teacher);

            await _auditService.LogAsync(
                entityName: "Teacher",
                entityId: teacher.Id,
                action: Actions.Updated,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Professor(a) atualizado(a). Nome: '{oldName}' -> '{teacher.Name}'."
            );

            return _mapper.Map<TeacherResponseDto>(teacher);
        }

        public async Task DeleteAsync(Guid id, Guid performedBySecretaryId)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Professor não encontrado.");

            if (!teacher.IsActive)
                throw new InvalidOperationException("Este professor já está desativado.");

            teacher.IsActive = false;
            await _teacherRepository.UpdateAsync(teacher); // note: UpdateAsync, não mais DeleteAsync do repository

            await _auditService.LogAsync(
                entityName: "Teacher",
                entityId: teacher.Id,
                action: Actions.Updated,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Professor '{teacher.Name}' foi desativado (removido da lista ativa)."
            );
        }
    }
}