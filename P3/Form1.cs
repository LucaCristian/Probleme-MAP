using Probleme_MAP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace P3
{
    public partial class Form1 : Form
    {
        private Graph Graph = new Graph();
        Stack<Vertex> stack = new Stack<Vertex>();
        List<List<Vertex>> vertices = null;
        int nodeSize = 40;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Graph.LoadFromFile("D:\\Stuff\\Coding\\Studies\\2023\\MAP2023\\Algoritmi\\Probleme MAP\\Probleme MAP\\graph.txt");
            vertices = P3Rez(Graph.Edges, Graph.Vertices);
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (var edge in Graph.Edges)
            {
                Point p1 = edge.V1.Point;
                Point p2 = edge.V2.Point;

                g.DrawLine(new Pen(Color.Black), p1, p2);

                int x = (p1.X + p2.X) / 2;
                int y = (p1.Y + p2.Y) / 2;
            }

            if (vertices == null)
            {
                foreach (var v in Graph.Vertices)
                {
                    g.FillEllipse(new SolidBrush(Color.Blue),
                        v.Point.X - nodeSize / 2, v.Point.Y - nodeSize / 2,
                        nodeSize, nodeSize);

                    g.DrawEllipse(new Pen(new SolidBrush(Color.Black)),
                        v.Point.X - nodeSize / 2, v.Point.Y - nodeSize / 2,
                        nodeSize, nodeSize);
                    g.DrawString(
                        v.Id.ToString(), new Font("Arial", 10),
                        new SolidBrush(Color.Black),
                        v.Point.X - nodeSize / 2 + 5, v.Point.Y - nodeSize / 2 + 5);
                }
            }
            else
            {
                Stack<Color> colors = new Stack<Color>();
                colors.Push(Color.Blue);
                colors.Push(Color.Red);
                colors.Push(Color.Green);
                colors.Push(Color.Yellow);
                colors.Push(Color.Lime);
                colors.Push(Color.Brown);
                colors.Push(Color.Pink);
                colors.Push(Color.Aqua);
                colors.Push(Color.Purple);
                colors.Push(Color.Black);

                foreach (List<Vertex> vertecs in vertices)
                {
                    Color c = colors.Pop();

                    foreach (Vertex v in vertecs)
                    {
                        if (v == null) continue;
                        g.FillEllipse(new SolidBrush(c),
                        v.Point.X - nodeSize / 2, v.Point.Y - nodeSize / 2,
                        nodeSize, nodeSize);

                        g.DrawEllipse(new Pen(new SolidBrush(c)),
                            v.Point.X - nodeSize / 2, v.Point.Y - nodeSize / 2,
                            nodeSize, nodeSize);
                    }
                }
            }
        }

        public bool Dfs(int curr, int des, List<List<int>> adj, List<int> vis)
        {
            if (curr == des)
            {
                return true;
            }
            vis[curr] = 1;
            foreach (var x in adj[curr])
            {
                if (vis[x] == 0)
                {
                    if (Dfs(x, des, adj, vis))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsPath(int src, int des, List<List<int>> adj)
        {
            var vis = new List<int>(adj.Count + 1);
            for (int i = 0; i < adj.Count + 1; i++)
            {
                vis.Add(0);
            }
            return Dfs(src, des, adj, vis);
        }

        public List<List<Vertex>> P3Rez(List<Edge> aa, List<Vertex> vv)
        {
            List<List<int>> a = new List<List<int>>();
            foreach (var edge in aa)
            {
                a.Add(new List<int> { edge.V1.Id, edge.V2.Id });
            }
            int n = a.Count;
            var ans = new List<List<int>>();

            var isScc = new List<int>(n + 1);
            for (int i = 0; i < n + 1; i++)
            {
                isScc.Add(0);
            }

            var adj = new List<List<int>>(n + 1);
            for (int i = 0; i < n + 1; i++)
            {
                adj.Add(new List<int>());
            }

            for (int i = 0; i < a.Count; i++)
            {
                adj[a[i][0]].Add(a[i][1]);
            }

            for (int i = 1; i <= n; i++)
            {
                if (isScc[i] == 0)
                {
                    var scc = new List<int>();
                    scc.Add(i);

                    for (int j = i + 1; j <= n; j++)
                    {
                        if (isScc[j] == 0 && IsPath(i, j, adj) && IsPath(j, i, adj))
                        {
                            isScc[j] = 1;
                            scc.Add(j);
                        }
                    }

                    ans.Add(scc);
                }
            }
            return ans.Select(vert =>
                vert.Select(v => vv.FirstOrDefault(vvi => v == vvi.Id)).ToList()
            ).ToList();
        }
    }
}
