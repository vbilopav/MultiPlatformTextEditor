using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MultiPlatformTextEditorCore
{
    [Route("api/file")]
    public class FileController : Controller
    {
        private static FileInfo Info { get; set; }
        private static readonly object LockObject = new object();

        public static void Init(string fileName)
        {
            Info = new FileInfo(fileName);
        }

        [HttpGet]
        public string Get()
        {
            this.Response.Headers.Add("x-file-name", Info.Name);
            this.Response.Headers.Add("x-full-name", Info.FullName);

            lock (LockObject)
            {
                return System.IO.File.ReadAllText(Info.Name);
            }
        }

        [HttpPost]
        public async Task Post()
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var content = await reader.ReadToEndAsync();
            lock (LockObject)
            {
                System.IO.File.WriteAllText(Info.Name, content);
            }
        }
    }
}