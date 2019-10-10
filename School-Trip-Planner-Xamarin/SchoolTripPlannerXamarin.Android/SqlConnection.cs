using SQLite;
using System.IO;
using toddler_scan_xamarin.Contracts.Connections;
using toddler_scan_xamarin.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqlConnection))]
namespace toddler_scan_xamarin.Droid
{
    class SqlConnection : ISqlConnection
    {
        public SQLiteAsyncConnection Connection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "Teachers.sqldb");

            return new SQLiteAsyncConnection(path);
        }
    }
}