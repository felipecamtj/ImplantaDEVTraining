using ImplantaDEVTraining.Data.ModelBuilderConfigurations;
using System.Data.Entity;

namespace ImplantaDEVTraining.Data
{
    public class ImplantaDEVTrainingDbContext: DbContext
    {
        public ImplantaDEVTrainingDbContext() 
            : base("Name=ImplantaDEVTraining")
        {

        }

        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Profissionais> Profissionais { get; set; }
        public DbSet<Enderecos> Enderecos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoriasConfiguration());
            modelBuilder.Configurations.Add(new ProfissionaisConfiguration());
            modelBuilder.Configurations.Add(new EnderecosConfiguration());
        }
    }
}
