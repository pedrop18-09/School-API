namespace School.Dtos.ClassSubjectDtos
{
    public class UpdateClassSubjectDto
    {
        // Normalmente só faz sentido trocar o professor responsável;
        // trocar Class/Subject seria melhor apagar e criar de novo
        public Guid TeacherId { get; set; }

    }
}
