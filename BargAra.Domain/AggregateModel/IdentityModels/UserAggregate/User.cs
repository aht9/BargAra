namespace BargAra.Domain.AggregateModel.IdentityModels.UserAggregate;

public class User : IdentityUser
{
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }
    
    public override string? Email { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}