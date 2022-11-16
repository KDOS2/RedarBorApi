namespace RedarborDataBase.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RedarBorModels.Entities;

    internal class EmployeesMapping : IEntityTypeConfiguration<RedarBorEmployeesEntity>
    {
        public void Configure(EntityTypeBuilder<RedarBorEmployeesEntity> builder)
        {
            builder.ToTable("Employees", "Redarbor");
            builder.HasKey(e => e.id).HasName("PK_Employees");
            builder.Property(e => e.id).HasColumnName("EmployeeId");
            builder.Property(e => e.CompanyId).IsRequired();
            builder.Property(e => e.CreatedOn).IsRequired().HasDefaultValueSql("(getdate())");
            builder.Property(e => e.DeletedOn).IsRequired(false);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Fax).IsRequired(false).HasMaxLength(12);
            builder.Property(e => e.Name).IsRequired(false).HasMaxLength(255);
            builder.Property(e => e.Lastlogin).IsRequired(false);
            builder.Property(e => e.Username).IsRequired(false).HasMaxLength(255);
            builder.Property(e => e.Password).IsRequired(false).HasMaxLength(255);
            builder.Property(e => e.PortalId).IsRequired();
            builder.Property(e => e.RoleId).IsRequired();
            builder.Property(e => e.StatusId).IsRequired();
            builder.Property(e => e.Telephone).IsRequired(false).HasMaxLength(12);
            builder.Property(e => e.UpdatedOn).IsRequired(false);
    }
}
}
