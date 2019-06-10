using DesafioSouthSystem.Helper;
using System;
using System.Configuration;
using System.IO;

namespace DesafioSouthSystem
{
    public class MonitorConfig
    {
        public static void RegisterWatchers()
        {
            var fsw = new FileSystemWatcher
            {
                Filter = "*.dat",
                Path = $"{Environment.GetEnvironmentVariable("HOMEPATH")}{ConfigurationManager.AppSettings["InDirectory"]}",
                EnableRaisingEvents = true,
                IncludeSubdirectories = false
            };

            fsw.Created += new FileSystemEventHandler(OnCreated);
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            ProcessDatFile.ReadAndWriteDatFile(e.FullPath);
        }
    }
}
