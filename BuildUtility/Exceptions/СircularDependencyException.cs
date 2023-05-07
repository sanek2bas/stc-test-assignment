
namespace BuildUtility.Exceptions
{
    public sealed class СircularDependencyException : Exception
    {
        private const string СircularDependency = "Сircular dependency";

        public СircularDependencyException() :
               base($"{СircularDependency}.")
        {
        }

        public СircularDependencyException(string message) :
               base($"{СircularDependency}: " + message)
        {
        }

        public СircularDependencyException(string message, Exception inneException) :
               base($"{СircularDependency}: " + message, inneException)
        {
        }
    }
}
