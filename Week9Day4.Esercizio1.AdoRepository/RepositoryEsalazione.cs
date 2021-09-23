using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Week9Day4.Esercizio1.Core.Entities;
using Week9Day4.Esercizio1.Core.Interfaces;

namespace Week9Day4.Esercizio1.AdoRepository
{
    public class RepositoryEsalazione : IRepositoryEsalazione
    {
        const string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;" +
                                          "Initial Catalog = AcademyI;" +
                                          "Integrated Security = true";
        public List<Esalazione> GetItemsWithOutState()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select * from dbo.EsalazioniTossiche where Stato is null";

                    SqlDataReader reader = command.ExecuteReader();

                    List<Esalazione> esalazioni = new List<Esalazione>();

                    while (reader.Read())
                    {
                        Esalazione esalazione = new Esalazione();
                        esalazione.Id = (int)reader["Id"];
                        esalazione.DataMisurazione = Convert.ToDateTime(reader["DataMisurazione"]); // (DateTime)reader["DataMisurazione"]
                        esalazione.OraMisurazione = (TimeSpan)(reader["OraMisurazione"]);
                        esalazione.ConcentrazionePpm = Convert.ToDouble(reader["ConcentrazionePpm"]);

                        esalazioni.Add(esalazione);
                    }

                    return esalazioni;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(Esalazione e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "insert into dbo.EsalazioniTossiche values(@ppm, @data, @ora, @stato)";
                    command.Parameters.AddWithValue("@ppm", e.ConcentrazionePpm);
                    command.Parameters.AddWithValue("@data", e.DataMisurazione);
                    command.Parameters.AddWithValue("@ora", e.OraMisurazione);
                    command.Parameters.AddWithValue("@stato", DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Esalazione e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "update dbo.EsalazioniTossiche set Stato = @stato where Id = @id";
                    command.Parameters.AddWithValue("@stato", e.Stato);
                    command.Parameters.AddWithValue("@id", e.Id);

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
