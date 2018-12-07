[assembly: Xamarin.Forms.Dependency(typeof(EnQentralov3.Droid.Implementations.PathService))]

namespace EnQentralov3.Droid.Implementations
{
    using Interfaces;
    using System;
    using System.IO;

    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "EnQentralov3.db3");
        }
    }
}
