using GoViatic.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GoViatic.Web.Models;

namespace GoViatic.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Traveler> Travelers { get; set; }
        public DbSet<Trip> Trips  { get; set; }
        public DbSet<Viatic> Viatics { get; set; }
        public DbSet<ViaticType> ViaticTypes { get; set; }
        public DbSet<GoViatic.Web.Models.TripViewModel> TripViewModel { get; set; }
    }
}
