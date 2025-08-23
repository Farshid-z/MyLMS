using Abp.Application.Services;
using MyLMS.Sessions.Dto;
using System.Threading.Tasks;

namespace MyLMS.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
