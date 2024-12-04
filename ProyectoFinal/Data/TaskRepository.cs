using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using ProyectoFinal.Models;

namespace ProyectoFinal.Data
{
    public class TaskRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["db.name"].ConnectionString;

        public List<Task> GetAllTasks()
        {
            var tasks = new List<Task>();
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM TASKS";
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Task
                        {
                            id = (int)reader["ID"],
                            title = reader["TITLE"].ToString(),
                            description = reader["DESCRIPTION"].ToString(),
                            isCompleted = (bool)reader["ISCOMPLETED"],
                            expDate = reader["EXPDATE"] as DateTime?
                        });
                    }
                }
            }
            return tasks;
        }

        public Task GetTaskById(int id)
        {
            Task task = null;
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM TASKS WHERE ID = @ID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        task = new Task
                        {
                            id = (int)reader["ID"],
                            title = reader["TITLE"].ToString(),
                            description = reader["DESCRIPTION"].ToString(),
                            isCompleted = (bool)reader["ISCOMPLETED"],
                            expDate = reader["EXPDATE"] as DateTime?
                        };
                    }
                }
            }
            return task;
        }

        public void AddTask(Task task)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "INSERT INTO TASKS (TITLE, DESCRIPTION, ISCOMPLETED, EXPDATE) VALUES (@Title, @Description, @IsCompleted, @ExpDate)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", task.title);
                command.Parameters.AddWithValue("@Description", task.description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IsCompleted", task.isCompleted);
                command.Parameters.AddWithValue("@ExpDate", task.expDate ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTask(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "DELETE FROM TASKS WHERE ID = @ID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}