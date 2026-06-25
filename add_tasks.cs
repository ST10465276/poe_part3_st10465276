using System;
using System.Data;

//import for SqlClient
using System.Data.SqlClient;
using System.Windows;

namespace part2
{//start of namespace
    public class add_tasks
    {//start of add_tasks class

        //connection string global
        string connection_string = @"Data Source=(localdb)\MSSQLLocalDB;Database=poe_3_task;";

    //method to create database and tables if not exists
    public void CreateDatabaseIfNotExists()
    {//start of method

        //master connection string to create database
        string masterConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;";

        using (SqlConnection masterConn = new SqlConnection(masterConnectionString))
        {
            masterConn.Open();

            //Check if database exists
            string checkDbQuery = "SELECT database_id FROM sys.databases WHERE Name = 'poe_3_task'";
            SqlCommand checkCmd = new SqlCommand(checkDbQuery, masterConn);
            object result = checkCmd.ExecuteScalar();

            if (result == null)
            {
                //create database if not exists
                string createDbQuery = "CREATE DATABASE poe_3_task";
                SqlCommand createCmd = new SqlCommand(createDbQuery, masterConn);
                createCmd.ExecuteNonQuery();
            }
        }

        //create tables
        CreateTablesIfNotExists();
    }

    //method to create tables
    public void CreateTablesIfNotExists()
        {//start of CreateTablesIfNotExists method 

            using (SqlConnection connects = new SqlConnection(connection_string))
        {
            connects.Open();

            //create tasks table
            string createTasksTable = @"
        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tasks' AND xtype='U')
        CREATE TABLE tasks (
            id INT PRIMARY KEY IDENTITY(1,1),
            username NVARCHAR(100) NOT NULL,
            task_name NVARCHAR(255) NOT NULL,
            task_description NVARCHAR(MAX),
            task_status NVARCHAR(50) DEFAULT 'pending',
            task_reminder NVARCHAR(50),
            
        )";

            SqlCommand createTaskCmd = new SqlCommand(createTasksTable, connects);
            createTaskCmd.ExecuteNonQuery();

            connects.Close();
        }
        }//end of CreateTablesIfNotExists method

        //Method to add each task
        public void add_each_tasks(string username, string task_name, string task_description, string task_reminder)
        {//start of add_each_tasks method

         //first, ensure database exists
            CreateDatabaseIfNotExists();
            
        using (SqlConnection connects = new SqlConnection(connection_string))
        {
            connects.Open();

            //create query with parameters to prevent SQL injection
            string query = @"INSERT INTO tasks (username, task_name, task_description, task_reminder, task_status) 
                         VALUES (@username, @task_name, @task_description, @task_reminder, 'pending')";

            SqlCommand run_query = new SqlCommand(query, connects);

            //add parameters
            run_query.Parameters.AddWithValue("@username", username);
            run_query.Parameters.AddWithValue("@task_name", task_name);
            run_query.Parameters.AddWithValue("@task_description", task_description);

            //convert task_reminder string to DateTime if needed
            DateTime reminderDate;
            if (DateTime.TryParse(task_reminder, out reminderDate))
            {
                run_query.Parameters.AddWithValue("@task_reminder", reminderDate);
            }
            else
            {
                run_query.Parameters.AddWithValue("@task_reminder", DBNull.Value);
            }

            //execute the query
            run_query.ExecuteNonQuery();

            connects.Close();

            }
        }

        //Method to find tasks by username
        public DataTable FindUserTasks(string username)
        {//start of FindUserTasks method

            DataTable tasksTable = new DataTable();

            using (SqlConnection connects = new SqlConnection(connection_string))
            {
                connects.Open();

                //query to get all tasks for the user
                string query = @"SELECT id, task_name, task_description, task_status, task_reminder
                         FROM tasks 
                         WHERE username = @username 
                         ORDER BY id DESC";

                SqlCommand run_query = new SqlCommand(query, connects);
                run_query.Parameters.AddWithValue("@username", username);

                //execute and fill DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(run_query);
                adapter.Fill(tasksTable);

                connects.Close();
            }

            return tasksTable;

        }//end of FindUserTasks method

        //get the user pending tasks
        public DataTable pendingTasks(string username)
        {//start of pendingTasks method

            DataTable tasksTable = new DataTable();

            using (SqlConnection connects = new SqlConnection(connection_string))
            {
                connects.Open();

                string query = @"SELECT id, task_name, task_description, task_status, task_reminder
                         FROM tasks 
                         WHERE username = @username AND task_status = 'pending'
                         ORDER BY id DESC";

                SqlCommand run_query = new SqlCommand(query, connects);
                run_query.Parameters.AddWithValue("@username", username);

                SqlDataAdapter adapter = new SqlDataAdapter(run_query);
                adapter.Fill(tasksTable);

                connects.Close();
            }

            //check if something is found or not

            return tasksTable;
        }//end of pendingTasks method

        //get the completed tasks
        public DataTable completedTasks(string username)
        {//start of completedTasks method

            DataTable tasksTable = new DataTable();

            using (SqlConnection connects = new SqlConnection(connection_string))
            {
                connects.Open();

                string query = @"SELECT id, task_name, task_description, task_status, task_reminder
                         FROM tasks 
                         WHERE username = @username AND task_status = 'completed'
                         ORDER BY id DESC";

                SqlCommand run_query = new SqlCommand(query, connects);
                run_query.Parameters.AddWithValue("@username", username);

                SqlDataAdapter adapter = new SqlDataAdapter(run_query);
                adapter.Fill(tasksTable);

                connects.Close();
            }

            return tasksTable;
        }//end of completedTasks method

        //get all the user tasks
        public DataTable allTasks(string username)
        {//start of allTasks method

            //check and create database and tables 
            CreateDatabaseIfNotExists();

            DataTable tasksTable = new DataTable();

            using (SqlConnection connects = new SqlConnection(connection_string))
            {
                connects.Open();

                string query = @"SELECT id, task_name, task_description, task_status, task_reminder
                         FROM tasks 
                         WHERE username = @username
                         ORDER BY id DESC";

                SqlCommand run_query = new SqlCommand(query, connects);
                run_query.Parameters.AddWithValue("@username", username);

                SqlDataAdapter adapter = new SqlDataAdapter(run_query);
                adapter.Fill(tasksTable);

                connects.Close();
            }

            return tasksTable;

        }//end of allTasks method

        //mark task as completed
        public void markTaskAsCompleted(int taskId)
        {//start of markTaskAsCompleted method
            using (SqlConnection connects = new SqlConnection(connection_string))
            {
                connects.Open();

                string query = @"UPDATE tasks 
                         SET task_status = 'completed' 
                         WHERE id = @taskId";

                SqlCommand run_query = new SqlCommand(query, connects);
                run_query.Parameters.AddWithValue("@taskId", taskId);

                run_query.ExecuteNonQuery();

                connects.Close();
            }
        }//end of markTaskAsCompleted method

        //mark task as pending
        public void markTaskAsPending(int taskId)
        {//start of method

            using (SqlConnection connects = new SqlConnection(connection_string))
            {
                connects.Open();

                string query = @"UPDATE tasks 
                         SET task_status = 'pending' 
                         WHERE id = @taskId";

                SqlCommand run_query = new SqlCommand(query, connects);
                run_query.Parameters.AddWithValue("@taskId", taskId);

                run_query.ExecuteNonQuery();

                connects.Close();
            }
        }//end of markTaskAsPending method

        //delete task
        public void deleteTask(int taskId)
        {//start of deleteTask method

            using (SqlConnection connects = new SqlConnection(connection_string))
            {
                connects.Open();

                string query = @"DELETE FROM tasks WHERE id = @taskId";

                SqlCommand run_query = new SqlCommand(query, connects);
                run_query.Parameters.AddWithValue("@taskId", taskId);

                run_query.ExecuteNonQuery();

                connects.Close();
            }
        }//end of deleteTask method


    }//end of add_tasks class
}//end of namespace