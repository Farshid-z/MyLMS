using Abp.Authorization;
using Abp.Runtime.Session;
using MyLMS.Configuration.Dto;
using System.Threading.Tasks;

namespace MyLMS.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : MyLMSAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
