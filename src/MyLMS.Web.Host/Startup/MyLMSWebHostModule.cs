using Abp.Modules;
using Abp.Reflection.Extensions;
using MyLMS.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MyLMS.Web.Host.Startup
{
    [DependsOn(
       typeof(MyLMSWebCoreModule))]
    public class MyLMSWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MyLMSWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyLMSWebHostModule).GetAssembly());
        }
    }
}
