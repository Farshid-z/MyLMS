using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyLMS.Authorization;

namespace MyLMS;

[DependsOn(
    typeof(MyLMSCoreModule),
    typeof(AbpAutoMapperModule))]
public class MyLMSApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<MyLMSAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(MyLMSApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
