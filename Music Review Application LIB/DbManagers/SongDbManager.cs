﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB.DbManagers
{
    public class SongDbManager
    {
        #region Constants and Fields

        private const string QueryAddSingleInSongTable = "INSERT INTO Song(title, dateOfRelease) OUTPUT INSERTED.id VALUES('{0}','{1}');";
        private const string QueryAddTrack = "INSERT INTO Song(title, dateOfRelease, trackId, albumId) VALUES('{0}','{1}',{2},{3});";
        private const string QueryAddSingle = "INSERT INTO Single(songId, img) VALUES({0},CONVERT(VARBINARY(MAX),'{1}'));";
        private const string QueryAddSongArtist = "INSERT INTO SongArtist(songId, artistId) VALUES({0}, {1});";
        private const string QueryAddSongGenre = "INSERT INTO SongGenre(songId, genre) VALUES({0},'{1}');";
        private const string QueryGetLatestSongId = "SELECT TOP 1 id FROM Song ORDER BY id DESC;";
        private const string QueryGetSongId = "SELECT Song.id FROM Song, songArtist WHERE Song.title = '{0}' AND Song.id = SongArtist.songId AND SongArtist.artistId = (SELECT id FROM Artist WHERE artistName = '{1}');";
        private const string QueryGetSongById = "SELECT * FROM Song WHERE id = {0};";
        private const string QueryGetSingleBySongId = "SELECT * FROM Single WHERE songId = {0};";
        private const string QueryGetSongArtistsBySongId = "SELECT * FROM SongArtist WHERE songId = {0};";
        private const string QueryGetSongGenresBySongId = "SELECT * FROM SongGenre WHERE songId = {0};";

        private const string QueryGetAllSongs = "";
        private const string QueryGetSortedSongs = "";
        private const string QueryUpdateScore = "";
        private const string QueryGetUserScore = "";
        private const string QueryDeleteSong = "DELETE FROM Song WHERE id = '{0}'";
        private const string QueryDeleteSingle = "DELETE FROM Single WHERE songId = '{0}'";
        private const string QueryDeleteSongArtist = "DELETE FROM SongArtist WHERE songId = '{0}'";
        private const string QueryDeleteSongGenre = "DELETE FROM SongGenre WHERE songId = '{0}'";

        private readonly AppManager _appManager = new();
        private readonly ArtistDbManager _artistDbManager = new();

        #endregion

        public SongDbManager()
        {

        }

        #region Methods

        public void AddSingle(SingleSong single)
        {
            CheckArtists(single);
            int songId = 0;

            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddSingleInSongTable, _appManager.GetSqlString(single.Title), single.DateOfRelease), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    if (reader.Read())
                    {
                        songId = reader.GetInt32(0);
                    }

                    reader.Close();
                }

                foreach (string artistName in single.ArtistNames)
                {
                    ArtistDbManager artistDbManager = new();

                    using (SqlCommand query = new SqlCommand(string.Format(QueryAddSongArtist, songId, artistDbManager.GetArtistId(artistName)), conn))
                    {
                        query.ExecuteNonQuery();
                    }
                }

                foreach (string genreName in single.GenreNames)
                {
                    using (SqlCommand query = new SqlCommand(string.Format(QueryAddSongGenre, songId, genreName), conn))
                    {
                        query.ExecuteNonQuery();
                    }
                }

                byte[] bytearray = _appManager.ImageToByteArray(single.Img);

                using (SqlCommand query = new SqlCommand(string.Format(QueryAddSingle, songId, bytearray), conn))
                {
                    query.ExecuteNonQuery();
                }

            }
        }

        private int GetLatestSongId()
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(QueryGetLatestSongId, conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();
                    return reader.GetInt32(0);
                }
            }
        }

        public void AddTrack(Song track)
        {

        }

        public int GetSongId(string title, List<string> artistNames)
        {
            ArtistDbManager artistDbManager = new();
            List<List<int>> songIds = new();
            int artistNumber = 0;

            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                foreach (string artistName in artistNames)
                {
                    using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongId, _appManager.GetSqlString(title), _appManager.GetSqlString(artistName)), conn))
                    {
                        conn.Open();
                        var reader = query.ExecuteReader();

                        if (reader.HasRows)
                        {
                            songIds.Add(new());
                        }

                        while (reader.Read())
                        {
                            songIds[artistNumber].Add(reader.GetInt32(0));
                        }

                        reader.Close();
                        conn.Close();
                    }

                    artistNumber++;
                }
            }

            if (songIds.Count != 0)
            {
                foreach (int songId in songIds[0])
                {
                    int amountOfASongId = 0;

                    for (int index = 1; index < songIds.Count; index++)
                    {
                        if (songIds.Count > index)
                        {
                            foreach (int aSongId in songIds[index])
                            {
                                if (aSongId == songId)
                                {
                                    amountOfASongId++;
                                }
                            }
                        }
                    }

                    if (amountOfASongId == songIds.Count - 1)
                    {
                        return songId;
                    }
                }
            }

            return 0;
        }
        
        public SingleSong GetSingle(int id)
        {
            string title = null;
            DateTime dateOfRelease = new();
            double score = 0;
            Image img = null;
            List<string> artistNames = new();
            List<string> genreNames = new();

            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongById, id), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        title = reader.GetString(1);
                        dateOfRelease = (DateTime)reader["dateOfRelease"];

                        if (reader["score"] != DBNull.Value)
                        {
                            score = (double)reader["score"];
                        }
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSingleBySongId, id), conn))
                {
                    var reader = query.ExecuteReader();

                    if (reader.Read())
                    {
                        img = _appManager.ByteArrayToImage((byte[])reader["img"]);
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongArtistsBySongId, id), conn))
                {
                    var reader = query.ExecuteReader();
                    ArtistDbManager artistDbManager = new();

                    while (reader.Read())
                    {
                        artistNames.Add(artistDbManager.GetArtist(reader.GetInt32(2)).ArtistName);
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongGenresBySongId, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        genreNames.Add(reader.GetString(2));
                    }

                    reader.Close();
                }
            }

            SingleSong single = new(title, dateOfRelease, img, artistNames, genreNames);
            single.Id = id;
            single.Score = score;

            return single;
        }

        /*
        public Song GetSong(string title, DateTime dateOfRelease)
        {

        }

        public List<Song> GetSongs()
        {
            
        }

        public List<Song> GetSong(string title, bool byDate, bool byScore, List<string> genres)
        {

        }

        public SongScore GetUserScore(Song song, string username)
        {

        }

        public void UpdateScore(int songId, SongScore userScore)
        {

        }*/

        public void DeleteSingle(int id)
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSongGenre, id), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSongArtist, id), conn))
                {
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSingle, id), conn))
                {
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSong, id), conn))
                {
                    query.ExecuteNonQuery();
                }
            }
        }

        private void CheckArtists(Song song)
        {
            foreach (string ArtistName in song.ArtistNames)
            {
                if (_artistDbManager.GetArtistId(ArtistName) == 0)
                {
                    Artist artist = new(ArtistName, null, null);
                    _artistDbManager.AddArtist(artist);
                }
            }
        }

        #endregion
    }
}
