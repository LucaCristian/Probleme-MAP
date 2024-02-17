using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_MAP
{
    internal class P7
    {
        public void Kruskal(List<Edge> Drumuri, List<Vertex> Orase)
        {
            List<Edge> MST = new List<Edge>();

            List<Edge> SortedEdges = new List<Edge>(Drumuri);
            SortedEdges.Sort((x, y) => x.Weight.CompareTo(y.Weight));

            List<List<Vertex>> SubGraphs = new List<List<Vertex>>();
            MST.Add(SortedEdges.First());
            SubGraphs.Add(new List<Vertex> { SortedEdges.First().V1, SortedEdges.First().V2 });

            SortedEdges.Remove(SortedEdges.First());

            while (SortedEdges.Any())
            {
                if (SubGraphs.First().Count == Orase.Count)
                    break;

                Edge next = SortedEdges.First();
                SortedEdges.Remove(next);
                MST.Add(next);

                int A_ParentSubgraph = -1;
                int B_ParentSubgraph = -1;

                SubGraphs.ForEach((x) => {
                    if (x.Contains(next.V1))
                        A_ParentSubgraph = SubGraphs.IndexOf(x);
                    if (x.Contains(next.V2))
                        B_ParentSubgraph = SubGraphs.IndexOf(x);
                });
                if (A_ParentSubgraph == -1 && B_ParentSubgraph == -1)
                    SubGraphs.Add(new List<Vertex>() { next.V1, next.V2 });

                else if (A_ParentSubgraph != -1 && B_ParentSubgraph == -1)
                    SubGraphs[A_ParentSubgraph].Add(next.V2);
                else if (B_ParentSubgraph != -1 && A_ParentSubgraph == -1)
                    SubGraphs[B_ParentSubgraph].Add(next.V1);

                else if ((B_ParentSubgraph != -1 && A_ParentSubgraph != -1) && A_ParentSubgraph != B_ParentSubgraph)
                {
                    SubGraphs[A_ParentSubgraph] = SubGraphs[A_ParentSubgraph].Union(SubGraphs[B_ParentSubgraph]).ToList();
                    SubGraphs.Remove(SubGraphs[B_ParentSubgraph]);
                }
                else
                    MST.Remove(next);
            }

            int costMin = 0;

            foreach (Edge e in MST)
            {
                costMin += e.Weight;
                Console.WriteLine($"{e.V1.Id} {e.V2.Id}: {e.Weight}");
            }

            Console.WriteLine($"Cost Minim Dezapezire: {costMin}");
        }
    }
}
