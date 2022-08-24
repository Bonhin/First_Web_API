using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Final_Project.Database
{
    public static class DatabaseMethods
    {
        public static async Task<SqlConnection> GetConnectionAsync()
        {
            string connectionString = @"Server=DESKTOP-GLLMT6B\SQLEXPRESS;Database=BoardGame_DB;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
        public static async Task<List<BoardGameProperties>> GetAllBoardGames()
        {
            var boardGameList = new List<BoardGameProperties>();
            var connection = await GetConnectionAsync();

            string query = "SELECT * FROM BoardGameProperties";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                boardGameList.Add(new BoardGameProperties()
                {
                    Id = await reader.GetFieldValueAsync<int>(0),
                    Name = await reader.GetFieldValueAsync<string>(1),
                    YearPublished = await reader.GetFieldValueAsync<int?>(2),
                    MinPlayers = await reader.GetFieldValueAsync<int?>(3),
                    MaxPlayers = await reader.GetFieldValueAsync<int?>(4),
                    PlayTime = await reader.GetFieldValueAsync<int?>(5),
                    MinAge = await reader.GetFieldValueAsync<int?>(6),
                    UsersRated = await reader.GetFieldValueAsync<int?>(7),
                    RatingAverage = await reader.GetFieldValueAsync<double?>(8),
                    BggRank = await reader.GetFieldValueAsync<int?>(9),
                    ComplexityAverage = await reader.GetFieldValueAsync<double?>(10),
                    OwnedUsers = await reader.GetFieldValueAsync<int?>(11),
                    Mechanics = await reader.GetFieldValueAsync<string>(12),
                    Domains = await reader.GetFieldValueAsync<string>(13),
                });


            }
            connection.Close();
            return boardGameList;



        }
        public static async Task<BoardGameProperties> InsertBoardGame(BoardGameProperties boardGameProperties)
        {
            var connection = await GetConnectionAsync();

            string query = @"INSERT INTO [dbo].[BoardGameProperties]
                            ([Name],[Year_Published],[Min_Players],[Max_Players],[Play_Time],[Min_Age],[Users_Rated],[Rating_Average],[BGG_Rank],[Complexity_Average],[Owned_Users],[Mechanics],[Domains]) 
                            VALUES(@Name,@Year_Published,@Min_Players,@Max_Players,@Play_Time,@Min_Age,@Users_Rated,@Rating_Average,@BGG_Rank,@Complexity_Average,@Owned_Users,@Mechanics,@Domains);
                            SELECT SCOPE_IDENTITY();";

            var command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("Name", boardGameProperties.Name));
            command.Parameters.Add(new SqlParameter("Year_Published", boardGameProperties.YearPublished));
            command.Parameters.Add(new SqlParameter("Min_Players", boardGameProperties.MinPlayers));
            command.Parameters.Add(new SqlParameter("Max_Players", boardGameProperties.MaxPlayers));
            command.Parameters.Add(new SqlParameter("Play_Time", boardGameProperties.PlayTime));
            command.Parameters.Add(new SqlParameter("Min_Age", boardGameProperties.MinAge));
            command.Parameters.Add(new SqlParameter("Users_Rated", boardGameProperties.UsersRated));
            command.Parameters.Add(new SqlParameter("Rating_Average", boardGameProperties.RatingAverage));
            command.Parameters.Add(new SqlParameter("BGG_Rank", boardGameProperties.BggRank));
            command.Parameters.Add(new SqlParameter("Complexity_Average", boardGameProperties.ComplexityAverage));
            command.Parameters.Add(new SqlParameter("Owned_Users", boardGameProperties.OwnedUsers));
            command.Parameters.Add(new SqlParameter("Mechanics", boardGameProperties.Mechanics));
            command.Parameters.Add(new SqlParameter("Domains", boardGameProperties.Domains));

            var id = command.ExecuteScalar();
            boardGameProperties.Id = Convert.ToInt32(id);
            connection.Close();
            return boardGameProperties;
        }

        public static async Task<BoardGameProperties> UpdateBoardGame(BoardGameProperties boardGameProperties)
        {
            var connection = await GetConnectionAsync();

            string query = @"UPDATE [dbo].[BoardGameProperties]
                            SET [Name] = @Name,[Year_Published] = @Year_Published,[Min_Players] = @Min_Players,[Max_Players] = @Max_Players,[Play_Time] = @Play_Time,[Min_Age] = @Min_Age,[Users_Rated] = @Users_Rated,[Rating_Average] = @Rating_Average,[BGG_Rank] = @BGG_Rank,[Complexity_Average] = @Complexity_Average,[Owned_Users] = @Owned_Users,[Mechanics] = @Mechanics,[Domains] = @Domains
                            WHERE[Name] = @Name";


            var command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("Name", boardGameProperties.Name));
            command.Parameters.Add(new SqlParameter("Year_Published", boardGameProperties.YearPublished));
            command.Parameters.Add(new SqlParameter("Min_Players", boardGameProperties.MinPlayers));
            command.Parameters.Add(new SqlParameter("Max_Players", boardGameProperties.MaxPlayers));
            command.Parameters.Add(new SqlParameter("Play_Time", boardGameProperties.PlayTime));
            command.Parameters.Add(new SqlParameter("Min_Age", boardGameProperties.MinAge));
            command.Parameters.Add(new SqlParameter("Users_Rated", boardGameProperties.UsersRated));
            command.Parameters.Add(new SqlParameter("Rating_Average", boardGameProperties.RatingAverage));
            command.Parameters.Add(new SqlParameter("BGG_Rank", boardGameProperties.BggRank));
            command.Parameters.Add(new SqlParameter("Complexity_Average", boardGameProperties.ComplexityAverage));
            command.Parameters.Add(new SqlParameter("Owned_Users", boardGameProperties.OwnedUsers));
            command.Parameters.Add(new SqlParameter("Mechanics", boardGameProperties.Mechanics));
            command.Parameters.Add(new SqlParameter("Domains", boardGameProperties.Domains));

            command.ExecuteNonQuery();
            connection.Close();
            return boardGameProperties;
        }

        public static async Task<BoardGameProperties> DeleteBoardGame(BoardGameProperties boardGameProperties)
        {
            var connection = await GetConnectionAsync();

            string query = @"DELETE FROM [dbo].[BoardGameProperties]
                            WHERE [Name] = '" + boardGameProperties.Name + "'";

            var command = new SqlCommand(query, connection);

            command.ExecuteNonQuery();
            connection.Close();
            return boardGameProperties;
        }
    }
}
