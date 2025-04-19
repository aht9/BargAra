using BargAra.Domain.AggregateModel.IdentityModels.Relations;

namespace BargAra.Domain.AggregateModel.IdentityModels.UserAggregate;

public class User : IdentityUser
{
    [MaxLength(100)]
    public string? FirstName { get; private set; }

    [MaxLength(100)]
    public string? LastName { get; private set; }
    
    public sealed override string? Email { get; set; }

    public bool IsActive { get; private set; } = true;
        

    public bool IsDeleted { get; private set; } = false;
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    
    private readonly List<UserCompanyRole> _userCompanyRoles = new();
    public IReadOnlyCollection<UserCompanyRole> UserCompanyRoles => _userCompanyRoles.AsReadOnly();
    
    private User() { } // EF
    
    public User(string? firstName, string? lastName, string? email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        IsActive = true;
        IsDeleted = false;
    }
    
    public void AddUserCompanyRole(UserCompanyRole userCompanyRole)
    {
        if (userCompanyRole == null)
            throw new ArgumentNullException(nameof(userCompanyRole));

        _userCompanyRoles.Add(userCompanyRole);
    }

    public void RemoveUserCompanyRole(UserCompanyRole userCompanyRole)
    {
        if (userCompanyRole == null)
            throw new ArgumentNullException(nameof(userCompanyRole));

        _userCompanyRoles.Remove(userCompanyRole);
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
    }
}