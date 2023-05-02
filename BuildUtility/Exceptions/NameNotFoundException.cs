namespace BuildUtility.Exceptions
{
    public sealed class NameNotFoundException : Exception
    {
        private const string NameWasNotFound = "Name was not found";

        public NameNotFoundException() :
               base($"{NameWasNotFound}.")
        {
        }

        public NameNotFoundException(string message) :
               base($"{NameWasNotFound}: " + message)
        {
        }

        public NameNotFoundException(string message, Exception inneException) :
               base($"{NameWasNotFound}: " + message, inneException)
        {
        }
    }
}
