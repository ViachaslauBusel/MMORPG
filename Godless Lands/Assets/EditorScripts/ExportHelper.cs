using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.EditorScripts
{
    internal class ExportHelper
    {

        public static void Write(string file, object value)
        {
            File.WriteAllText($"Export/{file}.dat", JsonConvert.SerializeObject(value));
        }
    }
}
