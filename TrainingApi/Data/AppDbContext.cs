using Microsoft.EntityFrameworkCore;

namespace TrainingApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<VideoLibrary> VideoLibraries { get; set; }
        public DbSet<ClientExercise> ClientExercises { get; set; }
        public DbSet<ClientWorkout> ClientWorkouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
