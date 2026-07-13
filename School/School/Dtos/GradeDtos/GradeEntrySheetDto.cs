namespace School.Dtos.GradeDtos
{
    public class GradeEntrySheetDto
    {
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string Quarter { get; set; }
        public List<GradeEntryDto> Students { get; set; }
    }
}
