using System;
using System.Threading.Tasks;
using XamarinAdvancedWebViewApp.Xamarin.Core.Models.Base;
using SQLite;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Models
{
    public class EnvironmentSetting : BaseEntity
    {
        public int UnreadMessagesCount { get; set; }

        public bool IsMessageDbBuilt { get; set; }

        public EnvironmentSetting()
        {

        }

        public static EnvironmentSetting GetDefault()
        {
            return new EnvironmentSetting()
            {
                IsMessageDbBuilt = false,
                UnreadMessagesCount = 0,
            };
        }
    }
}
