using NewApi.Models;

namespace NewApi.Services
{
    public interface ITokenService
    {
        string GerarToken(string key, string issuer, string audience , UserModel user);
    }
}
