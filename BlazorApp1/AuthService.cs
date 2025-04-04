// public class AuthService
// {
//     private readonly HttpClient _http;
//     private readonly CustomAuthenticationStateProvider _authStateProvider;
//
//     public AuthService(HttpClient http, CustomAuthenticationStateProvider authStateProvider)
//     {
//         _http = http;
//         _authStateProvider = authStateProvider;
//     }
//
//     public async Task<bool> Login(string email, string password)
//     {
//         var response = await _http.PostAsJsonAsync("http://localhost:5078/api/Auth/login", new { Email = email, Password = password });
//
//         if (!response.IsSuccessStatusCode)
//             return false;
//
//         var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
//          _authStateProvider.MarkUserAsAuthenticated(result.Token);
//
//         return true;
//     }
//
//     public void Logout()
//     {
//         _authStateProvider.MarkUserAsLoggedOut();
//     }
// }
//
// public class AuthResponse
// {
//     public string Token { get; set; }
// }