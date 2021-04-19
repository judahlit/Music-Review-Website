﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB
{
    public class Song
    {
        #region Properties

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DateOfRelease { get; set; }

        public double Score { get; set; }

        public List<string> ArtistNames { get; set; } = new();

        public List<string> GenreNames { get; set; } = new List<string>();

        #endregion

        protected Song(string title, DateTime date, List<string> artistNames, List<string> genreNames)
        {
            Title = title;
            DateOfRelease = date;
            ArtistNames = artistNames;
            GenreNames = genreNames;
        }
    }
}
