using BargAra.Domain.AggregateModel.IdentityModels.CompanyAggregate;

namespace BargAra.Infrastructure.EntityConfigurations.Identity;

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasOne<Company>()
            .WithMany(c=>c.Roles)
    }
}