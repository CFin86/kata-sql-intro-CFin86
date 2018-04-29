using System;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO Create a new user and grant him access to this database 
            var connectionString = "Server=localhost;Database=AdventureWorks;Uid=root;Pwd=kuwabara12;";
            var repo = new ProductRepository(connectionString);

            foreach (var prod in repo.GetProducts())
            {
                Console.WriteLine("Product Name:" + prod.Name);
            }


            Console.ReadLine();
        }
       
    }
}
