using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizPersistence.DataStructures
{
    /// <summary>
    /// an acyclic oriented graph with insert() and randomTopologicalOrder() methods. The insert() method insured, that the graph is indeed still a DAG
    /// <example>
    /// For example, <c>q</c> being the empty <c>PriorityQueue</c>:
    /// <code>
    /// Dag result = new Dag();
    /// result.insert("q0", new string[0] );
    /// result.insert("q1", new string[] { "q0" } );
    /// result.insert("q2", new string[] { "q0", "q1" });
    /// result.insert("q3", new string[0]);
    /// var topologicalOrder = result.randomTopologicalOrder();
    /// </code>
    /// <c>topologicalOrder</c> will either be <c>List: { "q0", "q1", "q2", "q3" }</c> or <c>List: { "q3", "q0", "q1", "q2" }</c>, since those are the only topological orders, the provided graph has
    /// </example>
    /// </summary>
    public class Dag
    {
        private enum State { Unreached, Opened, Closed }
        // node_id: List_of_neighbours_ids
        private Dictionary<uint, List<uint>> neighbours = new Dictionary<uint, List<uint>>();
        // node_id: nodes_tier
        private Dictionary<uint, uint> tier = new Dictionary<uint, uint>();
        private List<uint> tierZero = new List<uint>();
        // nodeId: nodeId_inDegree
        private Dictionary<uint, CounterDefault<int>> inDegrees = new Dictionary<uint, CounterDefault<int>>();
        // node_name: node_id
        private List<string> nodeName = new List<string>();
        private Dictionary<string, uint> nodeIdByName = new Dictionary<string, uint>();

        Random rn = new Random();

        public void insert(string label, string[] nodePredecessors)
        {
            if (nodeIdByName.ContainsKey(label))
            {
                Console.WriteLine($"Label '{label}' was already found!");
                return;
            }

            uint nodeId = (uint)nodeName.Count;
            nodeName.Add(label);
            nodeIdByName.Add(label, nodeId);

            uint largestPreciedingTier = 0;
            uint predecessorTier = 0;
            neighbours.Add(nodeId, new List<uint>());
            inDegrees.Add(nodeId, new CounterDefault<int>(nodePredecessors.Length));

            foreach (string predecessorName in nodePredecessors)
            {
                uint predecessorId = nodeIdByName[predecessorName];
                if (tier.ContainsKey(predecessorId))
                {
                    predecessorTier = tier[predecessorId];
                }
                else
                {
                    throw new Exception("Node conected to unknown predecessor!");
                }
                largestPreciedingTier = (largestPreciedingTier < predecessorTier) ? predecessorTier : largestPreciedingTier;

                neighbours[predecessorId].Add(nodeId);
            }

            uint nodeTier = 0;
            if (nodePredecessors.Length > 0)
            {
                nodeTier = largestPreciedingTier + 1;
            } else
            {
                tierZero.Add(nodeId);
            }

            tier.Add(nodeId, nodeTier);
        }

        public List<string> randomTopologicalOrder()
        {
            int maxPriority = nodeName.Count * nodeName.Count;
            List<string> topologicalOrder = new List<string>();
            int rnNum;
            PriorityQueue<uint> q = new PriorityQueue<uint>();
            foreach (uint n in tierZero) 
            {
                rnNum = rn.Next(0, maxPriority);
                q.Enque(rnNum, n);
            }
            while (q.Count > 0)
            {
                uint currentNode = q.Pop();
                decreaceSuccessorsIndegrees(currentNode);
                topologicalOrder.Add(nodeName[(int)currentNode]);
                inDegrees[currentNode].ResetToDefault();
            }
            return topologicalOrder;


            void decreaceSuccessorsIndegrees(uint node)
            {
                for (int neighbourId = 0; neighbourId < neighbours[node].Count; neighbourId++)
                {
                    inDegrees[neighbours[node][neighbourId]].Counter -= 1;
                    if (inDegrees[neighbours[node][neighbourId]].Counter == 0)
                    {
                        rnNum = rn.Next(0, maxPriority);
                        q.Enque(rnNum, neighbours[node][neighbourId]);
                    }
                }
            }
        }

        private class CounterDefault<T> 
        {
            public T Counter;
            public T Default { get; }

            public CounterDefault(T value) 
            {
                Counter = value;
                Default = value;
            }

            public void ResetToDefault()
            {
                Counter = Default;
            }
        }
    }
}
