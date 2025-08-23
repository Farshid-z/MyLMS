using MyLMS.Configuration.Dto;
using System.Threading.Tasks;

namespace MyLMS.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
