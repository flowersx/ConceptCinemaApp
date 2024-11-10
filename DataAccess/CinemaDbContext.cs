using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class CinemaDbContext(DbContextOptions<CinemaDbContext> options) : DbContext(options)
{
    // DbSets
    public DbSet<CinemaHall> CinemaHalls { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<FoodAndBeverage> FoodAndBeverages { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // 1. CinemaHall and Seat (One-to-Many)
    modelBuilder.Entity<Seat>()
        .HasOne(s => s.CinemaHall)
        .WithMany(h => h.Seats)
        .HasForeignKey(s => s.CinemaHallId)
        .OnDelete(DeleteBehavior.Cascade);

    // 2. Movie and Showtime (One-to-Many)
    modelBuilder.Entity<Showtime>()
        .HasOne(st => st.Movie)
        .WithMany(m => m.Showtimes)
        .HasForeignKey(st => st.MovieId)
        .OnDelete(DeleteBehavior.Cascade);

    // 3. CinemaHall and Showtime (One-to-Many)
    modelBuilder.Entity<Showtime>()
        .HasOne(st => st.CinemaHall)
        .WithMany(h => h.Showtimes)
        .HasForeignKey(st => st.CinemaHallId)
        .OnDelete(DeleteBehavior.Cascade);

    // 4. Showtime and Ticket (One-to-Many)
    modelBuilder.Entity<Ticket>()
        .HasOne(t => t.Showtime)
        .WithMany(st => st.Tickets)
        .HasForeignKey(t => t.ShowtimeId)
        .OnDelete(DeleteBehavior.Cascade);

    // 5. Seat and Ticket (One-to-Many)
    modelBuilder.Entity<Ticket>()
        .HasOne(t => t.Seat)
        .WithMany(s => s.Tickets)
        .HasForeignKey(t => t.SeatId)
        .OnDelete(DeleteBehavior.Restrict);  // Ensures tickets remain even if seats are modified

    // 6. User and Ticket (One-to-Many)
    modelBuilder.Entity<Ticket>()
        .HasOne(t => t.User)
        .WithMany(u => u.Tickets)
        .HasForeignKey(t => t.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // 7. Ticket and Package (Optional: Many-to-One)
    modelBuilder.Entity<Ticket>()
        .HasOne(t => t.Package)
        .WithMany(p => p.Tickets)
        .HasForeignKey(t => t.PackageId)
        .OnDelete(DeleteBehavior.SetNull);  // Optional relationship

    // 8. User and Transaction (One-to-Many)
    modelBuilder.Entity<Transaction>()
        .HasOne(tr => tr.User)
        .WithMany(u => u.Transactions)
        .HasForeignKey(tr => tr.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Composite Key Configuration for Ticket
    modelBuilder.Entity<Ticket>()
        .HasKey(t => new { t.UserId, t.SeatId, t.ShowtimeId });  // Prevents duplicate ticket entries

    base.OnModelCreating(modelBuilder);
    }
}

// Entities

public class CinemaHall
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TotalSeats { get; set; }

    // Navigation properties
    public ICollection<Seat> Seats { get; set; }
    public ICollection<Showtime> Showtimes { get; set; } 
}

public class Seat
{
    public int Id { get; set; }
    public int CinemaHallId { get; set; }
    public string Row { get; set; }
    public int Number { get; set; }
    public bool IsAvailable { get; set; }

    // Navigation properties
    public CinemaHall CinemaHall { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<Showtime> Showtimes { get; set; }
}

public class Showtime
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public int MovieId { get; set; }
    public int CinemaHallId { get; set; }

    // Navigation properties
    public Movie Movie { get; set; }
    public CinemaHall CinemaHall { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}

public class Ticket
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SeatId { get; set; }
    public int ShowtimeId { get; set; }
    public int? PackageId { get; set; }

    // Navigation properties
    public User User { get; set; }
    public Seat Seat { get; set; }
    public Showtime Showtime { get; set; }
    public Package Package { get; set; }
}

public class Package
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}

public class FoodAndBeverage
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string StripeTransactionId { get; set; }

    // Navigation property
    public User User { get; set; }
}