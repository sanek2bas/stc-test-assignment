using BuildUtility.Structures;
using NUnit.Framework;

namespace BuildUtility.UnitTests
{
    [TestFixture]
    public class GraphTests
    {
        private const string NODE_1 = "NODE_1";
        private const string NODE_2 = "NODE_2";
        private const string NODE_3 = "NODE_3";

        private Dictionary<string, INode<string>> nodesDic;

        [SetUp]
        public void Setup()
        {
            nodesDic = new Dictionary<string, INode<string>>();
        }

        [Test]
        public void Correct_Simple_Graph_Test()
        {
            var node1 = createNode(NODE_1);
            var node2 = createNode(NODE_2);
            var node3 = createNode(NODE_3);
            node1.DependenciesKey.Add(node2.Key);
            node1.DependenciesKey.Add(node3.Key);
            node2.DependenciesKey.Add(node3.Key);

            var result = Graph<string>.DFS(node1, nodesDic);
            Assert.That(result[0], Is.EqualTo(node3));
            Assert.That(result[1], Is.EqualTo(node2));
            Assert.That(result[2], Is.EqualTo(node1));
        }

        private Node createNode(string key)
        {
            var node = new Node(key);
            nodesDic.Add(key, node);
            return node;
        }

        private class Node : INode<string>
        {
            public string Key { get; init; }

            public IList<string> DependenciesKey { get; init; }

            public Node(string key)
            {
                Key = key;
                DependenciesKey = new List<string>();
            }
        }
    }
}
