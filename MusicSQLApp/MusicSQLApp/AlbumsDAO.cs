using MailKit.Search;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSQLApp
{
    internal class AlbumsDAO
    {
        string connectionString = "dataSource = localhost; port = 3306; username = root; password = root; database = music";//Datasource includes things like IP address etc
        //Computer is doubling as the application and the database server

        public List<Album> getAllAlbums()
        {

            List<Album> returnThese = new List<Album>();

            //Connect to MYSQL server

            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();

            MySqlCommand command = new MySqlCommand("Select * From Albums", connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album a = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5),

                    };


                    a.Tracks = getTracksForAlbum(a.ID); //helper method

                    returnThese.Add(a);
                }
            }

            connection.Close();

            return returnThese;

        }

        public List<Album> searchTitles(String searchTerm)
        {

            List<Album> returnThese = new List<Album>();

            //Connect to MYSQL server

            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();

            String searchWildPhrase = "%" + searchTerm + "%";

            MySqlCommand command = new MySqlCommand();

            command.CommandText = "Select * From Albums Where Album_Title Like @search";
            command.Parameters.AddWithValue("@search", searchWildPhrase);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album a = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5),

                    };
                    returnThese.Add(a);
                }
            }

            connection.Close();

            return returnThese;
        }

        internal int addOneAlbum(Album album)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();

            MySqlCommand command = new MySqlCommand("INSERT INTO `albums`(`Album_Title`, `Artist`, `Year`, `Image_Name`, `Description`) VALUES (@albumtitle, @artist, @year, @imageURL, @description)", connection);

            command.Parameters.AddWithValue("@albumtitle", album.AlbumName);
            command.Parameters.AddWithValue("@artist", album.ArtistName);
            command.Parameters.AddWithValue("@year", album.Year);
            command.Parameters.AddWithValue("@imageURL", album.ImageURL);
            command.Parameters.AddWithValue("@description", album.Description);

            int newRows = command.ExecuteNonQuery();



            connection.Close();

            return newRows;

        }

        internal int deleteTrack(int trackID)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();

            MySqlCommand command = new MySqlCommand("DELETE FROM tracks WHERE `tracks`.`ID` = @trackID", connection);

            command.Parameters.AddWithValue("@trackID", trackID);
            

            int result = command.ExecuteNonQuery();



            connection.Close();

            return result;
        }

        public List<Track> getTracksForAlbum(int albumID)
        {
            List<Track> returnThese = new List<Track>();

            //Connect to MYSQL server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "Select * From Tracks Where albums_ID = @albumid";
            command.Parameters.AddWithValue("@albumid", albumID);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Track t = new Track
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Number = reader.GetInt32(2),
                        VideoURL = reader.GetString(3),
                        Lyrics = reader.GetString(4),
                    };
                    returnThese.Add(t);
                }
            }

            connection.Close();

            return returnThese;
        }
    

    public List<JObject> getTracksUsingJoin(int albumID)
    {
        List<JObject> returnThese = new List<JObject>();

        //Connect to MYSQL server
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        MySqlCommand command = new MySqlCommand();
        command.CommandText = "SELECT tracks.ID as trackID, albums.Album_Title, `number`, track_Title,  `video_url` FROM `tracks` Join albums ON albums_ID = albums.ID where albums_ID = @albumid";
        command.Parameters.AddWithValue("@albumid", albumID);
        command.Connection = connection;

        using (MySqlDataReader reader = command.ExecuteReader())
        {

          
                    

                    while (reader.Read())
                    {

                        JObject newTrack = new JObject(); 

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            newTrack.Add(reader.GetName(i).ToString(), reader.GetValue(i).ToString());

                            //needs a property name and a property value
                        }
                    returnThese.Add(newTrack);
                }

     
                
            }

        connection.Close();

        return returnThese;
    }
}
}



    
