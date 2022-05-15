/*
foreach (var arg in args)
{
    Console.WriteLine(arg);
}
*/

using Microsoft.Data.Sqlite;
var connection = new SqliteConnection("Data Source=database.db");
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

connection.Open();

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
//Routing
var modelName = args[0];
var modelAction = args[1];


if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Computer List");
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        
        command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers;";
        
        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            Console.WriteLine("{0},{1},{2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        }
        
        reader.Close();
        connection.Close();
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];
        
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        
        command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES ($id, $ram, $processor);";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processor", processor);
        
        command.ExecuteNonQuery();
        connection.Close();
    }
}

if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Lab List");
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        
        command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Labs;";
        
        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            Console.WriteLine("{0},{1},{2},{3}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
        }
        
        reader.Close();
        connection.Close();
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = args[3];
        var name = args[4];
        var block = args[5];
        
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        
        command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Labs VALUES ($id, $number, $name, $block);";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$number", number);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$block", block);
        
        command.ExecuteNonQuery();
        connection.Close();
    }
}