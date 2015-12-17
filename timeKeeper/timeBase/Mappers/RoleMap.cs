using System.Data.Entity.ModelConfiguration;

namespace timeBase.Mappers
{
    class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.ToTable("Roles");
            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired();
            this.Property(p => p.Name).HasColumnName("Name").HasMaxLength(40);
        }
    }
}
