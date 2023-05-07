using BuildUtility.Exceptions;
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
        private const string NODE_4 = "NODE_4";

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

        [Test]
        public void Correct_Simple_Graph_Circular_Dependency_Test()
        {
            var node1 = createNode(NODE_1);
            var node2 = createNode(NODE_2);
            var node3 = createNode(NODE_3);
            node1.DependenciesKey.Add(node2.Key);
            node1.DependenciesKey.Add(node3.Key);
            node2.DependenciesKey.Add(node3.Key);
            node3.DependenciesKey.Add(node2.Key);

            Assert.Throws<СircularDependencyException>(() =>
                Graph<string>.DFS(node1, nodesDic));
        }

        private Node createNode(string key)
        {
            var node = new Node(key);
            nodesDic.Add(key, node);
            return node;
        }

        [Test]
        public void Correct_Difficult_Graph_Test()
        {
            var node1 = createNode(NODE_1);
            var node2 = createNode(NODE_2);
            var node3 = createNode(NODE_3);
            var node4 = createNode(NODE_4);
            node1.DependenciesKey.Add(node2.Key);
            node1.DependenciesKey.Add(node4.Key);
            node2.DependenciesKey.Add(node3.Key);
            node4.DependenciesKey.Add(node3.Key);

            var result = Graph<string>.DFS(node1, nodesDic);
            Assert.That(result[0], Is.EqualTo(node3));
            Assert.That(result[1], Is.EqualTo(node4));
            Assert.That(result[2], Is.EqualTo(node2));
            Assert.That(result[3], Is.EqualTo(node1));
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
