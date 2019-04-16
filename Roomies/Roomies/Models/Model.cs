using Microsoft.EntityFrameworkCore;
using System;

namespace EFGetStarted.AspNetCore.NewDb.Models
{
    public class RoomiesContext : DbContext
    {
        public RoomiesContext(DbContextOptions<RoomiesContext> options) : base(options)
        {

        }

        public DbSet<Apartment> Apartments { get; set; }
    }

    public class Apartment
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public long UpdatedBy { get; set; }
    }
}