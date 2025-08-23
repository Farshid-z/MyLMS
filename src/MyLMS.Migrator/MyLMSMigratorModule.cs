using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyLMS.Configuration;
using MyLMS.EntityFrameworkCore;
using MyLMS.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace MyLMS.Migrator;

[DependsOn(typeof(MyLMSEntityFrameworkModule))]
public class MyLMSMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public MyLMSMigratorModule(MyLMSEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(MyLMSMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            MyLMSConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(MyLMSMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
