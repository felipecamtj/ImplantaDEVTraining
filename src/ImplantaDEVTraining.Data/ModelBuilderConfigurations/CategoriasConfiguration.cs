using System.Data.Entity.ModelConfiguration;

namespace ImplantaDEVTraining.Data.ModelBuilderConfigurations
{
    public class CategoriasConfiguration: EntityTypeConfiguration<Categorias>
    {
        public CategoriasConfiguration()
        {
            ToTable("Categorias", "ImplantaDEVTraining");
            HasKey(c => c.Id);

            Property(c => c.Id)
                .HasColumnName("Id")
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            Property(c => c.Nome)
                .HasColumnName("Nome")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(250);

            Property(c => c.Ativo)
                .HasColumnName("Ativo")
                .HasColumnType("BIT")
                .IsRequired();
        }
    }
}
