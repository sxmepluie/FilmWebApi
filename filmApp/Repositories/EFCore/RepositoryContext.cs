using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class RepositoryContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
    }

    public DbSet<Film> Films { get; set; }
    public DbSet<Film> WLFilms { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<WatchedFilm> WatchedFilms { get; set; }
    public DbSet<User> Users {  get; set; }  
    public DbSet<WatchList> WatchLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<User>()
        .HasKey(u => u.Id);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Film)
            .WithMany(f => f.Reviews)
            .HasForeignKey(r => r.FilmId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<WatchedFilm>()
            .HasOne(wf => wf.User)
            .WithMany() 
            .HasForeignKey(wf => wf.UserId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<WatchedFilm>()
            .HasOne(wf => wf.Film)
            .WithMany()
            .HasForeignKey(wf => wf.FilmId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WatchList>()
            .HasOne(wl => wl.User)
            .WithMany()
            .HasForeignKey(wl => wl.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WatchList>()
            .HasOne(wl => wl.Film)
            .WithMany()
            .HasForeignKey(wl => wl.FilmId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
