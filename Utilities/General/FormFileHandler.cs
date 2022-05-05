using Microsoft.AspNetCore.Http;
using System.IO;

namespace GraduationProject.Utilities.General
{
    public static class FormFileHandler
    {
        public static byte[] ConvertToBytes(IFormFile file)
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            return stream.ToArray();
        }
    }
}
