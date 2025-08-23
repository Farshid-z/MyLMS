using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MyLMS.Controllers
{
    public abstract class MyLMSControllerBase : AbpController
    {
        protected MyLMSControllerBase()
        {
            LocalizationSourceName = MyLMSConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
