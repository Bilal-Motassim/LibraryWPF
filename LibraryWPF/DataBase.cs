using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Npgsql;

namespace LibraryWPF
{
    internal class DataBase
    {
        private static string Host = "localhost";
        private static string User = "postgres";
        private static string DBname = "dbtest";
        private static string Password = "123";
        private static string Port = "5432";

        public static void Start()
        {
            string connString =
                String.Format(
                    "Host={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password);

            using (var conn = new NpgsqlConnection(connString))
            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                CreateTable(conn, "user2", "CREATE TABLE IF NOT EXISTS \"user2\" (id_U SERIAL PRIMARY KEY, FirstName VARCHAR(255), LastName VARCHAR(255), Email VARCHAR(255), PasswordHash VARCHAR(255), isAdmin BOOLEAN)");

                CreateTable(conn, "book", "CREATE TABLE IF NOT EXISTS book (id_B SERIAL PRIMARY KEY, Title VARCHAR(255), Genre VARCHAR(255), Available BOOLEAN)");

                CreateTable(conn, "reservation", "CREATE TABLE IF NOT EXISTS reservation (DateRes DATE, Duration INT, user_id INT REFERENCES \"user2\"(id_U), book_id INT REFERENCES book(id_B))");
            }

            Console.WriteLine("Press RETURN to exit");
            Console.ReadLine();
        }

        static void CreateTable(NpgsqlConnection connection, string tableName, string createTableQuery)
        {
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Table '{tableName}' created successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating table '{tableName}': {ex.Message}");
            }
        }
    }
}




