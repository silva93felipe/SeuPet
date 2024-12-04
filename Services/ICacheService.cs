namespace SeuPet.Services
{
    public interface ICacheService {

        Task SetStringAsync(string key, string objeto);
        Task<string> GetStringAsync(string key);
        Task RemoveAsync(string key);
    }
}