
using LegoBL.Interfaces;
using Microsoft.Data.SqlClient;

public class LegoRepository : ILegoRepository
{
    private string connectionString;

    public LegoRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }



    public LegoTheme GeefLegoTheme(string name)
    {
        LegoTheme theme = null;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string queryTheme = "select Id, Name from LegoTheme where Name=@name";

            using (SqlCommand command = new SqlCommand(queryTheme, connection))
            {
                command.Parameters.AddWithValue("@name", name);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string themeName = (string)reader["Name"];
                        theme = new LegoTheme(id, themeName);
                    }
                }
            }

            if (theme != null)
            {
                string querySets = "select Id, Name, Year, Pieces, Minifigs, MinAge, ImageURL, RetailPrice from LegoSet where ThemeId=@themeId";

                using (SqlCommand command = new SqlCommand(querySets, connection))
                {
                    command.Parameters.AddWithValue("@themeId", theme.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string setId = (string)reader["Id"];
                            string setName = (string)reader["Name"];
                            int year = (int)reader["Year"];
                            int pieces = (int)reader["Pieces"];
                            int minifigs = (int)reader["Minifigs"];

                            int minAge = 0;
                            if (reader["MinAge"] != DBNull.Value)
                                minAge = (int)reader["MinAge"];

                            string imageUrl = "";
                            if (reader["ImageURL"] != DBNull.Value)
                                imageUrl = (string)reader["ImageURL"];

                            double retailPrice = 0;
                            if (reader["RetailPrice"] != DBNull.Value)
                                retailPrice = Convert.ToDouble(reader["RetailPrice"]);

                            LegoSet legoSet = new LegoSet(setId, setName, year, pieces, minifigs, minAge, imageUrl, retailPrice);
                            theme.AddLegoSet(legoSet);
                        }
                    }
                }
            }
        }

        return theme;
    }



    public void ImporteerLegoTheme(List<LegoTheme> legoThemes)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            foreach (LegoTheme theme in legoThemes)
            {
                string queryTheme = "insert into LegoTheme(Name) values(@name); SELECT SCOPE_IDENTITY();";

                SqlCommand commandTheme = new SqlCommand(queryTheme, connection, transaction);
                commandTheme.Parameters.AddWithValue("@name", theme.Name);

                int themeId = Convert.ToInt32(commandTheme.ExecuteScalar());

                foreach (LegoSet legoSet in theme.LegoSets)
                {
                    string querySet = @"insert into LegoSet
                (Id, Name, Year, Pieces, Minifigs, MinAge, ImageURL, RetailPrice, ThemeId)
                values
                (@id, @name, @year, @pieces, @minifigs, @minage, @imageurl, @retailprice, @themeid)";

                    SqlCommand commandSet = new SqlCommand(querySet, connection, transaction);
                    commandSet.Parameters.AddWithValue("@id", legoSet.Id);
                    commandSet.Parameters.AddWithValue("@name", legoSet.Name);
                    commandSet.Parameters.AddWithValue("@year", legoSet.Year);
                    commandSet.Parameters.AddWithValue("@pieces", legoSet.Pieces);
                    commandSet.Parameters.AddWithValue("@minifigs", legoSet.MiniFigs);
                    commandSet.Parameters.AddWithValue("@minage", legoSet.MinAge);
                    commandSet.Parameters.AddWithValue("@imageurl", legoSet.ImageUrl);
                    commandSet.Parameters.AddWithValue("@retailprice", legoSet.RetailPrice);
                    commandSet.Parameters.AddWithValue("@themeid", themeId);

                    commandSet.ExecuteNonQuery();
                }
            }

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
        finally
        {
            connection.Close();
        }

    }
}