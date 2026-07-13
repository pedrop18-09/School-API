namespace School.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Guid id, string name, string role);
    }
}
