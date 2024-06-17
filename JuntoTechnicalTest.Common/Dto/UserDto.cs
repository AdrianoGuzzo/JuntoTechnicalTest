namespace JuntoTechnicalTest.Common.Dto
{
    public record CreateUserDto(string Name, string Email, string Password);
    public record ChangePasswordDto(string OldPassword, string NewPassword,string NewPasswordConfirmation);
}
