using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DRPmodifierAPI
{
    public class Db
    {
        private static DateTime LastUpload = DateTime.Now;
        readonly SqlConnectionStringBuilder builder;
        public Db()
        {
            DotNetEnv.Env.Load(Environment.CurrentDirectory + "/.env");
            builder = new SqlConnectionStringBuilder
            {
                DataSource = Environment.GetEnvironmentVariable("DataSource"),
                UserID = Environment.GetEnvironmentVariable("UserID"),
                Password = Environment.GetEnvironmentVariable("Password"),
                InitialCatalog = Environment.GetEnvironmentVariable("InitialCatalog"),
            };
        }

        public List<DRPenv> GetAll()
        {
            try
            {
                using SqlConnection connection = new SqlConnection(builder.ConnectionString);
                connection.Open();
                List<DRPenv> EnvDetails = new List<DRPenv>();
                string sql = "SELECT * FROM env";
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string[] values = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        values[i] = reader.GetValue(i).ToString();
                    }
                    EnvDetails.Add(
                        new DRPenv(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7],
                        values[8], values[9], values[10], values[11]));
                }
                return EnvDetails;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public DRPenv GetID(int ID)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(builder.ConnectionString);
                connection.Open();
                string sql = "SELECT * FROM env WHERE ID = @ID";
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", ID);
                using SqlDataReader reader = command.ExecuteReader();
                string[] values;
                while (reader.Read())
                {
                    values = new string[reader.FieldCount - 1];
                    for (int i = 0; i < reader.FieldCount - 1; i++)
                    {
                        values[i] = reader.GetValue(i).ToString();
                    }
                    return new DRPenv(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7],
                        values[8], values[9], values[10], values[11]);
                }
                //cant find id so return empty env
                return new DRPenv("", "", "", "", "", "", "", "", "", "", "", "");
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public void Insert(DRPenv values)
        {
            try
            {
                string[] DRPValues = values.ToArray();
                using SqlConnection connection = new SqlConnection(builder.ConnectionString);
                connection.Open();
                //sql string builder @VALUE is used for SQL injection prevention
                string sql = "INSERT INTO env VALUES (";
                for (int i = 0; i < DRPValues.Length - 1; i++)
                {
                    if (i == DRPValues.Length - 2)
                    {
                        sql += "@VALUE" + i.ToString() + ")";
                    }
                    else
                    {
                        sql += "@VALUE" + i.ToString() + ",";
                    }
                }
                using SqlCommand command = new SqlCommand(sql, connection);
                for (int i = 0; i < DRPValues.Length-1; i++)
                {
                    command.Parameters.AddWithValue("@VALUE" + i.ToString(), DRPValues[i]);
                }
                command.ExecuteReader();
            }
            catch (SqlException) { /*sad react if this happens*/ }
        }

        public string CheckInsert(DRPenv values)
        {
            //Checks if it has been 5 minutes since last upload.
            TimeSpan TimeElapsed = DateTime.Now - LastUpload;
            if(TimeElapsed.TotalSeconds < 300)
            {
                return "Too many uploads recently, please try again later.";
            }
            //ClientID should be an int 
            if(!int.TryParse(values.CLIENTIDTEXTBOX, out int ClientID))
            {
                return "Invalid Client ID";
            }
            //bad words big yikes
            List<string> Profanity = new List<string> {
                "nig",
                "fuck",
                "shit",
            };

            //if contains any bad words
            if (Profanity.Any(values.FILENAMETEXTBOX.ToLower().Contains))
            {
                return "Failed to add - Contains Profanity";
            }

            //if the previous checks pass try to add to the DB.
            try
            {
                using SqlConnection connection = new SqlConnection(builder.ConnectionString);
                connection.Open();
                /*Checks to see if the clientID is already present and/or if the file name is present. 
                 If it returns more than 1 row it is not unique and may be spam.*/
                string sql = "SELECT * FROM dbo.env WHERE CLIENTIDTEXTBOX = @VALUE1 OR FILENAMETEXTBOX = @VALUE2";
                using SqlCommand command = new SqlCommand(sql, connection);
                //used for SQL injection prevention.
                command.Parameters.AddWithValue("@VALUE1", values.CLIENTIDTEXTBOX);
                command.Parameters.AddWithValue("@VALUE2", values.FILENAMETEXTBOX);
                using SqlDataReader reader = command.ExecuteReader();
                //if query returns nothing neither value exists.
                if(reader.HasRows)
                {
                    return "Preset already exists!";
                } else
                {
                    LastUpload = DateTime.Now;
                    return "Success";
                }
            }
            catch (Exception)
            {
                return "Something went wrong";
            } 
        }
    }
}
