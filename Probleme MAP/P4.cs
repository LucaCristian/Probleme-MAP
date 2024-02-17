using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_MAP
{
    public class P4
    {
        public void Start()
        {
            Graph graph = new Graph();

            int n = 5;
            int m = 7;

            for (int i = 0; i < n; i++)
            {
                Vertex vertex = new Vertex { Id = i };
                graph.Vertices.Add(vertex);
            }

            int[,] edges = new int[,] {
            {0, 1},
            {0, 2},
            {1, 2},
            {1, 3},
            {1, 4},
            {2, 3},
            {3, 4}
        };

            for (int i = 0; i < m; i++)
            {
                int v1Index = edges[i, 0];
                int v2Index = edges[i, 1];

                Edge edge = new Edge
                {
                    V1 = graph.Vertices[v1Index],
                    V2 = graph.Vertices[v2Index]
                };

                graph.Edges.Add(edge);
            }

            int[,] adjacencyMatrix = new int[n, n];
            foreach (var edge in graph.Edges)
            {
                adjacencyMatrix[edge.V1.Id, edge.V2.Id] = 1;
                adjacencyMatrix[edge.V2.Id, edge.V1.Id] = 1;
            }

            List<Edge> removedEdges = new List<Edge>();

            for (int i = 0; i < m; i++)
            {
                int v1Index = edges[i, 0];
                int v2Index = edges[i, 1];

                adjacencyMatrix[v1Index, v2Index] = 0;
                adjacencyMatrix[v2Index, v1Index] = 0;

                if (!IsConnected(adjacencyMatrix, n))
                {
                    adjacencyMatrix[v1Index, v2Index] = 1;
                    adjacencyMatrix[v2Index, v1Index] = 1;
                }
                else
                {
                    removedEdges.Add(graph.Edges[i]);
                }
            }

            Console.WriteLine("Muchiile eliminate:");
            foreach (var edge in removedEdges)
            {
                Console.WriteLine($"({edge.V1.Id}, {edge.V2.Id})");
            }

            Console.WriteLine("\nMatricea de adiacenta a grafului parțial obtinut:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(adjacencyMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static bool IsConnected(int[,] adjacencyMatrix, int n)
        {
            bool[] visited = new bool[n];
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(0);
            visited[0] = true;

            while (queue.Count > 0)
            {
                int currentVertex = queue.Dequeue();
                for (int i = 0; i < n; i++)
                {
                    if (adjacencyMatrix[currentVertex, i] == 1 && !visited[i])
                    {
                        visited[i] = true;
                        queue.Enqueue(i);
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
