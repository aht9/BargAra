using BargAra.Domain.SeedWork;

namespace BargAra.Domain.AggregateModel.IdentityModels.Relations;

public class UserCompanyRole : Entity
{
    public Guid UserId { get; private set; }
    public Guid? RoleId { get; private set; }
    public Guid? CompanyRoleId { get; private set; }

    private UserCompanyRole()
    {
    } // EF

    public UserCompanyRole(Guid userId, Guid? roleId, Guid? companyRoleId)
    {
        UserId = userId;
        SetRole(roleId, companyRoleId);
    }

    public void SetRole(Guid? roleId, Guid? companyRoleId)
    {
        if (roleId.HasValue)
        {
            RoleId = roleId;
        }else if (companyRoleId.HasValue)
        {
            CompanyRoleId = roleId;
        }
        else
        {
            throw new ArgumentException("Either roleId or companyRoleId must be provided.");
        }
    }
}