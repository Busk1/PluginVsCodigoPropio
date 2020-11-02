using PluginVsCodigoPropio.Plugin;
using PluginVsCodigoPropio.DiegoDev;
using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Running;
using System.Threading.Tasks;
using System.Collections.Generic;
using PluginVsCodigoPropio.Models;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PluginVsCodigoPropio
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Ejemplo 1
            //BenchmarkRunner.Run<PluginVsCodigoPropioBenchmarks>();

            //Ejemplo 2
            BenchmarkRunner.Run<NewtonsoftVsTextJsonBenchmark>();
            //var path = "C:\\csv\\json.txt";
            //var json = File.ReadAllText(path);
            //var result1 = JsonConvert.DeserializeObject<object>(json);
            //var result2 = System.Text.Json.JsonSerializer.Deserialize(json, typeof(object));
        }
    }
}
