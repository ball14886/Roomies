using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFGetStarted.AspNetCore.NewDb.Models
{
    public class RoomiesContext : DbContext
    {
        public RoomiesContext(DbContextOptions<RoomiesContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>()
                        .HasKey(x => new { x.RoomID, x.TenantID });
        }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Officer> Officers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractForm> ContractForms { get; set; }
    }

    /// <summary>
    /// Base 4
    /// Consiste of 
    ///     CreatedDateTime
    ///     CreatedBy
    ///     UpdatedDateTime
    ///     UpdatedBy
    /// </summary>

    /// <summary>
    /// Base 6 
    /// Consiste of 
    ///     IsActive
    ///     IsDeleted
    ///     CreatedDateTime
    ///     CreatedBy
    ///     UpdatedDateTime
    ///     UpdatedBy
    /// </summary>

    public class Apartment
    {
        public long ApartmentID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public long UpdatedBy { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }

    public class Tenant
    {
        public long TenantID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string TaxIdentification { get; set; }
        public string PassPortNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string ContactNumber2 { get; set; }
        public string Note { get; set; }

        public ICollection<Contract> Contracts { get; set; }

        #region Base 4
        public DateTime CreatedDateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public long UpdatedBy { get; set; } 
        #endregion
    }

    public class Officer
    {
        public long OfficerID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string TaxIdentification { get; set; }
        public string PassPortNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string ContactNumber2 { get; set; }
        public string Note { get; set; }

        #region Base 6
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public long UpdatedBy { get; set; } 
        #endregion
    }

    public class Room
    {
        public long RoomID { get; set; }
        public string RoomNumber { get; set; }

        #region Foreign Keys
        #region Apartment
        public long ApartmentID { get; set; }
        public Apartment Apartment { get; set; }
        #endregion

        #region RoomType
        public long RoomTypeID { get; set; }
        public RoomType RoomType { get; set; }  
        #endregion
        #endregion

        public bool IsRepairRequired { get; set; }
        public bool IsBeingRepaired { get; set; }

        #region Base 6
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public long UpdatedBy { get; set; }
        #endregion

        public ICollection<Contract> Contracts { get; set; }
    }

    public class RoomType
    {
        public long RoomTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal RoomRate { get; set; }
        public int MaxTenantCount { get; set; }
        public int LateAllowedDayCount { get; set; }
        public decimal LatePaymentRatePerDay { get; set; }

        #region Base 6
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public long UpdatedBy { get; set; } 
        #endregion

        public ICollection<Room> Rooms { get; set; }
    }

    public class Contract
    {
        #region Foreign Keys
        [Key]
        public long RoomID { get; set; }
        public Room Room { get; set; }

        [Key]
        public long TenantID { get; set; }
        public Tenant Tenant { get; set; }

        public long ContractFormID { get; set; }
        public ContractForm ContractForm { get; set; }

        public long OfficerID { get; set; }
        public Officer Officer { get; set; } 
        #endregion

        /// <Issue>
        /// For monthly rent
        /// *** Note : What if daily just check out and monthy check in
        ///            Do we have to remember daily in and out meter volumn?
        /// </Issue>
        public int WaterStartUnit { get; set; }
        public int ElectricityStartUnit { get; set; }
        public decimal DepositAmount { get; set; } //Decimal because some might deposit with decimal value
        public DateTime DepositDateTime { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public int MinimumMonth { get; set; }

        #region Base 6
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public long UpdatedBy { get; set; }
        #endregion
    }

    public class ContractForm
    {
        public long ContractFormID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        #region Base 6
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public long UpdatedBy { get; set; }
        #endregion

        public ICollection<Contract> Contracts { get; set; }
    }
}