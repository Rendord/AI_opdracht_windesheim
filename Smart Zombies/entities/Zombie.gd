extends "Character.gd"

var rng = RandomNumberGenerator.new() 

var wanderJitter : float = 4
var wanderRadius : float = 5
var wanderPoint : Vector2

var target = Vector2(640,310)
onready var circle = get_node("Node2D/circle")
onready var wandercast = get_node("Wander")

func _ready():
	wanderPoint = position

func _physics_process(delta):
	var steering = Vector2.ZERO
	steering += WanderBehaviour(50)
	steering += avoid_obstacles_steering()
	steering.clamped(max_steering)
	velocity += steering
	._physics_process(delta)


func PursuitBehaviour(var evader):
	var ToEvader = evader.position - position
	var Relativeheading = self.heading.dot(evader.heading)
	if ToEvader.dot(self.heading) > 0 and Relativeheading < -0.95:
		return SeekBehaviour(target)

	var LookAheadTime = ToEvader.length() / max_speed + evader.velocity.length()
	return SeekBehaviour(evader.position + evader.velocity * LookAheadTime)

func WanderBehaviour(wanderDistance : float):
		var x = rng.randf_range(-1, 1) * wanderJitter
		var y = rng.randf_range(-1, 1) * wanderJitter

		var wanderDisplacement = Vector2(x, y)
		wanderPoint += wanderDisplacement;
		wanderPoint = wanderPoint.normalized();
		wanderPoint *= wanderRadius;
		circle.position = heading * wanderDistance;
		var wanderTarget = (heading * wanderDistance) + wanderPoint;
		wandercast.cast_to = wanderTarget;

		return wanderTarget;


