using System.Data.Entity.ModelConfiguration;

namespace timeBase.Mappers
{
    class DetailMap : EntityTypeConfiguration<Detail>
    {
        public DetailMap()
        {
            this.ToTable("Details");
            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired();
            this.Property(p => p.Time).HasColumnName("Time");
            this.Property(p => p.Note).HasColumnName("Note");
            this.Property(p => p.Status).HasColumnName("Status").HasMaxLength(2);
        }
    }
}
