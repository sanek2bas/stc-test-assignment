using BuildUtility.Entity;
using BuildUtility.Extensions;

namespace BuildUtility.Parser
{
    public sealed class FileParser : IFileParser<Build>
    {
        private const char colon = ':';
        private readonly FileReader _fileReader;
        private string? _readedLine;

        public FileParser()
        {
            _fileReader = new FileReader();
        }

        public IEnumerable<Build> Parse(string fileName)
        {
            _fileReader.Start(fileName);

            Build? nextItem;
            while ((nextItem = tryGetNextItem()) != null)
                yield return nextItem;

            _fileReader.Stop();
        }

        private Build? tryGetNextItem()
        {
            if (string.IsNullOrEmpty(_readedLine))
            {
                _readedLine = _fileReader.ReadLine();
                if (string.IsNullOrEmpty(_readedLine))
                    return null;
            }

            if (lineContainsTabOrSpaceInFirstChar(_readedLine))
                throw new Exception();

            var parsedLineWithBuildName = parseLineWithBuildName(_readedLine);

            var build = new Build(parsedLineWithBuildName.BuildName);
            foreach (var dependencyBuildName in parsedLineWithBuildName.DependencyBuildNames)
                build.Dependencies.Add(dependencyBuildName);

            while ((_readedLine = _fileReader.ReadLine()) != null)
            {
                if (!lineContainsTabOrSpaceInFirstChar(_readedLine))
                    break;
                if (!lineContainsOnlyOneWord(_readedLine, out var action))
                    throw new Exception();
                build.Actions.Add(action);
            }

            return build;
        }


        private bool lineContainsTabOrSpaceInFirstChar(string line)
        {
            return char.IsWhiteSpace(line.First());
        }

        private (string BuildName, List<string> DependencyBuildNames) parseLineWithBuildName(string line)
        {
            var splitedLine = line.Split();
            var buildName = splitedLine[0];
            var isColonBuildNameLastChar = buildName.Last() == colon;

            if (string.IsNullOrEmpty(buildName) ||
                splitedLine.Length == 1 && isColonBuildNameLastChar ||
                splitedLine.Length > 1 && !isColonBuildNameLastChar)
                throw new Exception();

            if (isColonBuildNameLastChar)
                buildName = buildName.RemoveLastChar();

            return new(buildName, new List<string>(splitedLine.Skip(1)));
        }

        private bool lineContainsOnlyOneWord(string line, out string parsedOneWord)
        {
            parsedOneWord = string.Empty;
            var splitedLine = line.Trim()
                                  .Split()
                                  .Select(i => i.RemoveTab())
                                  .Select(i => i.RemoveSpace())
                                  .Where(i => i.Length > 0);

            if (splitedLine.Count() != 1)
                return false;
            parsedOneWord = splitedLine.First();
            return true;
        }
    }
}
