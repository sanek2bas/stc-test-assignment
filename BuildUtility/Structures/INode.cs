
namespace BuildUtility.Structures
{
    public interface INode<T>
    {
        T Key { get; }

        IList<T> DependenciesKey { get; }
    }
}
