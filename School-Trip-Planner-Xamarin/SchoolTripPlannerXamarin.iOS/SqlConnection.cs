using SQLite;
using System;
using System.IO;
using toddler_scan_xamarin.Contracts.Connections;
using toddler_scan_xamarin.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqlConnection))]
namespace toddler_scan_xamarin.iOS
{
    class SqlConnection : ISqlConnection
    {
        public SQLiteAsyncConnection Connection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "Teachers.sqldb");

            return new SQLiteAsyncConnection(path);
        }
    }
}