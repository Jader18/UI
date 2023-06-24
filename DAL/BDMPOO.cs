using EL;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace DAL
{
    public class BDMPOO : DbContext 
    {
        public BDMPOO() : base(Conexion.ConexionString(true)) { }
        public virtual DbSet<Formularios> Formularios { get; set; }
        public virtual DbSet<Formularios> Permisos { get; set; }
        public virtual DbSet<Formularios> Roles { get; set; }
        public virtual DbSet<Formularios> RolFormularios { get; set; }
        public virtual DbSet<Formularios> RolPermisos { get; set; }
        public virtual DbSet<Formularios> Usuarios { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Formularios>().Property(e => e.Formulario).IsUnicode(false);          
            modelBuilder.Entity<Permisos>().Property(e => e.Permiso).IsUnicode(false);
            modelBuilder.Entity<Roles>().Property(e => e.Rol).IsUnicode(false);
            modelBuilder.Entity<Usuarios>().Property(e => e.NombreCompleto).IsUnicode(false);
            modelBuilder.Entity<Usuarios>().Property(e => e.Correo).IsUnicode(false);
            modelBuilder.Entity<Usuarios>().Property(e => e.UserName).IsUnicode(false);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder); 

        }
    }
}
