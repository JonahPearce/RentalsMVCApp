using EntityLibrary.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PROG73020_M3_Group10.Models
{
    /// <summary>
    /// This class will inherit from the Entity Framework (EF) class
    /// called DbContext and is used by the code to interact with the DB
    /// </summary>
    public class PropertyDBContext : IdentityDbContext<Customer>
    {
        public PropertyDBContext(DbContextOptions options): base(options)
        {
        }

        // Access to the Customer Table.
        public DbSet<Customer> Customers { get; set; }

        // Access to the Property Table.
        public DbSet<Property> Properties { get; set; }

        // Access to the Bookings Table.
        public DbSet<Booking> Bookings { get; set; }

        // Access to the Booking Dates Table.
        public DbSet<BookingDate> BookingDates { get; set; }

        /// <summary>
        /// Add some records upon build
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adding base class version
            base.OnModelCreating(modelBuilder);

            // Adding the customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = "1",
                    UserName = "johndoe",
                    NormalizedUserName = "JOHNDOE",
                    Email = "johndoe@example.com",
                    NormalizedEmail = "JOHNDOE@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PhoneNumber = "123-456-7890",
                    FirstName = "John",
                    LastName = "Doe"
                },
                new Customer
                {
                    Id = "2",
                    UserName = "janedoe",
                    NormalizedUserName = "JANEDOE",
                    Email = "janedoe@example.com",
                    NormalizedEmail = "JANEDOE@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PhoneNumber = "555-555-5555",
                    FirstName = "Jane",
                    LastName = "Doe"
                }
            );

            // Seed the DB with 2 properties
            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    PropertyId = 1,
                    Address = "123 Main St",
                    City = "Toronto",
                    ProvinceOrState = "ON",
                    ZipOrPostalCode = "M5G 2L2",
                    Bedrooms = 2,
                    Bathrooms = 1,
                    PricePerNight = 100.0,
                    Description = "Cozy apartment in the heart of the city",
                    Appliances = new List<string> { "Fridge", "Stove", "Microwave" },
                    OwnerID = 1
                },
                new Property
                {
                    PropertyId = 2,
                    Address = "456 Elm St",
                    City = "Vancouver",
                    ProvinceOrState = "BC",
                    ZipOrPostalCode = "V6B 2G3",
                    Bedrooms = 3,
                    Bathrooms = 2,
                    PricePerNight = 200.0,
                    Description = "Spacious house with a backyard",
                    Appliances = new List<string> { "Fridge", "Stove", "Dishwasher", "Washer", "Dryer" },
                    OwnerID = 1
                }
            );

            // Seed the DB with bookings
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    BookingId = 1,
                    PropertyId = 1,
                    RenterId = 2,
                    NumberOfGuests = 2,
                    AmoundOwned = 200,
                    AmountPayed = 100,
                    Status = true,
                    Cancelled = false
                },
                new Booking
                {
                    BookingId = 2,
                    PropertyId = 2,
                    RenterId = 2,
                    NumberOfGuests = 4,
                    AmoundOwned = 800,
                    AmountPayed = 800,
                    Status = true,
                    Cancelled = false
                }
            );

            modelBuilder.Entity<BookingDate>().HasData(
                new BookingDate { BookingDateId = 1, Date = new DateTime(2023, 5, 1), BookingId = 1 },
                new BookingDate { BookingDateId = 2, Date = new DateTime(2023, 5, 2), BookingId = 1 },
                new BookingDate { BookingDateId = 3, Date = new DateTime(2023, 6, 1), BookingId = 2 },
                new BookingDate { BookingDateId = 4, Date = new DateTime(2023, 6, 2), BookingId = 2 },
                new BookingDate { BookingDateId = 5, Date = new DateTime(2023, 6, 3), BookingId = 2 }
            );

        }
        /// <summary>
        /// Creating an admin user in our user system.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<Customer> userManager =
                serviceProvider.GetRequiredService<UserManager<Customer>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "Sesame123#";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                Customer user = new Customer { 
                    UserName = username,
                    FirstName = "Admin",
                    LastName = "User"
                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
