using System.Data.Entity.ModelConfiguration;

namespace timeBase.Mappers
{
    class TeamMap : EntityTypeConfiguration<Team>
    {
        public TeamMap()
        {
            this.ToTable("Teams");
            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired();
            this.Property(p => p.Name).HasColumnName("Name").HasMaxLength(40);
        }
    }
}
