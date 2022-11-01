using System;
using System.IO;
using XamarinAdvancedWebViewApp.Xamarin.Core.Repositories;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Dependency;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDBConnection))]
namespace XamarinAdvancedWebViewApp.Xamarin.Droid.Dependency
{
    public class SQLiteDBConnection : ISQLiteConnection
    {
        public SQLiteDBConnection()
        {
        }

        public SQLiteConnection GetConnection()
        {
            string filename = "XamarinAdvancedWebViewApp.db3";

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbFilePath = Path.Combine(path, filename);

            return new SQLiteConnection(dbFilePath);
        }
    }
}
