namespace PLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(string login, string password, string roleName);
        Task LoginAsync(string login, string password);
    }
}
