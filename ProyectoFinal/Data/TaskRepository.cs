using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using ProyectoFinal.Models;

namespace ProyectoFinal.Data
{
    public class TaskRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TodoDB"].ConnectionString;

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
                            ID = (int)reader["ID"],
                            Title = reader["TITLE"].ToString(),
                            Description = reader["DESCRIPTION"].ToString(),
                            IsCompleted = (bool)reader["ISCOMPLETED"],
                            ExpDate = reader["EXPDATE"] as DateTime?
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
                            ID = (int)reader["ID"],
                            Title = reader["TITLE"].ToString(),
                            Description = reader["DESCRIPTION"].ToString(),
                            IsCompleted = (bool)reader["ISCOMPLETED"],
                            ExpDate = reader["EXPDATE"] as DateTime?
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
                command.Parameters.AddWithValue("@Title", task.Title);
                command.Parameters.AddWithValue("@Description", task.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                command.Parameters.AddWithValue("@ExpDate", task.ExpDate ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateTask(Task task)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "UPDATE TASKS SET TITLE = @Title, DESCRIPTION = @Description, ISCOMPLETED = @IsCompleted, EXPDATE = @ExpDate WHERE ID = @ID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", task.ID);
                command.Parameters.AddWithValue("@Title", task.Title);
                command.Parameters.AddWithValue("@Description", task.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                command.Parameters.AddWithValue("@ExpDate", task.ExpDate ?? (object)DBNull.Value);

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