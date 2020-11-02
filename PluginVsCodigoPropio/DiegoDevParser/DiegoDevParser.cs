using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PluginVsCodigoPropio.DiegoDev
{
    public class DiegoDevParser
    {
        public async Task<List<Model>> Parse(string path)
        {
            var result = new List<Model>();
            var cancellationToken = new CancellationToken();
            using (StreamReader stream = new StreamReader(path))
            {
                var isHeader = true;
                var buffer = new byte[4096];
                var haveContent = true;
                var missingData = string.Empty;
                while (haveContent)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var read = await stream.BaseStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                    if (read == 0)
                        haveContent = false;
                    else
                    {
                        var data = new byte[read];
                        buffer.ToList().CopyTo(0, data, 0, read);
                        var dataString = System.Text.Encoding.UTF8.GetString(data);
                        await Task.Run(() =>
                        {
                            foreach (ReadOnlySpan<char> line in dataString.SplitLines('\n'))
                            {
                                var missingLine = !string.IsNullOrEmpty(missingData) ? missingData + line.ToString() : line;

                                if (isHeader) isHeader = false;
                                else
                                {
                                    try
                                    {
                                        var enumerator = missingLine.ToString().SplitLines(';').GetEnumerator();
                                        enumerator.MoveNext();
                                        var pointOfSale = enumerator.Current.Line;
                                        enumerator.MoveNext();
                                        var product = enumerator.Current.Line;
                                        enumerator.MoveNext();
                                        var date = enumerator.Current.Line;
                                        enumerator.MoveNext();
                                        var stock = enumerator.Current.Line;

                                        result.Add(new Model
                                        {
                                            PointOfSale = pointOfSale.ToString(),
                                            Date = Convert.ToDateTime(date.ToString()),
                                            Product = product.ToString(),
                                            Stock = Convert.ToInt32(stock.ToString())
                                        });
                                        missingData = string.Empty;
                                    }
                                    catch (Exception ex)
                                    {
                                        missingData = line.ToString();
                                    }
                                }
                            }
                        });
                    }
                }
            }
            return result;
        }
    }
}
