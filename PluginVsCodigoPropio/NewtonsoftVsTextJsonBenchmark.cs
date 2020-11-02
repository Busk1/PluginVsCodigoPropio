using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PluginVsCodigoPropio
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    [HtmlExporter]
    public class NewtonsoftVsTextJsonBenchmark
    {
        private const string Path = "C:\\csv\\json.txt";
        public string Json { get; set; }
        public NewtonsoftVsTextJsonBenchmark()
        {
            Json = File.ReadAllText(Path);
        }

        [Benchmark]
        public void NewtonsoftTest()
        {
            JsonConvert.DeserializeObject<object>(Json);
        }

        [Benchmark]
        public void TextJsonTest()
        {
            System.Text.Json.JsonSerializer.Deserialize(Json, typeof(object));
        }
    }
}
