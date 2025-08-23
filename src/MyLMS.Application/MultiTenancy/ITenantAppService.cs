using Abp.Application.Services;
using MyLMS.MultiTenancy.Dto;

namespace MyLMS.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

