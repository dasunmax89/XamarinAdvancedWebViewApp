﻿using System;
using System.IO;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Constants
{
    public static class DbConstants
    {
        public const string DatabaseFilename = "XamarinAdvancedWebViewApp.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.FullMutex |
            SQLite.SQLiteOpenFlags.SharedCache;
    }
}
