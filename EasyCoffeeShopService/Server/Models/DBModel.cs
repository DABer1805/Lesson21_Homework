namespace Server;

using System.Data.SQLite;
using System.Collections.Generic;


public class SQLiteShopRepository : IShopRepository
{

    private string _connectionString;
    private const string CreateTableQuery = @"
        CREATE TABLE IF NOT EXISTS Orders (
            Id INTEGER PRIMARY KEY,
            TotalPrice REAL NOT NULL,
            ItemsList TEXT NOT NULL,
            CreateDate TEXT NOT NULL,
            EndDate TEXT NOT NULL
        )";

    public SQLiteShopRepository(string connectionString)
    {
        _connectionString = connectionString;
        CreateDatabase();
    }

    private void CreateDatabase()
    {
        SQLiteConnection connection = new SQLiteConnection(_connectionString);
        connection.Open();
        using(SQLiteCommand command = new SQLiteCommand(CreateTableQuery, connection))
        {    
            Console.WriteLine($"БД: {_connectionString} создана.");
            command.ExecuteNonQuery();
        }
    }
 
    public List<Order> GetAllOrders()
    {
        List<Order> orders = new List<Order>();
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Orders";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order(Convert.ToDouble(reader["TotalPrice"]), 
                        reader["ItemsList"].ToString(), reader["CreateDate"].ToString(), 
                        reader["EndDate"].ToString());

                        orders.Add(order);

                    }
                } 
            }
        }
        return orders;
    }

    public void AddOrder(Order order)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Orders (TotalPrice, ItemsList, CreateDate, EndDate) " +
            "VALUES (@TotalPrice, @ItemsList, @CreateDate, @EndDate)";
            
            using(SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                command.Parameters.AddWithValue("@ItemsList", order.ItemsList);
                command.Parameters.AddWithValue("@CreateDate", order.CreateDate);
                command.Parameters.AddWithValue("@EndDate", order.EndDate);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteOrder(int id)
    {
        using(SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Orders WHERE Id = @Id";
            
            using(SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}