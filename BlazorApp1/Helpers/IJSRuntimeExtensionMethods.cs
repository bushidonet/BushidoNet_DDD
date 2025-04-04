using Microsoft.JSInterop;

namespace BlazorApp1.Helpers;

public static class IJSRuntimeExtensionMethods
{
    public static ValueTask<object> SaveLocalStorage(this IJSRuntime js, string key, string content)
    {
        return js.InvokeAsync<object>("localStorage.setItem", key, content);
    }
    
    public static ValueTask<object> GetLocalStorage(this IJSRuntime js, string key)
    {
        return js.InvokeAsync<object>("localStorage.getItem", key);
    }
    
    public static ValueTask<object> RemoveLocalStorage(this IJSRuntime js, string key)
    {
        return js.InvokeAsync<object>("localStorage.removeItem", key);
    }
}