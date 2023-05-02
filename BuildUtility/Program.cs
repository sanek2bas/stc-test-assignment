using BuildUtility.Entity;
using BuildUtility.Parser;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.CompilerServices;

const string FILE_NAME = "makefile.txt";

var parser = new FileParser();
var t = parser.Parse(FILE_NAME);

Console.ReadKey();