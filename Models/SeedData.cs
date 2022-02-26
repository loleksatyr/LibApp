using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                
                
              if (context.MembershipTypes.Any())
                {
                    Console.WriteLine("Database already seeded");
                    return;
                }
              
              context.MembershipTypes.AddRange(
                    new MembershipType
                    {
                        Id=1,
                        Name="None",
                        SignUpFee = 0,
                        DurationInMonths = 0,
                        DiscountRate = 0
                    },
                    new MembershipType
                    {
                        Id = 2,
                        Name = "Month",
                        SignUpFee = 30,
                        DurationInMonths = 1,
                        DiscountRate = 10
                    },
                    new MembershipType
                    {
                        Id = 3,
                        Name = "3Month",
                        SignUpFee = 90,
                        DurationInMonths = 3,
                        DiscountRate = 15
                    },
                    new MembershipType
                    {
                        Id = 4,
                        Name = "Year",
                        SignUpFee = 300,
                        DurationInMonths = 12,
                        DiscountRate = 20
                    }    
                );
                context.Books.AddRange(
                     new Book
                     {
                        Name = "Harry Potter",
                        AuthorName = "J.K.Rowling",
                        Genre = context.Genre.First(x => x.Id == 1),
                        DateAdded = DateTime.Parse("2022/01/10"),
                        ReleaseDate = DateTime.Parse("1997/01/10"),
                        NumberInStock = 1
                    },
                    new Book
                    {
                        Name = "Proszę o 3",
                        AuthorName = "M.Pająk",
                        Genre = context.Genre.First(x => x.Id == 2),
                        DateAdded = DateTime.Parse("2022/02/20"),
                        ReleaseDate = DateTime.Parse("2022/02/20"),
                        NumberInStock = 2
                    },       
                    new Book
                    {
                        Name = "1984",
                        AuthorName = "G.Orwell",
                        Genre = context.Genre.First(x => x.Id == 3),
                        DateAdded = DateTime.Parse("1949/06/20"),
                        ReleaseDate = DateTime.Parse("1949/06/20"),
                        NumberInStock = 3
                    }
                );
             
                context.Customers.AddRange(
                    new Customer
                    {
                        Name = "Adam",
                        HasNewsletterSubscribed = false,
                        MembershipTypeId = 3,
                        Birthdate = DateTime.Parse("1980/02/24")
                    },
                    new Customer
                    {
                        Name = "Norman",
                        HasNewsletterSubscribed = true,
                        MembershipTypeId = 1,
                        Birthdate = DateTime.Parse("2000/01/30")
                    }

                    );
                 context.SaveChanges();
            }
           
        }
    }
}