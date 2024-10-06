using System;
using System.Collections.Generic;

class Edge : IComparable<Edge>
{
    public int Src, Dest, Weight;

    public Edge(int src, int dest, int weight)
    {
        Src = src;
        Dest = dest;
        Weight = weight;
    }

    public int CompareTo(Edge other)
    {
        return this.Weight.CompareTo(other.Weight);
    }
}

class Graph
{
    int Vertices;
    List<Edge> Edges = new List<Edge>();

    public Graph(int vertices)
    {
        Vertices = vertices;
    }

    public void AddEdge(int src, int dest, int weight)
    {
        Edges.Add(new Edge(src, dest, weight));
    }

    public int Find(int[] parent, int i)
    {
        if (parent[i] == i)
            return i;
        return Find(parent, parent[i]);
    }

    public void Union(int[] parent, int[] rank, int x, int y)
    {
        int xRoot = Find(parent, x);
        int yRoot = Find(parent, y);

        if (rank[xRoot] < rank[yRoot])
            parent[xRoot] = yRoot;
        else if (rank[xRoot] > rank[yRoot])
            parent[yRoot] = xRoot;
        else
        {
            parent[yRoot] = xRoot;
            rank[xRoot]++;
        }
    }

    public void KruskalMST()
    {
        List<Edge> result = new List<Edge>();
        Edges.Sort();

        int[] parent = new int[Vertices];
        int[] rank = new int[Vertices];

        for (int i = 0; i < Vertices; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }

        foreach (Edge edge in Edges)
        {
            int x = Find(parent, edge.Src);
            int y = Find(parent, edge.Dest);

            if (x != y)
            {
                result.Add(edge);
                Union(parent, rank, x, y);
            }
        }

        Console.WriteLine("Árbol de Expansión Mínima (Kruskal):");
        foreach (var edge in result)
        {
            Console.WriteLine($"{edge.Src} -- {edge.Dest} == {edge.Weight}");
        }
    }
}

class Program
{
    static void Main()
    {
        Graph g = new Graph(5);

        // Agregar aristas (src, dest, peso)
        g.AddEdge(0, 1, 1);
        g.AddEdge(0, 2, 3);
        g.AddEdge(1, 3, 2);
        g.AddEdge(2, 3, 4);
        g.AddEdge(2, 4, 5);

        g.KruskalMST();
    }
}
