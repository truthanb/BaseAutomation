using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace AutomationFramework.framework
{
    /// <summary>
    /// Modeled very very closely after https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
    /// Configuation must be loaded by Env class before connecting is possible.
    /// </summary>
    public class DbConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DbConnection()
        {
            Initialize();
        }

        public DbConnection(string databaseName)
        {
            Initialize(databaseName);
        }

        public DbConnection(string server, string databaseName, string username, string password)
        {
            Initialize(server, databaseName, username, password);
        }

        //Initialize values
        private void Initialize()
        {
            server = Env.config.db_server;
            database = Env.config.db_database;
            uid = Env.config.db_user_id;
            password = Env.config.db_password;
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //Initialize values
        private void Initialize(string databaseName)
        {
            server = Env.config.db_server;
            database = databaseName;
            uid = Env.config.db_user_id;
            password = Env.config.db_password;
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private void Initialize(string serverName, string databaseName, string username, string pwd)
        {
            server = serverName;
            database = databaseName;
            uid = username;
            password = pwd;
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Insert(string insertCommand)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(insertCommand, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Update(string updateCommand)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(updateCommand, connection);
                cmd.CommandTimeout = 240;
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        public void Execute(string executeCommand)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(executeCommand, connection);
                cmd.CommandTimeout = 240;
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }


        public void Delete(string deleteCommand)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(deleteCommand, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public DataTable Select(string query)
        {
            DataTable table = new DataTable();
            DataRow row;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.CommandTimeout = 240;
                MySqlDataReader dataReader = cmd.ExecuteReader();

                for (int fieldIndex = 0; fieldIndex < dataReader.FieldCount; fieldIndex++)
                {
                    table.Columns.Add(dataReader.GetName(fieldIndex), dataReader.GetFieldType(fieldIndex));
                }
                while (dataReader.Read())
                {
                    row = table.NewRow();
                    foreach (var column in table.Columns)
                    {
                        row[column.ToString()] = dataReader[column.ToString()];
                    }
                    table.Rows.Add(row);
                }
                dataReader.Close();
                this.CloseConnection();
                return table;
            }
            else
            {
                return table;
            }
        }
    }
}