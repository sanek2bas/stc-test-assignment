using BuildUtility.Entity;
using BuildUtility.Parser;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.CompilerServices;

const string FILE_NAME = "makefile.txt";

if(File.Exists(FILE_NAME))
    File.Delete(FILE_NAME);
var fs = File.Create(FILE_NAME);
fs.Dispose();

using (var streamWriter = new StreamWriter(FILE_NAME))
{
    for (int i = 0; i < 1000000; i++)
        streamWriter.WriteLine(i);
}

_ = doMonitoring(FILE_NAME);

Console.ReadKey();

static async Task doMonitoring(string fileName)
{
    var sw = new Stopwatch();
    sw.Start();
    readFile(fileName);
    sw.Stop();
    Console.WriteLine($"Sync file read: {sw.Elapsed.TotalMilliseconds} ms");

    sw.Restart();
    await readFileAsync(fileName);
    sw.Stop();
    Console.WriteLine($"Async file read: {sw.Elapsed.TotalMilliseconds} ms");
}

static void readFile(string fileName)
{
    using (var streamReader = new StreamReader(fileName))
    {
        while (!streamReader.EndOfStream)
            streamReader.ReadLine();
    }
}

static async Task readFileAsync(string fileName)
{
    using (var streamReader = new StreamReader(fileName))
    {
        while (!streamReader.EndOfStream)
            await streamReader.ReadLineAsync();
    }
}
