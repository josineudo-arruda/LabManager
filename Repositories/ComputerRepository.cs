using LabManager.Models;
using LabManager.Database;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig databaseConfig;
    
    public ComputerRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;
    public IEnumerable<Computer> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        
        var computers = connection.Query<Computer>("SELECT * FROM Computers");
        
        return computers;
    }

    public Computer Save(Computer computer)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        
        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)",computer);

        return computer;
    }

    public Computer GetById(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var computer = connection.QueryFirstOrDefault<Computer>("SELECT * FROM Computers WHERE id = @Id;", new {Id = id});

        return computer;
    }

    public Computer Update(Computer computer)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Computers SET ram = @Ram, processor = @Processor WHERE id = @Id",computer);

        connection.Close();

        return computer;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Computers WHERE id = @Id", new { Id = id });
    }

    public bool existsById(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        return Convert.ToBoolean(connection.ExecuteScalar("SELECT count(id) FROM Computers WHERE id = @id;", new {id = @id}));
    }

    private Computer readerToComputer(SqliteDataReader reader)
    {
        var computer = new Computer(reader.GetInt32(0),reader.GetString(1),reader.GetString(2));
        return computer;
    }
}