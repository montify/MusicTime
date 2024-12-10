using Microsoft.JSInterop;

namespace MusicTimeClient
{
    public class LocalStorage
    {
        private IJSRuntime m_jsRuntime;

        public LocalStorage(IJSRuntime jsRuntim)
        {
            m_jsRuntime = jsRuntim;
        }

        public async Task AddItem(string key, string value)
        {
            await m_jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }

        public async Task<string> GetItem(string key)
        {
            return await m_jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }

        public async Task<bool> ContainsKey(string key)
        {
            if (await m_jsRuntime.InvokeAsync<string>("localStorage.getItem", key) == null)
            {
                return false;
            }
            return true;
        }

        public async Task RemoveItem(string key)
        {
            await m_jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}