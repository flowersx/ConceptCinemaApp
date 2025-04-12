using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace DataAccess;

public class CinemaDbContext(DbContextOptions<CinemaDbContext> options) : DbContext(options)
{
    public DbSet<CinemaHall> CinemaHalls { get; set; }
    public DbSet<Seat?> Seats { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Package> Packages { get; set; }    
    public DbSet<FoodAndBeverage> FoodAndBeverages { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<PackageFoodItem> PackageFoodItems { get; set; }
    public DbSet<TicketPackage> TicketPackages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    modelBuilder.Entity<Seat>()
        .HasOne(s => s.CinemaHall)
        .WithMany(h => h.Seats)
        .HasForeignKey(s => s.CinemaHallId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Showtime>()
        .HasOne(st => st.Movie)
        .WithMany(m => m.Showtimes)
        .HasForeignKey(st => st.MovieId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Showtime>()
        .HasOne(st => st.CinemaHall)
        .WithMany(h => h.Showtimes)
        .HasForeignKey(st => st.CinemaHallId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Ticket>()
        .HasOne(t => t.Showtime)
        .WithMany(st => st.Tickets)
        .HasForeignKey(t => t.ShowtimeId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Ticket>()
        .HasOne(t => t.Seat)
        .WithMany(s => s.Tickets)
        .HasForeignKey(t => t.SeatId)
        .OnDelete(DeleteBehavior.Restrict);    modelBuilder.Entity<Ticket>()
        .HasOne(t => t.User)
        .WithMany(u => u.Tickets)
        .HasForeignKey(t => t.UserId)
        .OnDelete(DeleteBehavior.Cascade);    modelBuilder.Entity<Transaction>()
        .HasOne(tr => tr.User)
        .WithMany(u => u.Transactions)
        .HasForeignKey(tr => tr.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Ticket>()
        .HasKey(t => t.Id);
        
    modelBuilder.Entity<Ticket>()
        .HasIndex(t => new { t.UserId, t.SeatId, t.ShowtimeId })
        .IsUnique();
        
    modelBuilder.Entity<PackageFoodItem>()
        .HasKey(pf => new { pf.PackageId, pf.FoodAndBeverageId });
        
    modelBuilder.Entity<PackageFoodItem>()
        .HasOne(pf => pf.Package)
        .WithMany(p => p.PackageFoodItems)
        .HasForeignKey(pf => pf.PackageId)
        .OnDelete(DeleteBehavior.Cascade);
          modelBuilder.Entity<PackageFoodItem>()
        .HasOne(pf => pf.FoodAndBeverage)
        .WithMany(f => f.PackageFoodItems)
        .HasForeignKey(pf => pf.FoodAndBeverageId)
        .OnDelete(DeleteBehavior.Cascade);
        
    modelBuilder.Entity<TicketPackage>()
        .HasKey(tp => new { tp.TicketId, tp.PackageId });
        
    modelBuilder.Entity<TicketPackage>()
        .HasOne(tp => tp.Ticket)
        .WithMany(t => t.TicketPackages)
        .HasForeignKey(tp => tp.TicketId)
        .OnDelete(DeleteBehavior.Cascade);
        
    modelBuilder.Entity<TicketPackage>()
        .HasOne(tp => tp.Package)
        .WithMany(p => p.TicketPackages)
        .HasForeignKey(tp => tp.PackageId)
        .OnDelete(DeleteBehavior.Cascade);

    base.OnModelCreating(modelBuilder);
    }
}


public class CinemaHall
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TotalSeats { get; set; }

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

    public CinemaHall CinemaHall { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string ImageBase64 { get; set; }
    public bool ShowInMainPage { get; set; } = false;
    public ICollection<Showtime> Showtimes { get; set; }
}

public class Showtime
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int CinemaHallId { get; set; }
    public int DurationMinutes { get; set; }
    public decimal BasePrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public bool IsMonday { get; set; }
    public bool IsTuesday { get; set; }
    public bool IsWednesday { get; set; }
    public bool IsThursday { get; set; }
    public bool IsFriday { get; set; }
    public bool IsSaturday { get; set; }
    public bool IsSunday { get; set; }
    public bool IsActive { get; set; } = true;
    
    public Movie Movie { get; set; }
    public CinemaHall CinemaHall { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
    public ICollection<ShowtimeInterval> Intervals { get; set; }
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

    public User User { get; set; }
    public Seat Seat { get; set; }
    public Showtime Showtime { get; set; }
    public ICollection<TicketPackage> TicketPackages { get; set; }
}

public class Package
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ICollection<TicketPackage> TicketPackages { get; set; }
    public ICollection<PackageFoodItem> PackageFoodItems { get; set; }
}

public class FoodAndBeverage
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageBase64 { get; set; }
    public ICollection<PackageFoodItem> PackageFoodItems { get; set; }
}

public class PackageFoodItem
{
    public int PackageId { get; set; }
    public int FoodAndBeverageId { get; set; }
    public int Quantity { get; set; }
    
    public Package Package { get; set; }
    public FoodAndBeverage FoodAndBeverage { get; set; }
}

public class TicketPackage
{
    public int TicketId { get; set; }
    public int PackageId { get; set; }
    
    public Ticket Ticket { get; set; }
    public Package Package { get; set; }
}

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string StripeTransactionId { get; set; }
    public User User { get; set; }
}