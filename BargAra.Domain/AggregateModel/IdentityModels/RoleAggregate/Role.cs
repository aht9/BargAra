namespace BargAra.Domain.AggregateModel.RoleAggregate;

public class Role : IdentityRole
{
    public Role(string name, string displayName) : base(name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentException("Display name cannot be empty.", nameof(displayName));

        DisplayName = displayName;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }

    public bool IsDeleted { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public string DisplayName { get; private set; }

    internal long CompanyId { get; private set; } // EF mapping only
    
    public void UpdateDisplayName(string newDisplayName)
    {
        if (string.IsNullOrWhiteSpace(newDisplayName))
            throw new ArgumentException("Display name cannot be empty.", nameof(newDisplayName));

        DisplayName = newDisplayName;
    }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
    }

    public void Restore()
    {
        IsDeleted = false;
    }
}