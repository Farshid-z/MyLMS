using Abp.Authorization;
using MyLMS.Authorization.Roles;
using MyLMS.Authorization.Users;

namespace MyLMS.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
