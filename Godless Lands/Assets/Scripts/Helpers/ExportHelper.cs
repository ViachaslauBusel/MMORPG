using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    internal class ExportHelper
    {

        public static void WriteToFile(string file, object value)
        {
            if(!Directory.Exists("Export"))
            {
                Directory.CreateDirectory("Export");
            }
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            File.WriteAllText($"Export/{file}.dat", JsonConvert.SerializeObject(value, settings));
        }
    }
}
