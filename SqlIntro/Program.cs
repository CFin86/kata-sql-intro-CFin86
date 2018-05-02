using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
#if DEBUG
                .AddJsonFile("appsettings.Debug.json")
#else
    .AddJsonFile("appsettings.Release.json", optional: true)
#endif
            .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            // This statement on line 23 allows you to use the Nuget package Dapper
            var repo = new DapperProductRepo(connectionString);

            //This statement on line 26 allows you to use parameterized ANSI SQL statements
            //var repo = new ProductRepository(connectionString);
            foreach (var prod in repo.GetProducts())
            {
                Console.Write("Product Name: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(prod.Name);
                Console.ResetColor();
            }

            // this block(line 35, 41) is in place to test the InsertProduct, UpdateProduct && DeleteProduct methods
            /* int id = 1004;
            var name = "True Coders Student Terrorizer";
            var newEntry = new Product { Id = id, Name = name };
            //repo.InsertProduct(newEntry);
            // repo.UpdateProduct(newEntry);
            //repo.DeleteProduct(id); */

            foreach (var rev in repo.GetProductsWithReview())
            {
                Console.Write("Product with review: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(rev.Name);
                Console.ResetColor();
            }
            foreach (var prodAndRev in repo.GetProductsAndReview())
            {
                Console.Write("Product: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(prodAndRev.Name);
                Console.ResetColor();

                Console.Write("Review: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(prodAndRev.Comments + "\n");
                Console.ResetColor();
            }
            Console.ReadLine();
        }

    }
}
