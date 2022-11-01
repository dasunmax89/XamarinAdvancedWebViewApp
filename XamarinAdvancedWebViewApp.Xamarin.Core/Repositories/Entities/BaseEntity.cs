using System;
using SQLite;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Models
{
    public class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int Status { get; set; }
    }
}
