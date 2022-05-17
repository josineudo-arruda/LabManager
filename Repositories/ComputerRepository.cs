using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class ComputerRepository
{
    public List<Computer> GetAll()
    {
        var computers  = new List<Computer>();
        
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers;";
        
        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            var computer = new Computer(reader.GetInt32(0),reader.GetString(1),reader.GetString(2));

            computers.Add(computer);
        }
        connection.Close();

        return computers;
    }
}