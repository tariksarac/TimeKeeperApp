using System.Data.Entity.ModelConfiguration;

namespace timeBase.Mappers
{
    class DayMap : EntityTypeConfiguration<Day>
    {
        public DayMap()
        {
            this.ToTable("Days");
            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired();
            this.Property(p => p.Date).HasColumnName("Date").IsRequired();
            this.Property(p => p.Note).HasColumnName("Note");
            this.Property(p => p.Time).HasColumnName("Time");
            this.Property(p => p.Type).HasColumnName("Type").HasMaxLength(2);
        }
    }
}
