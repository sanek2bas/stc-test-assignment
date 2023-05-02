using BuildUtility.Entity;
using BuildUtility.Exceptions;
using BuildUtility.Extensions;

namespace BuildUtility.Parser
{
    public sealed class FileParser : IFileParser<Build>
    {
        private const char colon = ':';
        private readonly FileReader fileReader;
        private string? readedLine;
        private int linesCount;

        public FileParser()
        {
            fileReader = new FileReader();
        }

        public IEnumerable<Build> Parse(string fileName)
        {
            fileReader.Start(fileName);
            Build? nextItem;
            while ((nextItem = tryGetNextItem()) != null)
                yield return nextItem;
            Close();
        }

        public void Close()
        {
            fileReader.Stop();
            linesCount = 0;
        }

        private Build? tryGetNextItem()
        {
            linesCount++;
            if (string.IsNullOrEmpty(readedLine))
            {
                readedLine = fileReader.ReadLine();
                if (string.IsNullOrEmpty(readedLine))
                    return null;
            }

            if (lineContainsTabOrSpaceInFirstChar(readedLine))
                throw new InvalidFileDataException($"Line contains tab or space in first char: {linesCount} line");

            var parsedLineWithBuildName = parseLineWithBuildName(readedLine);

            var build = new Build(parsedLineWithBuildName.BuildName);
            foreach (var dependencyBuildName in parsedLineWithBuildName.DependencyBuildNames)
                build.Dependencies.Add(dependencyBuildName);

            while ((readedLine = fileReader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(readedLine) || string.IsNullOrWhiteSpace(readedLine))
                    throw new InvalidFileDataException($"File contains empty line: {linesCount} line");
                if (!lineContainsTabOrSpaceInFirstChar(readedLine))
                    break;
                if (!lineContainsOnlyOneWord(readedLine, out var action))
                    throw new InvalidFileDataException($"Build hasn't contain action(s): {linesCount} line");
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
            var splitedLine = line.Split()
                                  .Where(s => !string.IsNullOrEmpty(s))
                                  .ToArray();
            var buildName = splitedLine[0];
            var isColonBuildNameLastChar = buildName.Last() == colon;

            if (string.IsNullOrEmpty(buildName) ||
                splitedLine.Length == 1 && isColonBuildNameLastChar ||
                splitedLine.Length > 1 && !isColonBuildNameLastChar)
                throw new InvalidFileDataException($"Invalid line with build's name: {linesCount} line");

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
