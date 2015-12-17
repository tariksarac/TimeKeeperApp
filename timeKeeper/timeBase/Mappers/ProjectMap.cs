using System.Data.Entity.ModelConfiguration;

namespace timeBase.Mappers
{
    class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            this.ToTable("Projects");
            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired();
            this.Property(p => p.Name).HasColumnName("Name").HasMaxLength(40);
        }
    }
}
