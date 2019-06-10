using DesafioSouthSystem.Helper;
using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace DesafioSouthSystem.Controllers
{
    public class HomeController : Controller
    {
        private string pathDatFiles = $"{Environment.GetEnvironmentVariable("HOMEPATH")}{ConfigurationManager.AppSettings["InDirectory"]}";
        public ActionResult Index()
        {
            ProcessFiles(pathDatFiles);
            ViewBag.PathFiles =  pathDatFiles;

            return View();
        }

        private static void ProcessFiles(string pathDatFiles)
        {
            

            string[] diretorios = Directory.GetDirectories(pathDatFiles);
            string[] files = Directory.GetFiles(pathDatFiles);

            foreach (string file in files)
            {
                if (Path.GetExtension(file) == ".dat")
                    ProcessDatFile.ReadAndWriteDatFile(file);
            }
        }
    }
}