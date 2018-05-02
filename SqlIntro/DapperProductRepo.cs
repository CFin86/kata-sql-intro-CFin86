using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Dapper;

namespace SqlIntro
{
    public class DapperProductRepo : IProductRepository
    {
        private readonly string _connectionString;

        public DapperProductRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Using Dapper, Reads all the products from the products table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "SELECT ProductId as Id, Name FROM product;";
                return conn.Query<Product>(sql);
            }
        }
        /// <summary>
        /// Using Dapper, Deletes 1 product from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "DELETE FROM product WHERE ProductID = @id";
                conn.Execute(sql, new { id });
            }
        }

        /// <summary>
        /// Using Dapper, Inserts a new Product into the database
        /// </summary>
        /// <param name="prod"></param>
        public void InsertProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "INSERT into product(name) values(@name)";
                conn.Execute(sql, new { name = prod.Name });
            }
        }

        /// <summary>
        /// Using Dapper, Updates 1 Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "UPDATE product SET name = @name WHERE ProductID = @id";
                conn.Execute(sql, new { id = prod.Id, name = prod.Name });
            }
        }

        /// <summary>
        /// Using Dapper, this performs an INNER JOIN from the product table and productreview table, 
        /// returns the name of all products with a review 
        /// </summary>
        ///<returns></returns>
        public IEnumerable<Product> GetProductsWithReview()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "SELECT p.name FROM product as p INNER JOIN productreview as pr on p.ProductID = pr.ProductID;";
                return conn.Query<Product>(sql);
            }
        }

        /// <summary>
        /// Using Dapper, this performs a LEFT JOIN from the product table and productreview table,
        /// returns the name of products with a review 
        /// and the corresponding review 
        /// </summary>
        ///<returns></returns>
        public IEnumerable<Product> GetProductsAndReview()
        {
            
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "SELECT p.name, pr.Comments FROM product as p LEFT JOIN productreview as pr ON p.ProductID = pr.ProductID WHERE comments IS NOT NULL;";
                return conn.Query<Product>(sql);
            }
        }
    }
}