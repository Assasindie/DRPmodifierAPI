using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DRPmodifierAPI
{
    public class db
    {
        SqlConnectionStringBuilder builder;
        public db()
        {
            DotNetEnv.Env.Load(Environment.CurrentDirectory);
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
                    string[] values = new string[reader.FieldCount - 1];
                    for (int i = 0; i < reader.FieldCount - 1; i++)
                    {
                        values[i] = reader.GetValue(i).ToString();
                    }
                    EnvDetails.Add(
                        new DRPenv(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7],
                        values[8], values[9], values[10]));
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
                        values[8], values[9], values[10]);
                }
                //cant find id so return empty env
                return new DRPenv("", "", "", "", "", "", "", "", "", "", "");
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
                string sql = "INSERT INTO env VALUES (";
                for (int i = 0; i < DRPValues.Length; i++)
                {
                    if (i == DRPValues.Length - 1)
                    {
                        sql += "@VALUE" + i.ToString() + ")";
                    }
                    else
                    {
                        sql += "@VALUE" + i.ToString() + ",";
                    }
                }
                using SqlCommand command = new SqlCommand(sql, connection);
                for (int i = 0; i < DRPValues.Length; i++)
                {
                    command.Parameters.AddWithValue("@VALUE" + i.ToString(), DRPValues[i]);
                }
                command.ExecuteReader();
            }
            catch (SqlException) { /*sad react if this happens*/ }
        }
    }
}
