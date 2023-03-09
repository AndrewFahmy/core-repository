#pragma warning disable IDE0073

using System;
using FrameworkRepository.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FrameworkRepository.Tests.Common
{
    internal static class ContextFactory
    {
        public static InMemContext CreateContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var context = new InMemContext(options);

            SeedData(context);

            return context;
        }


        private static void SeedData(InMemContext context)
        {
            // users
            context.Users.AddRange(new[] {
                new User{FirstName = "Andrew", LastName = "Fahmy", Mail = "andrew.fahmy@outlook.com"},
                new User{FirstName = "Dia", LastName = "Helmy", Mail = "dia.helmy@outlook.com"},
                new User{FirstName = "Maggie", LastName = "Youssef", Mail = "maggie.youssef@outlook.com"},
                new User{FirstName = "Radwa", LastName = "Mohamed", Mail = "radwa.mohamed@outlook.com"},
                new User{FirstName = "Mina", LastName = "Mouris", Mail = "mina.mouris@outlook.com"}
            });

            // products
            context.Products.AddRange(new[] {
                new Product{Name = "Sanitizer", Price=5.30},
                new Product{Name = "Shampoo", Price=10.30},
                new Product{Name = "Mobile", Price=1200},
                new Product{Name = "Shirt", Price=2.50},
                new Product{Name = "Watch", Price=800},
                new Product{Name = "Laptop", Price=1500},
                new Product{Name = "TV", Price=700},
                new Product{Name = "Soundbar", Price=900}
            });

            context.SaveChanges();
        }
    }
}
