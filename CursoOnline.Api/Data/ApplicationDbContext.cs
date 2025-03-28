namespace CursoOnline.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        }

        public DbSet<Curso> Cursos { get; set; }
        // Adicionar outras entidades depois
}
