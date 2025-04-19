using BargAra.Domain.SeedWork;

namespace BargAra.Domain.AggregateModel.IdentityModels.Relations;

public class CompanyRole : Entity
{
    public Guid CompanyId { get; private set; }
    public Guid RoleId { get; private set; }

    private CompanyRole() { } // EF

    public CompanyRole(Guid companyId, string roleId)
    {
        CompanyId = companyId;
        RoleId = new Guid(roleId);
    }
}