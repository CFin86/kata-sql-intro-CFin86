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
            var sql = "SELECT ProductId as Id, Name FROM product;";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>(sql);
            }
        }
        /// <summary>
        /// Using Dapper, Deletes 1 product from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            var sql = "DELETE FROM product WHERE ProductID = @id";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(sql, new { id });
            }
        }

        /// <summary>
        /// Using Dapper, Inserts a new Product into the database
        /// </summary>
        /// <param name="prod"></param>
        public void InsertProduct(Product prod)
        {
            var sql = "INSERT into product(name) values(@name)";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(sql, new { name = prod.Name });
            }
        }

        /// <summary>
        /// Using Dapper, Updates 1 Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {
            var sql = "UPDATE product SET name = @name WHERE ProductID = @id";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
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
            var sql = "SELECT product.name FROM product INNER JOIN productreview on product.ProductID = productreview.ProductID;";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
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
            var sql = "SELECT product.name, productreview.Comments FROM product LEFT JOIN productreview ON product.ProductID = productreview.ProductID WHERE comments IS NOT NULL;";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>(sql);
            }
        }
    }
}