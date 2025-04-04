using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using BlazorApp1.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace BlazorApp1;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider, ILoginService
{
     private readonly IJSRuntime _jsRuntime;
     private readonly HttpClient _httpClient;

    public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, HttpClient httpClient)
    {
        _jsRuntime = jsRuntime;
        _httpClient = httpClient;
    }

    public static readonly string TOKENKEY = "tokenkey";
    
    private AuthenticationState Anonimus => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
        
            var token = await _jsRuntime.GetLocalStorage(TOKENKEY);

        if (token is null) return Anonimus;
        
        return BuildAuthenticationState(token.ToString());
        }
        catch (InvalidOperationException)
        {
            // ⚠️ Si aún no se puede acceder a JavaScript, devolver un usuario anónimo
            return Anonimus;
        }
        

        // var paco = new ClaimsIdentity();
        // var userFilipo = new ClaimsIdentity(
        //     new List<Claim>
        //     {
        //         new Claim(ClaimTypes.Name, "Paco"),
        //         new Claim("Edad", "19"),
        //         new Claim("llave1", "valor1"),
        //     }, "Paco");
        // return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(paco)));
    }
    private AuthenticationState BuildAuthenticationState(string token)
    {
        //Incluye el TOKEN en el encabezado para las peticiones HTTP:
        //Esto permite al servidor que verifique la identidad del usuario.
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("bearer", token);
        
        //Extrae los CLAIMS del JWT:
        var claims = ParseClaimsJWT(token);
        
        //Devuelve el estado de la autentificacion:
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
    }

    //Extrae los CLAIMS del TOKEN
    private IEnumerable<Claim> ParseClaimsJWT(string token)
    {
        token = token.Trim().Trim('"'); // Limpia el token
        
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        
        
        var deserialiceToken = jwtSecurityTokenHandler.ReadJwtToken(token);
        return deserialiceToken.Claims;
    }
    
    public async Task Login(string token)
    {
        //Guardamos el token en LocalStorage
        await _jsRuntime.SaveLocalStorage(TOKENKEY, token);
        
        //Construir el estado de la Autentificacion.
        var authState = BuildAuthenticationState(token);
        
        //Notificar si la autentificacion ha cambiado
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task Logout()
    {
        //Eliminar el token del local storage
        await _jsRuntime.RemoveLocalStorage(TOKENKEY);
        //Remover el token del http client
        _httpClient.DefaultRequestHeaders.Authorization = null;
        
        //Notificar si la autentificacion ha cambiado
        NotifyAuthenticationStateChanged(Task.FromResult(Anonimus));
    }


    // public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    // {
    //     // Intenta obtener el token del localStorage
    //     var token = await _localStorage.GetItemAsync<string>("authToken");
    //
    //     var identity = string.IsNullOrEmpty(token)
    //         ? new ClaimsIdentity()
    //         : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
    //
    //     var user = new ClaimsPrincipal(identity);
    //     return new AuthenticationState(user);
    // }
    //
    // public void MarkUserAsAuthenticated(string token)
    // {
    //     var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
    //     var user = new ClaimsPrincipal(identity);
    //     var authenticationState = new AuthenticationState(user);
    //     NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
    // }
    //
    // public void MarkUserAsLoggedOut()
    // {
    //     var identity = new ClaimsIdentity();
    //     var user = new ClaimsPrincipal(identity);
    //     var authenticationState = new AuthenticationState(user);
    //     NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
    // }
    // private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    // {
    //     // Divide el JWT en sus tres partes: header, payload, signature
    //     var parts = jwt.Split('.');
    //     if (parts.Length != 3)
    //         throw new ArgumentException("El JWT no tiene el formato esperado.");
    //
    //     // Obtener el payload
    //     var payload = parts[1];
    //
    //     // Decodificar Base64Url a Base64 estándar
    //     var base64 = Base64UrlDecode(payload);
    //
    //     // Convertir los bytes a una cadena JSON
    //     var jsonString = Encoding.UTF8.GetString(base64);
    //     Dictionary<string, object> keyValuePairs = 
    //         JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
    //
    //     // Mapear las claves y valores del diccionario a Claims
    //     return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    // }
    //
    // private byte[] Base64UrlDecode(string base64Url)
    // {
    //     // Reemplazar los caracteres específicos de Base64Url
    //     var base64 = base64Url
    //         .Replace('-', '+')  // Reemplazar '-' por '+'
    //         .Replace('_', '/');  // Reemplazar '_' por '/'
    //
    //     // Agregar los posibles caracteres de relleno '='
    //     switch (base64.Length % 4)
    //     {
    //         case 2: base64 += "=="; break;
    //         case 3: base64 += "="; break;
    //     }
    //
    //     // Decodificar Base64 estándar a bytes
    //     return Convert.FromBase64String(base64);
    // }
    
}