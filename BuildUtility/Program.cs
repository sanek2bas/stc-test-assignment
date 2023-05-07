using BuildUtility.Entity;
using BuildUtility.Exceptions;
using BuildUtility.Parser;
using BuildUtility.Structures;

internal class Program
{
    private static void Main(string[] args)
    {
        const string FILE_NAME = "makefile.txt";

        try
        {
            if (args.Length != 1)
                throw new ArgumentException("You only need to provide a name of target build");
            
            var targetBuild = args[0];
            var parser = new FileParser();
            var builds = parser.Parse(FILE_NAME);
            
            if (builds.All(b => b.Name != targetBuild))
                throw new NameNotFoundException(targetBuild);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.WriteLine("Try again please...");
        }
     

        Console.ReadKey();
    }
}