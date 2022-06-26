class_name MyWorld extends Node2D

var Obstacles = []
var Graph
onready var edgeChecker = get_node("Map/edgeChecker")

func _ready():
	var GraphScript = load("res://Classes/Graph.cs")
	var VertexScript = load("res://Classes/Vertex.cs")
	var EdgeScript = load("res://Classes/Edge.cs")
	Obstacles.append(Obstacle.new(Vector2(344,224), 40, "top1"));
	Obstacles.append(Obstacle.new(Vector2(704,152), 48, "top2"));
	Obstacles.append(Obstacle.new(Vector2(1128,168), 40, "top3"));
	Obstacles.append(Obstacle.new(Vector2(1136,352), 40, "middle5"));
	Obstacles.append(Obstacle.new(Vector2(1120,568), 32, "bottom3"));
	Obstacles.append(Obstacle.new(Vector2(888,400), 72, "middle4"));
	Obstacles.append(Obstacle.new(Vector2(628,384), 40, "middle3"));
	Obstacles.append(Obstacle.new(Vector2(608,616), 32, "bottom2"));
	Obstacles.append(Obstacle.new(Vector2(256,432), 40, "middle2"));
	Obstacles.append(Obstacle.new(Vector2(384,432), 40, "middle1"));
	Obstacles.append(Obstacle.new(Vector2(160,552), 32, "bottom1"));
	
	Graph = GraphScript.new(Vector2(24,24), 8, edgeChecker)
	Graph.MakeGraph()

	
	get_node("Human").Obstacles = Obstacles
	get_node("Human").target = get_node("Zombie")
