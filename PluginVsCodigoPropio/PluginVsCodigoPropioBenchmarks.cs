using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using PluginVsCodigoPropio.DiegoDev;
using PluginVsCodigoPropio.Plugin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PluginVsCodigoPropio
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class PluginVsCodigoPropioBenchmarks
    {
        private const string Path = "C:\\csv\\ceseve.csv";
        private static readonly PluginParser Plugin= new PluginParser();
        private static readonly DiegoDevParser DiegoDev= new DiegoDevParser();


        [Benchmark]
        public async Task PluginTest()
        {
            await Plugin.Parse(Path);
        }

        [Benchmark]
        public async Task DiegoDevTest()
        {
            await DiegoDev.Parse(Path);
        }
    }
}
