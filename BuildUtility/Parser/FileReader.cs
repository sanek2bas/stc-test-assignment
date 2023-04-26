namespace BuildUtility.Parser
{
    public sealed class FileReader
    {
        private StreamReader? _streamReader;

        public void Start(string fileName)
        {
            Stop();
            _streamReader = new StreamReader(fileName);
        }

        public void Stop()
        {
            if (_streamReader == null)
                return;
            _streamReader.Dispose();
            _streamReader.Close();
            _streamReader = null;
        }

        public string? ReadLine()
        {
            if (_streamReader == null || _streamReader.EndOfStream)
                return null;
            return _streamReader.ReadLine();
        }
    }
}
