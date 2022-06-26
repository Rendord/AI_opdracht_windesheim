using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using SmartZombies.Classes;

public class Graph : Node
{
	public static readonly double INF = System.Double.MaxValue;
	public Dictionary<Vector2, Vertex> Vertices;
	private int spacing;
	private Vector2 start;
	private Vertex Startingnode;
	private Vertex Endnode;
	private RayCast2D edgeChecker;
	private List<Vector2> directions;

	public Graph(Vector2 start, int spacing, RayCast2D edgeChecker)
	{
		Vertices = new Dictionary<Vector2, Vertex>();
		this.start = start;
		this.spacing = spacing;
		this.edgeChecker = edgeChecker;
		directions = new List<Vector2>
		{
			new Vector2(0,spacing), new Vector2(spacing, 0), new Vector2(0,-spacing),new Vector2(-spacing,0)
		};
	}

	public void MakeGraph()
	{
		Queue<Vertex> vertices = new Queue<Vertex>();
		vertices.Enqueue(getVertex(start));
		while (vertices.Count != 0)
		{
			Vertex vertex = vertices.Dequeue();
			List<Vector2> neighbouring_positions = vertex.CheckNeighbours(edgeChecker, directions);
			foreach (Vector2 pos in neighbouring_positions)
			{
				if (!Vertices.ContainsKey(pos))
				{
					vertices.Enqueue(getVertex(pos));
				}

				AddEdge(vertex.position, pos, (vertex.position - pos).Length());
			}
		}
		
	}

	public Vertex getVertex(Vector2 position)
	{
		if (Vertices.ContainsKey(position))
		{
			return Vertices[position];
		}
		else
		{
			Vertices.Add(position, new Vertex(position));
			return Vertices[position];
		}
	}

	public void AddEdge(Vector2 source, Vector2 destination, double cost)
	{
		Vertex v = getVertex(source);
		Edge e = new Edge(getVertex(destination), cost);
		v.adjacent.Add(e);
	}

//	public void AStar(Vertex startnode, Vertex endnode)
//	{
//		foreach (var vertex in Vertices)
//		{
//			vertex.Reset()
//		}
//
//		Startingnode = startnode;
//		Endnode = endnode;
//
//	}
	

	private bool EvaluateStart()
	{
		edgeChecker.Position = start;
		edgeChecker.CastTo = start;
		edgeChecker.ForceRaycastUpdate();
		if (edgeChecker.IsColliding())
		{
			Console.WriteLine("invalid start");
			return false;
		}
		else
		{
			return true;
		}
	}

}
