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

            var targetBuildName = args[0];
            var parser = new FileParser();

            Build? targetBuild = null;
            var buildsDic = new Dictionary<string, INode<string>>();
            foreach(var build in parser.Parse(FILE_NAME))
            {
                if(build.Key == targetBuildName)
                    targetBuild = build;
                buildsDic.Add(build.Key, build);
            }
            
            if (targetBuild == null)
                throw new NameNotFoundException(targetBuildName);

            var orderedBuilds = Graph<string>.DFS(targetBuild, buildsDic);
            foreach(Build build in orderedBuilds.Cast<Build>())
            {
                
                foreach(var action in build.Actions)
                Console.WriteLine(action);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.WriteLine("Try again please...");
        }
     

        Console.ReadKey();
    }
}