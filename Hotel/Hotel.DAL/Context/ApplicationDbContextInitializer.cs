// <copyright file="ApplicationDbContextInitializer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Hotel.DAL.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// Initialize db if it is needed.
    /// </summary>
    public class ApplicationDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        /// <summary>
        /// Seeds db.
        /// </summary>
        /// <param name="db">Database context to be seeded through.</param>
        protected override void Seed(ApplicationDbContext db)
        {
            RoomClass roomClass1 = new RoomClass()
            {
                DisplayColor = "yellow",
                Name = "royal",
                Price = 150,
                Info = "The rooms in Ca' Pisani hotel have been realised" +
                " completely using original pieces of design and are taken c" +
                "are of down to the last detail." +
                "Refined style,play of colours and precious materials make the " +
                "hotel a real jewel of contemporary design.The presence of marbles," +
                "precious fabrics and inlaid wood furniture has been designed to guarantee" +
                "guests maximum comfort and relaxation. Elegant and refined," +
                "some rooms overlook the side of the Gallerie dell'Accademia, or have" +
                "a partial view of Giudecca Canal. All rooms are treated with taste" +
                "and refinement: the Deco furnishings typical of the 1930s and 1940s, " +
                "fine Bevilacqua fabrics(created to designs of delicate beauty) and doors " +
                "with geometric patterns each different from the other(and executed with" +
                " extraordinary cabinet-making work) are just some examples that make Ca " +
                "'Pisani a unique place of its kind.",
                AddressImage = "/Content/images/room-royal.jpg",
            };

            RoomClass roomClass2 = new RoomClass()
            {
                DisplayColor = "purple",
                Name = "luxury",
                Price = 90,
                Info = "The marble finishes, the high ceilings of the rooms on" +
                " the first floor, the original beds of the 1930s and 1940s " +
                "(embellished with silver headboards), the bamboo floors, " +
                "the exposed beams of the rooms on the top floor, the stairs" +
                " in wood and glass and the door handles designed ad hoc are" +
                " all meticulously studied elements, they are the result of " +
                "a precious work of craftsmanship." +
                "Some rooms can accommodate three people and we have both cradles" +
                " and cots for small children. In addition, two rooms are available" +
                " for guests with disabilities, both on the first floor.",
                AddressImage = "/Content/images/room-luxury.jpg",
            };

            RoomClass roomClass3 = new RoomClass()
            {
                DisplayColor = "royalblue",
                Name = "standard",
                Price = 50,
                Info = "All rooms are equipped with free Wi-Fi, telephone, " +
                "flat-screen TV, air conditioning, heating with" +
                " electric blinds and double-glazed windows." +
                "The bathrooms are embellished with starlight marble decorations, " +
                "bathtub / shower, hair dryer," +
                "heated towel rail and exclusive complimentary toiletries.",
                AddressImage = "/Content/images/room-standard.jpg",
            };

            RoomClass roomClass4 = new RoomClass()
            {
                DisplayColor = "white",
                Name = "econom",
                Price = 20,
                Info = "As our smallest budget rooms," +
                " the compact bedrooms are suited for single " +
                "occupancy or short-stay double occupancy as " +
                "they have limited space and storage.",
                AddressImage = "/Content/images/room-econom.jpg",
            };

            Random random = new Random(1000);
            for (int i = 0; i < 100; i++)
            {
                RoomClass roomClass = null;
                switch (random.Next(1, 5))
                {
                    case 1:
                        roomClass = roomClass1;
                        break;
                    case 2:
                        roomClass = roomClass2;
                        break;
                    case 3:
                        roomClass = roomClass3;
                        break;
                    case 4:
                        roomClass = roomClass4;
                        break;
                }

                Room room = new Room()
                {
                    Number = i + 1,
                    RoomClass = roomClass,
                    PeopleCount = byte.Parse(random.Next(1, 5).ToString()),
                    IsAccessible = random.Next(0, 21) != 20,
                };
                db.Rooms.Add(room);
            }

            db.RoomClasses.AddRange(new List<RoomClass>() { roomClass1, roomClass2, roomClass3, roomClass4 });

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            roleManager.Create(new IdentityRole("admin"));
            roleManager.Create(new IdentityRole("user"));
            roleManager.Create(new IdentityRole("deleted"));
            roleManager.Create(new IdentityRole("staff"));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var admin = new ApplicationUser()
            {
                UserName = "Dima",
                Email = "dima@mail.com",
            };
            if (userManager.Create(admin, "Asss7&").Succeeded)
            {
                userManager.AddToRoles(admin.Id, "admin");
            }

            var user1 = new ApplicationUser()
            {
                UserName = "DemDeeDum",
                Email = "DemDeeDum@mail.com",
            };
            if (userManager.Create(user1, "Dbbb11%").Succeeded)
            {
                userManager.AddToRoles(user1.Id, "user");
            }

            var user2 = new ApplicationUser()
            {
                UserName = "dark",
                Email = "dark@mail.com",
            };
            if (userManager.Create(user2, "Dbbb11%").Succeeded)
            {
                userManager.AddToRoles(user2.Id, "user");
            }

            var user3 = new ApplicationUser()
            {
                UserName = "red",
                Email = "red@mail.com",
            };
            if (userManager.Create(user3, "Dbbb11%").Succeeded)
            {
                userManager.AddToRoles(user3.Id, "user");
            }

            var user4 = new ApplicationUser()
            {
                UserName = "Nick",
                Email = "Nick@mail.com",
            };
            if (userManager.Create(user4, "Cccc11%").Succeeded)
            {
                userManager.AddToRoles(user4.Id, "staff");
            }

            db.Bookings.Add(new Booking()
            {
                BeginningDate = DateTime.Today.AddDays(10),
                EndingDate = DateTime.Today.AddDays(15),
                DeadLine = DateTime.Today,
                IsConfirmed = true,
                User = user1,
                Room = db.Rooms.Find(random.Next(1, 101)),
            });

            db.Bookings.Add(new Booking()
            {
                BeginningDate = DateTime.Today.AddDays(20),
                EndingDate = DateTime.Today.AddDays(25),
                DeadLine = DateTime.Today.AddDays(2),
                IsConfirmed = false,
                User = user1,
                Room = db.Rooms.Find(random.Next(1, 101)),
            });

            db.Bookings.Add(new Booking()
            {
                BeginningDate = DateTime.Today.AddDays(-10),
                EndingDate = DateTime.Today.AddDays(-5),
                DeadLine = DateTime.Today.AddDays(-25),
                IsConfirmed = true,
                User = user1,
                Room = db.Rooms.Find(random.Next(1, 101)),
            });

            db.Bookings.Add(new Booking()
            {
                BeginningDate = DateTime.Today.AddDays(2),
                EndingDate = DateTime.Today.AddDays(4),
                DeadLine = DateTime.Today.AddDays(-1),
                IsConfirmed = false,
                User = user1,
                Room = db.Rooms.Find(random.Next(1, 101)),
            });

            db.UnluckyRequests.Add(new UnluckyRequest()
            {
                User = user1,
                FinishDate = DateTime.Today.AddDays(30),
                StartDate = DateTime.Today.AddDays(20),
                PeopleCount = byte.Parse(random.Next(1, 5).ToString()),
                RoomClassName = roomClass1.Name,
            });

            db.UnluckyRequests.Add(new UnluckyRequest()
            {
                User = user2,
                FinishDate = DateTime.Today.AddDays(14),
                StartDate = DateTime.Today.AddDays(10),
                PeopleCount = byte.Parse(random.Next(1, 5).ToString()),
                RoomClassName = roomClass2.Name,
            });

            db.UnluckyRequests.Add(new UnluckyRequest()
            {
                User = user3,
                FinishDate = DateTime.Today.AddDays(7),
                StartDate = DateTime.Today.AddDays(5),
                PeopleCount = byte.Parse(random.Next(1, 5).ToString()),
                RoomClassName = roomClass3.Name,
            });

            db.UnluckyRequests.Add(new UnluckyRequest()
            {
                User = user3,
                FinishDate = DateTime.Today.AddDays(7),
                StartDate = DateTime.Today,
                PeopleCount = byte.Parse(random.Next(1, 5).ToString()),
                RoomClassName = roomClass4.Name,
            });

            db.SaveChanges();
        }
    }
}
