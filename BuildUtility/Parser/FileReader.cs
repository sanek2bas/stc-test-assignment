namespace BuildUtility.Parser
{
    public sealed class FileReader
    {
        private StreamReader? streamReader;

        public void Start(string fileName)
        {
            Stop();
            streamReader = new StreamReader(fileName);
        }

        public void Stop()
        {
            if (streamReader == null)
                return;
            streamReader.Dispose();
            streamReader.Close();
            streamReader = null;
        }

        public string? ReadLine()
        {
            if (streamReader == null || 
                streamReader.EndOfStream)
                return null;
            return streamReader.ReadLine();
        }
    }
}
