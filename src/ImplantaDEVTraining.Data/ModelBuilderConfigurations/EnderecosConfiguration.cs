using System.Data.Entity.ModelConfiguration;

namespace ImplantaDEVTraining.Data.ModelBuilderConfigurations
{
    public class EnderecosConfiguration: EntityTypeConfiguration<Enderecos>
    {
        public EnderecosConfiguration()
        {
            ToTable("Enderecos", "ImplantaDEVTraining");
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName("Id")
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            Property(p => p.IdProfissional)
                .HasColumnName("IdProfissional")
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            Property(p => p.CEP)
                .HasColumnName("CEP")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(15);

            Property(p => p.Logradouro)
                .HasColumnName("Logradouro")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(300);

            Property(p => p.Bairro)
                .HasColumnName("Bairro")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(250);

            Property(p => p.Cidade)
                .HasColumnName("Cidade")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(250);

            Property(p => p.Numero)
                .HasColumnName("Numero")
                .HasColumnType("VARCHAR")
                .IsOptional()
                .HasMaxLength(20);

            HasRequired(e => e.Profissional)
                .WithMany(p => p.Enderecos)
                .HasForeignKey(e => e.IdProfissional)
                .WillCascadeOnDelete(true);
        }
    }
}
