using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TP_PWEB.Helpers;
using TP_PWEB.Models;
using TP_PWEB.Models.Properties;
using TP_PWEB.Models.Users;

namespace TP_PWEB.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public string UserId
        {
            get
            {
                return GetCurrentUserId();
            }

        }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
            , IHttpContextAccessor httpContextAccessor)
            : base(options)
        {

            _httpContextAccessor = httpContextAccessor;
        }   
        
        //public 
        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public bool IsCurrentUser(string userId)
        {
            return UserId == userId;
        }

        public async Task<Property> GetPropertyAsync(int? propertyId)
        {

            return propertyId != null ? await this.Properties.FindAsync(propertyId) : null;
        }

        public async Task<bool> IsEmployeeAsync(int propertyId)
        {
            
            var property = await this.GetPropertyAsync(propertyId);

            if (property == null)
                return false;

            return await this.PropertyEmployees
                .Where(e => e.PropertyManagerId == property.OwnerId)
                .AnyAsync(e => e.PropertyEmployeeId == this.UserId);


        }

        public async Task<bool> IsEmployeeOrOwnerAsync(int propertyId)
        {

            var property = await this.GetPropertyAsync(propertyId);

            if (property == null)
                return false;

            return await this.PropertyEmployees
                .Where(e => e.PropertyManagerId == property.OwnerId)
                .AnyAsync(e => e.PropertyEmployeeId == this.UserId) 
                || UserId == property.OwnerId;


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Seed();

            /*
            builder.Entity<Image>()
                .HasOne(i => i.Verification)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(v => v.VerificationId);
                //.OnDelete(DeleteBehavior.);
            */
            builder.Entity<Image>()
                .HasOne(p=>p.Property)
                .WithMany( i=> i.Images)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Property>()
                .HasOne(p => p.Category)
                .WithOne()
                .IsRequired(true);

            builder.Entity<Property>()
                .HasOne(p => p.Category)
                .WithMany()
                .IsRequired(false);

            builder.Entity<VerificationReservation>()
                .HasOne(vr => vr.Verification)
                .WithMany()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
                

            /*
                        builder.Entity<Reservation>().HasOne(e => e.StayEvaluation)
                            .WithOne()
                            .HasForeignKey<Evaluation>()
                            .IsRequired(required: false);
                        //.OnDelete(DeleteBehavior.NoAction);

                        builder.Entity<Reservation>().HasOne(e => e.ClientEvaluation)
                            .WithOne()
                            .HasForeignKey<Evaluation>()
                            .IsRequired(required: false);
                            //.OnDelete(DeleteBehavior.NoAction);
            /*
                        builder.Entity<Verification>()
                            .HasOne(v => v.EntranceProperty)
                            .WithMany(v => v.EntranceVerifications)
                            .HasForeignKey( f => f.EntrancePropertyId)
                            .OnDelete(DeleteBehavior.NoAction);

                        builder.Entity<Verification>()
                         .HasOne(v => v.ExitProperty)
                         .WithMany(v => v.ExitVerifications)
                         .HasForeignKey(f => f.ExitPropertyId)
                         .OnDelete(DeleteBehavior.NoAction);
            */
            //builder.Owned<Verification>();
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


        }
        public DbSet<Property> Properties { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Admin> Admins{ get; set; }

        public DbSet<PropertyManager> PropertyManagers { get; set; }

        public DbSet<PropertyEmployee> PropertyEmployees { get; set; }

        public DbSet<Verification> Verifications{ get; set; }

        public DbSet<VerificationReservation> VerificationReservations{ get; set; }

        public DbSet<Evaluation> Evaluations { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }

    }

    

    



}
