using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
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
	
	private float Heuristic(Vertex a, Vertex b)
	{
		return (float)Math.Sqrt((a.position.x - b.position.x) * (a.position.x - b.position.x) + (a.position.y - b.position.y) * (a.position.y - b.position.y));
	}

	private List<Vertex> constructPath(Dictionary<Vertex, Vertex> path, Vertex endNode)
	{
		List<Vertex> totalPath = new List<Vertex>();
		Vertex currentNode = endNode;
		while (path.Keys.Contains(currentNode))
		{
			currentNode = path[currentNode];
			totalPath.Insert(0, currentNode);
		}
		return totalPath;
	}
	
	public Tuple<List<Vertex>, List<Vertex>> AStar(Vertex startNode, Vertex endNode)
	{
		var openList = new List<Vertex>();
		var consideredList = new List<Vertex>();
		openList.Add(startNode);
		// path
		var path = new Dictionary<Vertex, Vertex>();
		var gScore = new Dictionary<Vertex, double>();
		gScore.Add(startNode, 0);
		var fScore = new Dictionary<Vertex, double>();
		fScore.Add(startNode, Heuristic(startNode, startNode));
		
		while (openList.Count > 0)
		{
			// select node with lowest fScore value
			Vertex currentNode = openList[0];
			double lowestF = fScore[currentNode];
			openList.ForEach(node =>
			{
				if (fScore[node] < lowestF)
				{
					lowestF = fScore[node];
					currentNode = node;
				}
			});
			// end if current is the end node
			if (currentNode == endNode)
			{
				return Tuple.Create(constructPath(path, endNode), consideredList);
			}
			openList.Remove(currentNode);
			consideredList.Add(currentNode);
			currentNode.adjacent.ForEach(edge =>
			{
				Vertex adjacentNode = edge.dest;
				double tentativeScore = gScore[currentNode] + edge.cost;
				if (!gScore.ContainsKey(adjacentNode) || tentativeScore < gScore[adjacentNode])
				{
					path[adjacentNode] = currentNode;
					gScore[adjacentNode] = tentativeScore;
					fScore[adjacentNode] = tentativeScore + Heuristic(adjacentNode, endNode);
					if (!openList.Contains(adjacentNode)) 
						openList.Add(adjacentNode);
				}
			});
		}
		// unable to find path
		return new Tuple<List<Vertex>, List<Vertex>>(openList, consideredList);
	}


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
