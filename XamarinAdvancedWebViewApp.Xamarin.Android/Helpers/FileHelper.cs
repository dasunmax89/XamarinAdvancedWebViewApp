using System;
using System.IO;
using XamarinAdvancedWebViewApp.Xamarin.Core.Helpers;
using XamarinAdvancedWebViewApp.Xamarin.Droid.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]  
namespace XamarinAdvancedWebViewApp.Xamarin.Droid.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
