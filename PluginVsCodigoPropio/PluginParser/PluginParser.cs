using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;

namespace PluginVsCodigoPropio.Plugin
{
    public class PluginParser
    {
        [Benchmark]
        public async Task<List<Model>> Parse(string path)
        {
            var result = new List<Model>();
            using (TextReader fileReader = File.OpenText(path))
            {
                var csv = new CsvReader(fileReader, new CsvConfiguration(new CultureInfo("es")));
                csv.Configuration.HasHeaderRecord = false;
                csv.Read();
                result = csv.GetRecords<Model>().ToList();
            }
            return result;
        }
    }

}
