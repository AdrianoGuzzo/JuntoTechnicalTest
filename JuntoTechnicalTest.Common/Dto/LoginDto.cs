namespace JuntoTechnicalTest.Common.Dto
{
    public record LoginDto(string Email, string Password);
    public record CreateUserDto(string Name, string Email, string Password);

}
