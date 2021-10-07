using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using webapp.Models;

namespace webapp.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var organizerRole = configuration["Data:Roles:OrganizerRole"];
            var userRole = configuration["Data:Roles:UserRole"];

            if (await roleManager.FindByNameAsync(organizerRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(organizerRole));
            }

            if (await roleManager.FindByNameAsync(userRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(userRole));
            }
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Channel> Channel { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventChannel> EventChannel { get; set; }
        public DbSet<EventType> EventType { get; set; }
        public DbSet<Venue> Venue { get; set; }
        public DbSet<VenueChannel> VenueChannel { get; set; }
        public DbSet<VenueType> VenueType { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<TopUp> TopUp { get; set; }
        public DbSet<TopUpCard> TopUpCard { get; set; }
        public DbSet<GearType> GearType { get; set; }
        public DbSet<Gear> Gear { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<EventGear> EventGear { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<NotificationAppUser> UserNotification { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<EventRating> EventRating { get; set; }

        public DbSet<GearImage> GearImage { get; set; }
        public DbSet<ExceptionLog> ExceptionLog { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<NotificationAppUser>()
                .HasKey(k => new { k.NotificationId, k.AppUserId });
            this.SeedData(builder);
            base.OnModelCreating(builder);
            
        }

        private void SeedData(ModelBuilder builder)
        {
            //AddRoles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "8a0bedfe-d5c7-40d8-9471-1614f7769cb2", Name = "Organizer", ConcurrencyStamp = "fc51a9c2-a72e-46b5-8678-2de81cdcfc5f", NormalizedName = "ORGANIZER" },
                new IdentityRole() { Id = "e80120a3-b085-4f1d-898d-675453fa98e1", Name = "User", ConcurrencyStamp = "c9e0d98c-7943-48d4-8b00-a48828161b35", NormalizedName = "USER" }
                );

            //Add cities

            builder.Entity<Country>()
                .HasData
                (
                    new Country
                    {
                        Id = 1,
                        Name = "Bosnia and Herzegovina"
                    },
                    new Country
                    {
                        Id = 2,
                        Name = "Croatia"
                    },
                    new Country
                    {
                        Id = 3,
                        Name = "Serbia"
                    }
                );

            //Add cities

            builder.Entity<City>()
                .HasData
                (
                    new City
                    {
                        Id = 1,
                        CountryId = 1,
                        Name = "Mostar"
                    },
                    new City
                    {
                        Id = 2,
                        CountryId = 1,
                        Name = "Sarajevo"
                    },
                    new City
                    {
                        Id = 3,
                        CountryId = 2,
                        Name = "Zagreb"
                    },
                    new City
                    {
                        Id = 4,
                        CountryId = 3,
                        Name = "Beograd"
                    }
                );

            //Add streets (addresses)

            builder.Entity<Address>()
                .HasData
                (
                    new Address
                    {
                        Id = 1,
                        CityId = 1,
                        Code = "88000",
                        Street = "Dubrovacka"
                    },
                    new Address
                    {
                        Id = 2,
                        CityId = 2,
                        Code = "71000",
                        Street = "Mehmeda Spahe"
                    },
                    new Address
                    {
                        Id = 3,
                        CityId = 3,
                        Code = "10020",
                        Street = "Masiceva"
                    },
                    new Address
                    {
                        Id = 4,
                        CityId = 4,
                        Code = "11000",
                        Street = "Savska"
                    }
                );

            //Add gear types

            builder.Entity<GearType>()
                .HasData
                (
                    new GearType
                    {
                        Id = 1,
                        Name = "Microphones"
                    },
                    new GearType
                    {
                        Id = 2,
                        Name = "Speakers"
                    },
                    new GearType
                    {
                        Id = 3,
                        Name = "Lights"
                    }
                );

            //Add gear to store

            builder.Entity<Gear>()
                .HasData
                (
                    new Gear
                    {
                        Id = 1,
                        Name = "Korg KG1",
                        Price = 5,
                        GearTypeId = 1,
                    },
                    new Gear
                    {
                        Id = 2,
                        Name = "Sony AX15",
                        Price = 10,
                        GearTypeId = 2
                    },
                    new Gear
                    {
                        Id = 3,
                        Name = "Lux AL350",
                        Price = 15,
                        GearTypeId = 3
                    }
                );

            //Add images for gear pieces

            builder.Entity<GearImage>()
                .HasData
                (
                    new GearImage
                    {
                        Id = 1,
                        ImageLink = "/EventStage/StageEquipment/SpeakerAX.png",
                        ContainerW = 12,
                        ContainerH = 30,
                        TranslateX = 650,
                        TranslateY = 200,
                        ScaleX = 1,
                        ScaleY = 1,
                        GearId = 2,
                        SkewX = 0,
                        SkewY = 0
                    },
                    new GearImage
                    {
                        Id = 2,
                        ImageLink = "/EventStage/StageEquipment/SpeakerAX.png",
                        ContainerW = 12,
                        ContainerH = 30,
                        TranslateX = -130,
                        TranslateY = 200,
                        ScaleX = -1,
                        ScaleY = 1,
                        GearId = 2,
                        SkewX = 0,
                        SkewY = 0
                    },
                    new GearImage
                    {
                        Id = 3,
                        ImageLink = "/EventStage/StageEquipment/StageLights.png",
                        ContainerW = 80,
                        ContainerH = 30,
                        TranslateX = 5,
                        TranslateY = 0,
                        ScaleX = (float)0.6,
                        ScaleY = (float)0.6,
                        GearId = 3,
                        SkewX = 0,
                        SkewY = 0
                    },
                    new GearImage
                    {
                        Id = 4,
                        ImageLink = "/EventStage/StageEquipment/Microphone1.png",
                        ContainerW = 100,
                        ContainerH = 100,
                        TranslateX = 50,
                        TranslateY = 70,
                        ScaleX = (float)0.4,
                        ScaleY = (float)0.3,
                        GearId = 1,
                        SkewX = 0,
                        SkewY = 0
                    }
                );

            //Add codes with credits for topping up wallet

            builder.Entity<TopUpCard>()
                .HasData
                (
                    new TopUpCard
                    {
                        Id = 1,
                        Amount = 100,
                        Code = "ABCDEFG123456100A"
                    },
                    new TopUpCard
                    {
                        Id = 2,
                        Amount = 200,
                        Code = "ABCDEFG123456200A"
                    },
                    new TopUpCard
                    {
                        Id = 3,
                        Amount = 100,
                        Code = "ABCDEFG123456100B"
                    },
                    new TopUpCard
                    {
                        Id = 4,
                        Amount = 200,
                        Code = "ABCDEFG123456200B"
                    },
                    new TopUpCard
                    {
                        Id = 5,
                        Amount = 200,
                        Code = "ABCDEFG123456200C"
                    },
                    new TopUpCard
                    {
                        Id = 6,
                        Amount = 100,
                        Code = "ABCDEFG123456100C"
                    }
                );

            //Add venue types

            builder.Entity<VenueType>()
                .HasData
                (
                    new VenueType
                    {
                        Id = 1,
                        Name = "Club"
                    },
                    new VenueType
                    {
                        Id = 2,
                        Name = "Theater"
                    },
                    new VenueType
                    {
                        Id = 3,
                        Name = "Hall"
                    }
                );

            //Add Event types

            builder.Entity<EventType>()
                .HasData
                (
                    new EventType
                    {
                        Id = 1,
                        Name = "Concert"
                    },
                    new EventType
                    {
                        Id = 2,
                        Name = "Party"
                    },
                    new EventType
                    {
                        Id = 3,
                        Name = "Performance"
                    }
                );
            
            //Add app users

            AppUser user1 = new AppUser()
            {
                Id = "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb",
                UserName = "Organizer1",
                NormalizedUserName = "ORGANIZER1",
                CityId = 1,
                Email = "organizer1@gmail.com",
                NormalizedEmail = "ORGANIZER1@GMAIL.COM",
                LockoutEnabled = false,
                FirstName = "Emir",
                LastName = "Memic",
                Gender = 'M',
                SecurityStamp = "stamp1"
            };
            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            user1.PasswordHash = passwordHasher.HashPassword(user1, "Test123!");

            AppUser user2 = new AppUser()
            {
                Id = "4a74ef28-adb9-4817-8d04-1928dcf3ab32",
                UserName = "User1",
                NormalizedUserName = "USER1",
                CityId = 2,
                Email = "user1@gmail.com",
                NormalizedEmail = "USER1@GMAIL.COM",
                LockoutEnabled = false,
                FirstName = "Melisa",
                LastName = "Ibric",
                Gender = 'F',
                SecurityStamp = "stamp2"
            };
            user2.PasswordHash = passwordHasher.HashPassword(user2, "Test123!");

            AppUser user3 = new AppUser()
            {
                Id = "6ac426e9-e07e-434a-885f-ff3e7489f1c4",
                UserName = "User2",
                NormalizedUserName = "USER2",
                CityId = 3,
                Email = "user2@gmail.com",
                NormalizedEmail = "USER2@GMAIL.COM",
                LockoutEnabled = false,
                FirstName = "Tarik",
                LastName = "Novalic",
                Gender = 'M',
                SecurityStamp = "stamp3"
            };
            user3.PasswordHash = passwordHasher.HashPassword(user3, "Test123!");

            AppUser user4 = new AppUser()
            {
                Id = "9286a727-d145-4e89-b48f-67d760d77855",
                UserName = "User3",
                NormalizedUserName = "USER3",
                CityId = 4,
                Email = "user3@gmail.com",
                NormalizedEmail = "USER3@GMAIL.COM",
                LockoutEnabled = false,
                FirstName = "Riad",
                LastName = "Sendic",
                Gender = 'M',
                SecurityStamp = "stamp4"
            };
            user4.PasswordHash = passwordHasher.HashPassword(user4, "Test123!");
            

            AppUser[] userList = { user1, user2, user3, user4 };

            builder.Entity<AppUser>().HasData(userList);

            //Add user roles

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "8a0bedfe-d5c7-40d8-9471-1614f7769cb2", UserId = "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb" },
                new IdentityUserRole<string>() { RoleId = "e80120a3-b085-4f1d-898d-675453fa98e1", UserId = "4a74ef28-adb9-4817-8d04-1928dcf3ab32" },
                new IdentityUserRole<string>() { RoleId = "e80120a3-b085-4f1d-898d-675453fa98e1", UserId = "6ac426e9-e07e-434a-885f-ff3e7489f1c4" },
                new IdentityUserRole<string>() { RoleId = "e80120a3-b085-4f1d-898d-675453fa98e1", UserId = "9286a727-d145-4e89-b48f-67d760d77855" }
                );

            //Add wallet for user

            builder.Entity<Wallet>()
                .HasData
                (
                    new Wallet
                    {
                        Id = 1,
                        Credits = 500,
                        Discount = 4,
                        AppUserId = "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb"
                    }
                );

            //Add some top ups for top up history

            builder.Entity<TopUp>()
                .HasData
                (
                    new TopUp
                    {
                        Id = 1,
                        TopUpCardId = 1,
                        AppUserId = "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb",
                        TopUpDate = DateTime.Now
                    },
                    new TopUp
                    {
                        Id = 2,
                        TopUpCardId = 2,
                        AppUserId = "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb",
                        TopUpDate = DateTime.Now
                    }
                );

            //Add few sample venues

            builder.Entity<Venue>()
                .HasData
                (
                    new Venue
                    {
                        Id = 1,
                        Name = "Parallax",
                        ImageUrl = "Venue1.jpg",
                        AppUserId = "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb",
                        VenueTypeId = 1,
                        AddressId = 1,
                        Capacity = 1500
                    },
                    new Venue
                    {
                        Id = 2,
                        Name = "Obsidian",
                        ImageUrl = "Venue2.jpg",
                        AppUserId = "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb",
                        VenueTypeId = 2,
                        AddressId = 2,
                        Capacity = 1000
                    },
                    new Venue
                    {
                        Id = 3,
                        Name = "Galaxis",
                        ImageUrl = "Venue3.jpg",
                        AppUserId = "27e1c9dc-44a9-4aa4-a23c-7ee45ce4d5fb",
                        VenueTypeId = 3,
                        AddressId = 3,
                        Capacity = 500
                    }
                );


            //Add few sample events

            builder.Entity<Event>()
                .HasData
                (
                    new Event
                    {
                        Id = 1,
                        Name = "Dino Merlin",
                        ImageUrl = "DinoMerlin1.jpg",
                        VenueId = 1,
                        EventTypeId = 1,
                        CreatedDate = DateTime.Now,
                        StartDate = new DateTime(2021, 8, 5, 20, 30, 00),
                        EndDate = new DateTime(2021, 8, 5, 23, 00, 00),
                        Description = "Dino is recognised for his later solo work during which he established himself as one of the best-selling regional artists of all time. During his career he has produced over a dozen chart-topping albums. Don't miss your opportunity to attend."
                    },
                    new Event
                    {
                        Id = 2,
                        Name = "Eddie Hazel",
                        ImageUrl = "EddieHazel1.jpg",
                        VenueId = 2,
                        EventTypeId = 1,
                        CreatedDate = DateTime.Now,
                        StartDate = new DateTime(2021, 9, 10, 11, 30, 00),
                        EndDate = new DateTime(2021, 9, 10, 17, 00, 00),
                        Description = "Eddie was an American guitarist and singer in early funk music who played lead guitar with Parliament-Funkadelic. His ten-minute guitar solo in the Funkadelic song Maggot Brain is hailed as one of the greatest solos of all time on any instrument"
                    },
                    new Event
                    {
                        Id = 3,
                        Name = "Duck Party",
                        ImageUrl = "DuckParty1.jpg",
                        VenueId = 3,
                        EventTypeId = 2,
                        CreatedDate = DateTime.Now,
                        StartDate = new DateTime(2021, 11, 16, 20, 30, 00),
                        EndDate = new DateTime(2021, 11, 16, 23, 00, 00),
                        Description = "Best duck party around, you will regret if you don't come and will have to wait forever for another one. So don't miss out!"
                    },
                    new Event
                    {
                        Id = 4,
                        Name = "Dino Merlin 2",
                        ImageUrl = "DinoMerlin2.jpg",
                        VenueId = 1,
                        EventTypeId = 1,
                        CreatedDate = DateTime.Now,
                        StartDate = new DateTime(2021, 9, 4, 19, 00, 00),
                        EndDate = new DateTime(2021, 9, 4, 22, 00, 00),
                        Description = "Dino is recognised for his later solo work during which he established himself as one of the best-selling regional artists of all time. During his career he has produced over a dozen chart-topping albums. Don't miss your opportunity to attend."
                    },
                    new Event
                    {
                        Id = 5,
                        Name = "Jimi Hendrix",
                        ImageUrl = "JimiHendrix1.jpg",
                        VenueId = 2,
                        EventTypeId = 3,
                        CreatedDate = DateTime.Now,
                        StartDate = new DateTime(2021, 10, 5, 18, 00, 00),
                        EndDate = new DateTime(2021, 10, 5, 22, 30, 00),
                        Description = "Jimi was an American musician, singer, and songwriter. Although his mainstream career spanned only four years, he is widely regarded as one of the most influential electric guitarists in the history of popular music, and one of the most celebrated musicians of the 20th century. "
                    }
                );

        }
    }
}
