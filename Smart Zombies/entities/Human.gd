extends "Character.gd"

var Obstacles : Array
onready var Target : Character = get_parent().get_node("Zombie")

func _physics_process(_delta: float) -> void:
	var steering: Vector2 = Vector2.ZERO
	steering += HideBehaviour(Target, Obstacles)
	steering += avoid_obstacles_steering()
	steering = steering.clamped(max_steering)
	
	velocity += steering
	
	._physics_process(_delta)

func get_hiding_position(posOb : Vector2, radiusOb : float, posTarget: Vector2) -> Vector2:
	var distanceFromBoundary : float = 30
	var distAway : float = radiusOb + distanceFromBoundary
	var toOb : Vector2 = (posOb - posTarget).normalized()
	return (toOb * distAway) + posOb

func EvadeBehaviour(pursuer : Character):
	var toPursuer : Vector2 = pursuer.position - position
	var lookAheadTime : float = toPursuer.length() / max_speed + pursuer.velocity.length()
	return FleeBehaviour(pursuer.position + pursuer.velocity * lookAheadTime)

func HideBehaviour(target : Character, obstacles : Array) -> Vector2:
	var distToClosest : float = 100000
	var BestHidingSpot : Vector2 = Vector2.ZERO
	for obstacle in obstacles:
		var hidingSpot = get_hiding_position(obstacle.position, obstacle.radius, target.position)
		var dist : float = hidingSpot.distance_squared_to(position)
		if dist < distToClosest:
			distToClosest = dist
			BestHidingSpot = hidingSpot
	if(distToClosest == 100000):
		return EvadeBehaviour(target)
	else:
		return ArriveBehaviour(BestHidingSpot, Decel.fast)


