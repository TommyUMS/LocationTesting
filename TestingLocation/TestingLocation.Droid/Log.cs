using System.IO;
using System.Threading.Tasks;
using Android.OS;
using TestingLocation.Droid;
using TestingLocation.Interfaces;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(Log))]
namespace TestingLocation.Droid
{
    public class Log : ILog
    { 
        public void addText(string filename, string text)
        {
            
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            if (!File.Exists(filePath))
            {
                var textfile = File.Create(filePath);
                File.WriteAllText(filePath, "\n" + text);
               
            }
            else
                File.WriteAllText(filePath, text);


        }
        public string LoadText(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            return System.IO.File.ReadAllText(filePath);
        }
    }
}