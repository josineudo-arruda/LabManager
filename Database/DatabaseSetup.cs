using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Models;

namespace LabManager.Database;

class DatabaseSetup 
{
    private DatabaseConfig databaseConfig;
    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig; 
        CreateTableComputer();
        CreateTableLab();
    }
    private void CreateTableComputer()
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Computers(
                id int not null primary key,
                ram varchar(100) not null,
                processor varchar(100) not null
            );
        ";
        command.ExecuteNonQuery();
        connection.Close();
    }

    private void CreateTableLab()
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Labs(
                id int not null primary key,
                number varchar(100) not null,
                name varchar(100) not null,
                block varchar(100) not null
            );
        ";
        command.ExecuteNonQuery();
        connection.Close();
    }
}