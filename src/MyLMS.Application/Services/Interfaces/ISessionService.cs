using Abp.Application.Services;
using MyLMS.DTOs;
using MyLMS.Enums;
using System.Threading.Tasks;

namespace MyLMS.Services.Interfaces
{
    public interface ISessionService : IApplicationService
    {
        Task<int> CreateSession(CreateSessionDto createSessionDto);
        Task<DeleteSessionResult> DeleteSession(int sessionId);
    }
}
