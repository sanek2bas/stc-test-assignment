namespace BuildUtility.Parser
{
    public interface IFileParser<T>
    {
        IEnumerable<T> Parse(string fileName);
    }
}
