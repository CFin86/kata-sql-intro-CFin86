using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Reads all the products from the products table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM product;";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product { Name = dr["Name"].ToString() };
                }
            }
        }

        /// <summary>
        /// Deletes a Product from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM product WHERE ProductID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates the Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "update product set name = @name where ProductID = @id";
                cmd.Parameters.AddWithValue("@name", prod.Name);
                cmd.Parameters.AddWithValue("@id", prod.Id);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Inserts a new Product into the database
        /// </summary>
        /// <param name="prod"></param>
        public void InsertProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT into product (name) values(@name)";
                cmd.Parameters.AddWithValue("@name", prod.Name);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Performs an INNER JOIN from the product table and productreview table, 
        /// returns the name of all products with a review 
        /// </summary>
        ///<returns></returns>
        public IEnumerable<Product> GetProductsWithReview()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT p.name FROM product as p INNER JOIN productreview as pr on p.ProductID = pr.ProductID;";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product { Name = dr["Name"].ToString() };
                }
            }
        }

        /// <summary>
        /// Performs a LEFT JOIN from the product table and productreview table,
        /// returns the name of products with a review 
        /// and the corresponding review 
        /// </summary>
        ///<returns></returns>
        public IEnumerable<Product> GetProductsAndReview()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT p.name, pr.Comments FROM product as p LEFT JOIN productreview as pr ON p.ProductID = pr.ProductID WHERE comments IS NOT NULL;";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product { Name = dr["Name"].ToString(), Comments = dr["Comments"].ToString() };
                }
            }
        }
    }
}
