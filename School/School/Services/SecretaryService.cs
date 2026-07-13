using AutoMapper;
using School.Dtos.SecretaryDtos;
using School.DTOs;
using School.Enums;
using School.Repositories.Interfaces;
using School.Services.Interfaces;

namespace School.Services
{
    public class SecretaryService : ISecretaryService
    {
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;

        public SecretaryService(
            ISecretaryRepository secretaryRepository,
            IAuditService auditService,
            IMapper mapper)
        {
            _secretaryRepository = secretaryRepository;
            _auditService = auditService;
            _mapper = mapper;
        }

        public async Task<List<SecretaryResponseDto>> GetAllAsync()
        {
            var secretaries = await _secretaryRepository.GetAllAsync();
            return _mapper.Map<List<SecretaryResponseDto>>(secretaries);
        }

        public async Task<SecretaryResponseDto?> GetByIdAsync(Guid id)
        {
            var secretary = await _secretaryRepository.GetByIdAsync(id);
            return secretary == null ? null : _mapper.Map<SecretaryResponseDto>(secretary);
        }

        public async Task<SecretaryResponseDto> UpdateAsync(Guid id, UpdateSecretaryDto dto, Guid performedBySecretaryId)
        {
            var secretary = await _secretaryRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Secretário não encontrado.");

            // Se o e-mail está sendo alterado, confere se o novo já não está em uso por outro secretário
            if (!string.Equals(secretary.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var emailEmUso = await _secretaryRepository.ExistsByEmailAsync(dto.Email);
                if (emailEmUso)
                    throw new InvalidOperationException("Este e-mail já está em uso por outro secretário.");
            }

            var oldName = secretary.Name;
            var oldEmail = secretary.Email;

            secretary.Name = dto.Name;
            secretary.Email = dto.Email;

            await _secretaryRepository.UpdateAsync(secretary);

            await _auditService.LogAsync(
                entityName: "Secretary",
                entityId: secretary.Id,
                action: Actions.Updated,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Secretário atualizado. Nome: '{oldName}' -> '{secretary.Name}', Email: '{oldEmail}' -> '{secretary.Email}'."
            );

            return _mapper.Map<SecretaryResponseDto>(secretary);
        }

        public async Task DeleteAsync(Guid id, Guid performedBySecretaryId)
        {
            var secretary = await _secretaryRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Secretário não encontrado.");

            await _secretaryRepository.DeleteAsync(id);

            await _auditService.LogAsync(
                entityName: "Secretary",
                entityId: secretary.Id,
                action: Actions.Deleted,
                performedBySecretaryId: performedBySecretaryId,
                details: $"Secretário '{secretary.Name}' (Email: {secretary.Email}) foi deletado."
            );
        }
    }
}