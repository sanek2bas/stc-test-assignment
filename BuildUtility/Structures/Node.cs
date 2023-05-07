
namespace BuildUtility.Structures
{
    public sealed class Node<T>
    {
        public T Value { get; init; }
        
        public List<Node<T>> Children { get; init; }

        public Node(T value)
        {
            Value = value;
            Children = new List<Node<T>>();
        }
    }
}
