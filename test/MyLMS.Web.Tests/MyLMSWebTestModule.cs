using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyLMS.EntityFrameworkCore;
using MyLMS.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace MyLMS.Web.Tests;

[DependsOn(
    typeof(MyLMSWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class MyLMSWebTestModule : AbpModule
{
    public MyLMSWebTestModule(MyLMSEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(MyLMSWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(MyLMSWebMvcModule).Assembly);
    }
}