using System.Data.Entity.ModelConfiguration;

namespace timeBase.Mappers
{
    class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            this.ToTable("People");
            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired();
            this.Property(p => p.FirstName).HasColumnName("FirstName").HasMaxLength(40);
            this.Property(p => p.LastName).HasColumnName("LastName").HasMaxLength(40);
            this.Property(p => p.Email).HasColumnName("Email").IsMaxLength();
            this.Property(p => p.Phone).HasColumnName("Phone").HasMaxLength(20);
        }
    }
}
