using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week9Day4.Esercizio1.Core.Entities;
using Week9Day4.Esercizio1.Core.Interfaces;

namespace Week9Day4.Esercizio1.AdoRepository
{
    public class RepositoryMisurazioneTemperatura : IRepositoryMisurazioneTemperatura
    {
        const string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;" +
                                            "Initial Catalog = AcademyI;" +
                                            "Integrated Security = true";
        public List<MisurazioneTemperatura> GetItemsWithOutState()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select * from dbo.Temperatura where Stato is null";
                    SqlDataReader reader = command.ExecuteReader();
                    List<MisurazioneTemperatura> misurazioneTemperatura = new List<MisurazioneTemperatura>();

                    while (reader.Read())
                    {
                        MisurazioneTemperatura mt = new MisurazioneTemperatura();
                        mt.Id = (int)reader["Id"];
                        mt.DataMisurazione = Convert.ToDateTime(reader["DataMisurazione"]);
                        mt.OraMisurazione = (TimeSpan)reader["OraMisurazione"];
                        mt.Temperatura = (double)reader["Temperatura"];

                        misurazioneTemperatura.Add(mt);
                    }
                    return misurazioneTemperatura;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(MisurazioneTemperatura mt)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "insert into dbo.Temperatura values(@temp, @data, @ora, @stato)";
                    command.Parameters.AddWithValue("@temp", mt.Temperatura);
                    command.Parameters.AddWithValue("@data", mt.DataMisurazione);
                    command.Parameters.AddWithValue("@ora", mt.OraMisurazione);
                    command.Parameters.AddWithValue("@stato", DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(MisurazioneTemperatura mt)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "update dbo.Temperatura set Stato = @stato where Id = @id";
                    command.Parameters.AddWithValue("@stato", mt.Stato);
                    command.Parameters.AddWithValue("@id", mt.Id);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
