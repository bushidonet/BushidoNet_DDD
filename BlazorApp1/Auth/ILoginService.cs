namespace BlazorApp1;

public interface ILoginService
{
    Task Login(string token);
    Task Logout();
}