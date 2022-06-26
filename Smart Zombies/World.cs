using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using SmartZombies.Scenes;


namespace SmartZombies
{
	public class World : Node2D
	{
		public Godot.Collections.Array Obstacles = new Godot.Collections.Array();
		public Graph worldGraph;
		public RayCast2D edgeChecker;
		public Node2D graphRepresentation;
		public PackedScene vertexScene;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			vertexScene = (PackedScene)ResourceLoader.Load("res://Scenes/Graph/Node.tscn");
			
			Obstacles.Add(new Obstacle(new Vector2(344,224), 40, "top1"));
			Obstacles.Add(new Obstacle(new Vector2(704,152), 48, "top2"));
			Obstacles.Add(new Obstacle(new Vector2(1128,168), 40, "top3"));
			Obstacles.Add(new Obstacle(new Vector2(1136,352), 40, "middle5"));
			Obstacles.Add(new Obstacle(new Vector2(1120,568), 32, "bottom3"));
			Obstacles.Add(new Obstacle(new Vector2(888,400), 72, "middle4"));
			Obstacles.Add(new Obstacle(new Vector2(628,384), 40, "middle3"));
			Obstacles.Add(new Obstacle(new Vector2(608,616), 32, "bottom2"));
			Obstacles.Add(new Obstacle(new Vector2(256,432), 40, "middle2"));
			Obstacles.Add(new Obstacle(new Vector2(384,432), 40, "middle1"));
			Obstacles.Add(new Obstacle(new Vector2(160,552), 32, "bottom1"));
			GetNode("Human").Set("Obstacles", Obstacles);
			//GetNode("Human").Set("Target", GetNode("Zombie"));
			edgeChecker = GetNode<RayCast2D>("Map/edgeChecker");
			graphRepresentation = GetNode<Node2D>("Map/graphRepresentation");

			worldGraph = new Graph(new Vector2(24, 24), 16, edgeChecker);
			worldGraph.MakeGraph();
			foreach (var vertex in worldGraph.Vertices.Values)
			{
				Node2D vertexNode = (Node2D)vertexScene.Instance();
				vertexNode.Position = vertex.position;
				graphRepresentation.AddChild(vertexNode);
			}
		}

		public override void _Draw()
		{
			foreach (var vertex in worldGraph.Vertices.Values)
			{
				foreach (var edge in vertex.adjacent)
				{
					DrawLine(vertex.position, edge.dest.position, Colors.Aqua);
				}
			}
		}
	}

	
}
