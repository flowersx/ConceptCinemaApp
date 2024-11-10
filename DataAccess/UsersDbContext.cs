using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Authentification;

namespace DataAccess;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : IdentityDbContext<AppUser>(options);