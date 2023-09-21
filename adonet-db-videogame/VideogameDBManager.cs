using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    public static class VideogameDBManager
    {
        private static string connectionString = "Data Source=localhost;Initial Catalog=videogames;Integrated Security=True;Pooling=False";

        //STAMPO LA LISTA DI TUTTI I VIDEOGIOCHI
        public static List<Videogame> GetVideogames()
        {

            List<Videogame> videogames = new List<Videogame>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();

                    string query = "SELECT id, name, overview, release_date, software_house_id FROM videogames;";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            Videogame videogameReaded = new Videogame(data.GetInt64(0), data.GetString(1), data.GetString(2), data.GetDateTime(3), data.GetInt64(4));
                            videogames.Add(videogameReaded);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return videogames;

            }

        }


        //INSERISCO NUOVO VIDEOGIOCO NELLA TABELLA VIDEEOGIOCHI
        public static bool InsertVideogame(Videogame videogameToAdd)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    try
                    {
                        connection.Open();

                        string query = "INSERT INTO videogames (name, overview, release_date, software_house_id) VALUES (@Name, @Overview, @Release_date, @Software_house_id);";

                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.Add(new SqlParameter("@Name", videogameToAdd.Name));
                        cmd.Parameters.Add(new SqlParameter("@Overview", videogameToAdd.Overview));
                        cmd.Parameters.Add(new SqlParameter("@Release_date", videogameToAdd.ReleaseDate));
                        cmd.Parameters.Add(new SqlParameter("@Software_house_id", videogameToAdd.SoftwareHouseId));


                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    return false;

                }
            }


        //STAMPO IL VIDEOGIOCO CHE CORRISPONDE AD UN DETERMINATO ID
        public static void GetIdVideogame(long id)
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();

                    string query = "SELECT * FROM videogames WHERE id = @Id;";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", id ));
                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            while (data.Read())
                            {
                                Console.WriteLine($"The Videogame name is: {data["name"]}");
                                
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                

            }

        }

    }
}
