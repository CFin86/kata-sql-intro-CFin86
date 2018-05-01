using System;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=localhost;Database=AdventureWorks;Uid=finney;Pwd=password;";
            // This statement on line 11 allows you to use the Nuget package Dapper
            //var repo = new DapperProductRepo(connectionString);

            //This statement on line 14 allows you to use parameterized ANSI SQL statements
            var repo = new ProductRepository(connectionString);
            foreach (var prod in repo.GetProducts())
            {
                Console.Write("Product Name: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(prod.Name);
                Console.ResetColor();
            }

            // this block(line 22, 30) is in place to test the InsertProduct, UpdateProduct && DeleteProduct methods
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
