using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Taska.Models;

public class DatabaseHelper
{
    private readonly string _databasePath;

    public DatabaseHelper(string databasePath)
    {
        _databasePath = databasePath;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using (var connection = new SqliteConnection($"Data Source={_databasePath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS TaskModel (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    IsCompleted INTEGER NOT NULL
                )";
            command.ExecuteNonQuery();
        }
    }

    public async Task<List<TaskModel>> GetTasksAsync()
    {
        var tasks = new List<TaskModel>();
        using (var connection = new SqliteConnection($"Data Source={_databasePath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM TaskModel";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tasks.Add(new TaskModel
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                        IsCompleted = reader.GetBoolean(3)
                    });
                }
            }
        }
        return tasks;
    }

    public async Task AddTaskAsync(TaskModel task)
    {
        using (var connection = new SqliteConnection($"Data Source={_databasePath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO TaskModel (Name, Description, IsCompleted)
                VALUES ($name, $description, $isCompleted)";
            command.Parameters.AddWithValue("$name", task.Name);
            command.Parameters.AddWithValue("$description", task.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("$isCompleted", task.IsCompleted ? 1 : 0);
            command.ExecuteNonQuery();
        }
    }

    public async Task UpdateTaskAsync(TaskModel task)
    {
        using (var connection = new SqliteConnection($"Data Source={_databasePath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE TaskModel
                SET Name = $name, Description = $description, IsCompleted = $isCompleted
                WHERE Id = $id";
            command.Parameters.AddWithValue("$name", task.Name);
            command.Parameters.AddWithValue("$description", task.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("$isCompleted", task.IsCompleted ? 1 : 0);
            command.Parameters.AddWithValue("$id", task.Id);
            command.ExecuteNonQuery();
        }
    }

    public async Task DeleteTaskAsync(int id)
    {
        using (var connection = new SqliteConnection($"Data Source={_databasePath}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM TaskModel WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }
    }
}