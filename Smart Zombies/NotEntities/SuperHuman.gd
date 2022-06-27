extends KinematicBody2D

var velocity : Vector2
var heading : Vector2
var MyPath : Array = []
var traversing = false;
var current_destination : Vector2 
export var max_speed : float = 50
export var mass : float = 10

func _input(event):
	if event is InputEventMouseButton:
		if not traversing:
			setupAstar(position, event.position)
			getNextNode()

func _ready():
	pass

func _physics_process(delta):
	if traversing:
		velocity += ArriveBehaviour(current_destination, 1)
		if(position.distance_to(current_destination) < 1):
			if(!MyPath.empty()):
				getNextNode()
			else:
				traversing = false;
		move_and_slide(velocity)
	
func setupAstar(from : Vector2, to : Vector2):
	MyPath = get_parent().CalculateAStarForPlayer(from, to);
	traversing = true;

func getNextNode():
	current_destination = MyPath.pop_front()
	print(current_destination)

func ArriveBehaviour(targetPosition : Vector2, speed : int) -> Vector2:
	var toTarget = targetPosition - position
	var dist = toTarget.length()

	if dist > 0:
		var decelerationTweaker : float = 0.3
		var calculatedSpeed = dist / (speed * decelerationTweaker)
		calculatedSpeed = min(calculatedSpeed, max_speed)
		var DesiredVelocity = toTarget * calculatedSpeed / dist
		return DesiredVelocity - velocity

	return Vector2.ZERO
	
func SeekBehaviour(target : Vector2):
	var DesiredVelocity = target - position
	return (DesiredVelocity - velocity).normalized() * max_speed
