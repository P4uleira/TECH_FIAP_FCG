using FCG.Domain.DTO.Requests.LoginRequests;
using FCG.Domain.DTO.Responses.LoginResponses;

namespace FCG.Application.Interfaces
{
    public interface IAuthHandler
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
