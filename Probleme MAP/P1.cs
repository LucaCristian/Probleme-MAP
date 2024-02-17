using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_MAP
{
    public class Graph
    {
        public List<Vertex> Vertices = new List<Vertex>();
        public List<Edge> Edges = new List<Edge>();
        public void LoadFromFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string data = sr.ReadLine();
            int n = int.Parse(data);
            for (int i = 0; i < n; i++)
            {
                data = sr.ReadLine();
                string[] tokens = data.Split(' ');
                Vertex v = new Vertex
                {
                    Id = int.Parse(tokens[0]),
                    Point = new System.Drawing.Point(
                        int.Parse(tokens[1]), int.Parse(tokens[2]))
                };
                Vertices.Add(v);
            }
            data = sr.ReadLine();
            n = int.Parse(data);
            for (int i = 0; i < n; i++)
            {
                data = sr.ReadLine();
                string[] tokens = data.Split(' ');
                Vertex v1 = Vertices.Where(v => v.Id == int.Parse(tokens[0])).FirstOrDefault();
                Vertex v2 = Vertices.Where(v => v.Id == int.Parse(tokens[1])).FirstOrDefault();
                Edge edge = new Edge
                {
                    V1 = v1,
                    V2 = v2
                };
                Edges.Add(edge);
            }
        }
    }
}
