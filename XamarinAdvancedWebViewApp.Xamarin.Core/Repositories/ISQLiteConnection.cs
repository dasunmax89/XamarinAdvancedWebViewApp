using System;
using SQLite;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Repositories
{
    public interface ISQLiteConnection
    {
        SQLiteConnection GetConnection();
    }
}
