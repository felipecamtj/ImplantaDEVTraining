using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplantaDEVTraining.Data.ModelBuilderConfigurations
{
    public class ProfissionaisConfiguration: EntityTypeConfiguration<Profissionais>
    {
        public ProfissionaisConfiguration()
        {
            ToTable("Profissionais", "ImplantaDEVTraining");
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName("Id")
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(250);

            Property(p => p.Ativo)
                .HasColumnName("Ativo")
                .HasColumnType("BIT")
                .IsRequired();

            Property(p => p.DataNascimento)
                .HasColumnName(@"DataNascimento")
                .HasColumnType("DATETIME2")
                .IsOptional();

            Property(p => p.CPF)
                .HasColumnName("CPF")
                .HasColumnType("VARCHAR")
                .HasMaxLength(15)
                .IsOptional();

            Property(p => p.RG)
                .HasColumnName("RG")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20)
                .IsOptional();

            Property(p => p.Observacoes)
                .HasColumnName("Observacoes")
                .HasColumnType("VARCHAR")
                .IsOptional();

            Property(p => p.IdCategoria)
                .HasColumnName("IdCategoria")
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            HasRequired(p => p.Categoria)
                .WithMany(c => c.Profissionais)
                .HasForeignKey(p => p.IdCategoria)
                .WillCascadeOnDelete(false);
        }
    }
}
