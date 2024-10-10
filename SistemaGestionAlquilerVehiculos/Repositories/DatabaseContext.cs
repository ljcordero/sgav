using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaGestionAlquilerVehiculos.Entities;

namespace SistemaGestionAlquilerVehiculos.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Alquiler> Alquileres { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Cliente> builderCliente = modelBuilder.Entity<Cliente>();
            builderCliente.HasKey(x => x.Id);
            builderCliente.Property(x => x.Nombre).IsRequired().HasMaxLength(200);
            builderCliente.Property(x => x.Apellido).IsRequired().HasMaxLength(200);
            builderCliente.Property(x => x.Cedula).IsRequired().HasMaxLength(50);
            builderCliente.Property(x => x.Telefono).IsRequired().HasMaxLength(20);
            builderCliente.Property(x => x.Direccion).HasMaxLength(200);
            builderCliente.Property(x => x.Compania).HasMaxLength(200);
            builderCliente.Property(x => x.Borrado).IsRequired().HasDefaultValue(false);
            builderCliente.HasIndex(x => x.Cedula).IsUnique();

            EntityTypeBuilder<Vehiculo> builderVehiculo = modelBuilder.Entity<Vehiculo>();
            builderVehiculo.HasKey(x => x.Id);
            builderVehiculo.Property(x => x.Marca).IsRequired().HasMaxLength(200);
            builderVehiculo.Property(x => x.Modelo).IsRequired().HasMaxLength(200);
            builderVehiculo.Property(x => x.Year).IsRequired();
            builderVehiculo.Property(x => x.Color).IsRequired().HasMaxLength(20);
            builderVehiculo.Property(x => x.Matricula).IsRequired().HasMaxLength(200);
            builderVehiculo.Property(x => x.CodigoGps).IsRequired().HasMaxLength(200);
            builderVehiculo.Property(x => x.PrecioAlquiler).IsRequired();
            builderVehiculo.Property(x => x.Estado).IsRequired().HasConversion<int>();
            builderCliente.Property(x => x.Borrado).IsRequired().HasDefaultValue(false);

            EntityTypeBuilder<Alquiler> builderAlquiler = modelBuilder.Entity<Alquiler>();
            builderAlquiler.HasKey(x => x.Id);
            builderAlquiler.Property(x => x.FechaInicio).IsRequired();
            builderAlquiler.Property(x => x.FechaFin).IsRequired();
            builderAlquiler.Property(x => x.Devuelto).IsRequired().HasDefaultValue(false);
            builderAlquiler.Property(x => x.PrecioAlquilerVehiculo).IsRequired();
            builderAlquiler.Property(x => x.PrecioTotal).IsRequired();
            builderCliente.Property(x => x.Borrado).IsRequired().HasDefaultValue(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
