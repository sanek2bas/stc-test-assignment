namespace BuildUtility.Exceptions
{
    public class InvalidFileDataException : Exception
    {
        private const string InvalidFileData = "Invalid file data";

        public InvalidFileDataException() :
               base($"{InvalidFileData}.")
        {
        }

        public InvalidFileDataException(string message) : 
               base($"{InvalidFileData}: " + message)
        {
        }

        public InvalidFileDataException(string message, Exception inneException) :
               base($"{InvalidFileData}: " + message, inneException)
        {
        }
    }
}
