using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.IO
{
    public class IOReader
    {/*
        public string GetOneLineFrom(string route, string filename)
         {
             route += filename;

             if (File.Exists(route))
             {
                 using (var stream = File.Open(route, FileMode.Open, FileAccess.Read, FileShare.None))
                 {
                     using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                     {
                         return reader.ReadString();
                     }
                 }
             }

             return "";

         }*/

        public static string GetOneLineFromFile(string path, string filename)
        {
            var myfiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList();

            foreach (string file in myfiles)
            {

                FileInfo mFile = new(file);

                if (mFile.Name.Equals(filename, StringComparison.OrdinalIgnoreCase))
                {
                    using var fs = mFile.OpenRead();

                    using var reader = new StreamReader(fs);

                    return reader.ReadToEnd();

                }
            }
            return "";
        }

        public static List<string> GetLinesFromFile(string path, string filename)
        {
            var myfiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList();
            List<string> lines = new();
            foreach (string file in myfiles)
            {

                FileInfo mFile = new(file);

                if (mFile.Name.Equals(filename, StringComparison.OrdinalIgnoreCase))
                {
                    using var fs = mFile.OpenRead();

                    using var reader = new StreamReader(fs);
                    
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }

                }
            }

            return lines;
        }


    }
}
