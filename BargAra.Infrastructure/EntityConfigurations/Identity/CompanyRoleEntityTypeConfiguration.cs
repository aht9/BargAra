namespace BargAra.Infrastructure.EntityConfigurations.Identity;

public class CompanyRoleEntityTypeConfiguration : IEntityTypeConfiguration<CompanyRole>
{
    public void Configure(EntityTypeBuilder<CompanyRole> builder)
    {
        builder
            .HasKey(cr => new { cr.CompanyId, cr.RoleId });

        builder
            .HasOne<Company>()
            .WithMany(c => c.CompanyRoles)
            .HasForeignKey(cr => cr.CompanyId);

        builder
            .HasOne<Role>()
            .WithMany()
            .HasForeignKey(cr => cr.RoleId);

    }
}