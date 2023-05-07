using BuildUtility.Entity;
using BuildUtility.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildUtility.Structures
{
    public sealed class Graph<T>
    {
        public static IList<INode<T>> DFS(INode<T> targetNode, Dictionary<T, INode<T>> nodesDic)
        {
            var resultList = new List<INode<T>>();
            var stack = new Stack<T>();
            var visited = new HashSet<T>();
            var completed = new HashSet<T>();

            stack.Push(targetNode.Key);

            while (stack.Count > 0)
            {
                var nodeKey = stack.Peek();
                visited.Add(nodeKey);
                var node = nodesDic[nodeKey];
                bool addedToStack = false;
                foreach (var dependency in node.DependenciesKey)
                {
                    if (visited.Contains(dependency))
                        throw new СircularDependencyException($"{node.Key}: {dependency}");
                    if (completed.Contains(dependency))
                        continue;
                    stack.Push(dependency);
                    addedToStack = true;
                }

                if (addedToStack)
                    continue;

                resultList.Add(node);
                completed.Add(nodeKey);
                visited.Remove(nodeKey);
                stack.Pop();
            }

            return resultList;
        }
    }
}
