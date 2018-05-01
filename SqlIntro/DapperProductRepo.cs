using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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


        public IEnumerable<Product> GetProducts()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>("SELECT ProductId as Id, Name FROM product;");
            }
        }
        public void DeleteProduct(int id)
        {
            var sql = "DELETE FROM product WHERE ProductID = @id";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(sql, new { id });
            }
        }

        public void InsertProduct(Product prod)
        {
            var sql = "INSERT into product(name) values(@name)";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(sql, new { name = prod.Name });
            }
        }

        public void UpdateProduct(Product prod)
        {
            var sql = "UPDATE product SET name = @name WHERE ProductID = @id";
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var count = conn.Execute(sql, new { id = prod.Id, name = prod.Name });
            }
        }
    }
}