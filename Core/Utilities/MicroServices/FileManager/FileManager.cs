using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.MicroServices.FileManager {
    public static class FileManager {
        public static void Create(IFormFile file,string path) {
            using (Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write)) {
                file.CopyTo(fileStream);
            }
        }

        public static void Delete(string path) {
            File.Delete(path);
        }

        public static Stream GetFileStream(string path) {
            using (Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write)) {
                return fileStream;
            }
        }
    }
}
