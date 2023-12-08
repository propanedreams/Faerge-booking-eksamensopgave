using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DataAccess.Model;

namespace DataAccess.Context
{
    internal class FaergeContext : DbContext
    {
        public FaergeContext()
        {
            bool created = Database.EnsureCreated();
            if (created)
            {
                Debug.WriteLine("Database created");
            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-06T9FC7\\SQLEXPRESS;Initial Catalog=FærgeDatabasen;Integrated Security=SSPI; TrustServerCertificate=true; user id=sa; password=123456789");
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faerge>().HasData(new Faerge[] {
                new Faerge{id= -1, navn="SS Bon Voyage", maxAntalBiler=20, maxAntalGaester = 100, minAntalBiler=10, minAntalGaester=50, laengde=100, prisPrBil = 199, prisPrGaest = 99},
                new Faerge{id= -2, navn="Jomfru Maria", maxAntalBiler=25, maxAntalGaester = 125, minAntalBiler=10, minAntalGaester=40, laengde=100, prisPrBil = 199, prisPrGaest = 99},
                new Faerge{id= -3, navn="Empirestate Ferry", maxAntalBiler=30, maxAntalGaester = 150, minAntalBiler=10, minAntalGaester=40, laengde=100, prisPrBil = 199, prisPrGaest = 99}
            });

            modelBuilder.Entity<Afrejse>().HasData(new Afrejse[] {
                                new Afrejse{afrejseId=-10, dato= new DateTime(2023, 10, 10, 10,00,00), faergeid = -1},
                                new Afrejse{afrejseId=-11, dato= new DateTime(2023, 10, 10, 10,00,00), faergeid = -1},
                                new Afrejse{afrejseId=-12, dato= new DateTime(2023, 10, 10, 10,00,00), faergeid = -2}
            }) ;
            modelBuilder.Entity<Booking>().HasData(new Booking[] {
                                new Booking {id = -21, dato = new DateTime(2023, 10, 10, 10,00,00), afrejseId = -11},
                                new Booking {id = -22, dato = new DateTime(2023, 10, 10, 10,00,00), afrejseId = -11},
                                new Booking {id = -23, dato = new DateTime(2023, 10, 10, 10,00,00), afrejseId = -12}

            });

            modelBuilder.Entity<Bil>().HasData(new Bil[] {
                                new Bil {nummerplade ="123456", model = "ziggy", bookingId = -21, længde = 200},
                                 new Bil {nummerplade ="654321", model = "impala", bookingId = -22, længde = 200},
                                  new Bil {nummerplade ="414231", model = "Chevrolet", bookingId = -23, længde = 200},
                                   new Bil {nummerplade ="141231", model = "thunderbird", bookingId = -21, længde = 200},
                                    new Bil {nummerplade ="413123", model = "sun fire", bookingId = -22, længde = 200}

            });

            modelBuilder.Entity<Gaest>().HasData(new Gaest[] {
                                new Gaest {gaestId = -30, køn= true, navn = "castiel", bilId = "123456"},
                                new Gaest {gaestId = -31, køn= true, navn = "dean", bilId = "654321"},
                                new Gaest {gaestId = -32, køn= true, navn = "sam", bilId = "141231"},
                                new Gaest {gaestId = -33, køn= true, navn = "crowley", bilId = "414231"},
                                new Gaest {gaestId = -34, køn= true, navn = "metatron", bilId = "413123"}

            });


        }


        public DbSet<Faerge> Faerge { get; set; }

        public DbSet<Afrejse> Afrejse { get; set; }

        public DbSet<Bil> Bil { get; set; }

        public DbSet<Gaest> Gaest { get; set; }

        public DbSet<Booking> Booking { get; set; }


    }
}
