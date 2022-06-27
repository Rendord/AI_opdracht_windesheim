using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using Godot;
using Godot.Collections;
using SmartZombies.FuzzyLogic;
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
		public FuzzyModule FuzzyModule;
		public bool FuzzyInitialized;

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
				vertexNode.Name = vertex.position.ToString();
				graphRepresentation.AddChild(vertexNode);
			}

			Random rand = new Random(); 
			var start = worldGraph.Vertices.ElementAt((rand.Next(0, worldGraph.Vertices.Count))).Value;
			var end = worldGraph.Vertices.ElementAt((rand.Next(0, worldGraph.Vertices.Count))).Value;
			Console.WriteLine(start.position);
			Console.WriteLine(end.position);
			Console.WriteLine("beep");
			var result  = worldGraph.AStar(start, end);
			var path = result.Item1;
			path.Add(start);
			path.Add(end);

			var considered = result.Item2;
			considered.ForEach(vertex =>
			{
				var vertexRep = graphRepresentation.GetNode<Node2D>(vertex.position.ToString());
				vertexRep.Scale = new Vector2(3, 3);
			});
			
			path.ForEach(vertex =>
			{
				var vertexRep = graphRepresentation.GetNode<Node2D>(vertex.position.ToString());
				vertexRep.Scale = new Vector2(5, 5);
			});
			
			
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

		public void InitializeFuzzyModule()
		{
			FuzzyModule = new FuzzyModule();
			FuzzyVariable Aggression = FuzzyModule.CreateFLV("Aggression");
			FzSet Calm = Aggression.AddLeftShoulderSet("Calm", 0, 25, 50);
			FzSet Alert = Aggression.AddTriangularSet("Alert", 25, 50, 75);
			FzSet Aggresive = Aggression.AddRightShoulderSet("Aggressive", 50, 75, 100);
			FuzzyVariable DistanceToTarget = FuzzyModule.CreateFLV("DistanceToTarget");
			FzSet Close = DistanceToTarget.AddLeftShoulderSet("Close", 0, 60, 120);
			FzSet MediumFar = DistanceToTarget.AddTriangularSet("MediumFar", 60, 120, 300);
			FzSet Far = DistanceToTarget.AddRightShoulderSet("Far", 120, 300, 1000);
			FuzzyVariable TimeSinceLastSeen = FuzzyModule.CreateFLV("TimeSinceLastSeen");
			FzSet Short = TimeSinceLastSeen.AddLeftShoulderSet("Short", 0, 0, 15);
			FzSet Medium = TimeSinceLastSeen.AddTriangularSet("Medium", 0, 15, 30);
			FzSet Long = TimeSinceLastSeen.AddRightShoulderSet("Long", 15, 30, 30);
			
			FuzzyModule.AddRule(new FuzzyAnd(Short,Close),Aggresive);
			FuzzyModule.AddRule(new FuzzyAnd(Short,MediumFar),Aggresive);
			FuzzyModule.AddRule(new FuzzyAnd(Short,Far),Alert);
			FuzzyModule.AddRule(new FuzzyAnd(Medium,Close),Alert);
			FuzzyModule.AddRule(new FuzzyAnd(Medium,MediumFar),Alert);
			FuzzyModule.AddRule(new FuzzyAnd(Medium,Far),Calm);
			FuzzyModule.AddRule(new FuzzyAnd(Long,Close),Alert);
			FuzzyModule.AddRule(new FuzzyAnd(Long,MediumFar),Calm);
			FuzzyModule.AddRule(new FuzzyAnd(Long,Far),Calm);

			FuzzyInitialized = true;	
		}

		public double CalculateAggression(FuzzyModule FM, double distance, double lastTimeSeen)
		{
			if (FuzzyInitialized)
			{
				FM.Fuzzify("DistanceToTarget", distance);
				FM.Fuzzify("TimeSinceLastSeen", lastTimeSeen);

				return FM.DeFuzzify("Aggresion");
			}
			else
			{
				return 0;
			}

		}
	}

	
}
