using AutoMapper;
using School.Dtos.SubjectDtos;
using School.DTOs;
using School.Enums;
using School.Models;
using School.Repositories.Interfaces;
using School.Services.Interfaces;

namespace School.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;

        public SubjectService(
            ISubjectRepository subjectRepository,
            IAuditService auditService,
            IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _auditService = auditService;
            _mapper = mapper;
        }

        public async Task<List<SubjectResponseDto>> GetAllAsync()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            return _mapper.Map<List<SubjectResponseDto>>(subjects);
        }

        public async Task<SubjectResponseDto?> GetByIdAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            return subject == null ? null : _mapper.Map<SubjectResponseDto>(subject);
        }

        public async Task<SubjectResponseDto> CreateAsync(CreateSubjectDto dto, Guid performedBySecretaryId)
        {
            var nomeEmUso = await _subjectRepository.ExistsByNameAsync(dto.Name);
            if (nomeEmUso)
                throw new InvalidOperationException("Já existe uma disciplina cadastrada com este nome.");

            var subject = new Subject
            {
                Name = dto.Name
            };

            await _subjectRepository.AddAsync(subject);

            await _auditService.LogAsync(
                entityName: "Subject",
                entityId: subject.Id,
                action: Actions.Created,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Disciplina '{subject.Name}' foi criada."
            );

            return _mapper.Map<SubjectResponseDto>(subject);
        }

        public async Task<SubjectResponseDto> UpdateAsync(Guid id, UpdateSubjectDto dto, Guid performedBySecretaryId)
        {
            var subject = await _subjectRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Disciplina não encontrada.");

            if (!string.Equals(subject.Name, dto.Name, StringComparison.OrdinalIgnoreCase))
            {
                var nomeEmUso = await _subjectRepository.ExistsByNameAsync(dto.Name);
                if (nomeEmUso)
                    throw new InvalidOperationException("Já existe uma disciplina cadastrada com este nome.");
            }

            var oldName = subject.Name;
            subject.Name = dto.Name;

            await _subjectRepository.UpdateAsync(subject);

            await _auditService.LogAsync(
                entityName: "Subject",
                entityId: id,
                action: Actions.Updated,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Disciplina renomeada de '{oldName}' para '{subject.Name}'."
            );

            return _mapper.Map<SubjectResponseDto>(subject);
        }

        public async Task DeleteAsync(Guid id, Guid performedBySecretaryId)
        {
            var subject = await _subjectRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Disciplina não encontrada.");

            await _subjectRepository.DeleteAsync(id);

            await _auditService.LogAsync(
                entityName: "Subject",
                entityId: id,
                action: Actions.Deleted,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Disciplina '{subject.Name}' foi deletada."
            );
        }
    }
}