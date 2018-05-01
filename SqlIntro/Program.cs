using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=localhost;Database=AdventureWorks;Uid=finney;Pwd=password;";
            var repo = new DapperProductRepo(connectionString);

            foreach (var prod in repo.GetProducts())
            {
                Console.WriteLine("Product Name:" + prod.Name);
            }
            var id = 1001;
            var name = "The Archive";
            var newEntry = new Product { Id = id, Name = name };
            //repo.InsertProduct(newEntry);
            repo.DeleteProduct(1002);
            Console.ReadLine();
        }
       
    }
}
