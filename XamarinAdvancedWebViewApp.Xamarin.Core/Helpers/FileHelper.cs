using System;
namespace XamarinAdvancedWebViewApp.Xamarin.Core.Helpers
{
    /**
     * 
     * Purpose of this interface is to abstract the platform specific functionality.
     * This interface will be mainly implemented related to SQLite DB implementation
     *
     */
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
