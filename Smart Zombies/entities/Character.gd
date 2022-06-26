class_name Character extends KinematicBody2D

export var max_speed : float = 50
export var mass : float = 10

var velocity : Vector2
var heading : Vector2
var radius : float = 8
onready var raycasts: Node2D = get_node("RayCasts")
var avoid_force = 10
var max_steering : float = 2.5

enum Decel {slow = 3, normal = 2, fast = 1}

func _physics_process(delta):
	if velocity.length_squared() > 0.0001:
		heading = velocity.normalized()
	
	velocity = velocity.clamped(max_speed)
	move_and_slide(velocity)

func SeekBehaviour(target : Vector2):
	var DesiredVelocity = Vector2()
	DesiredVelocity = (target - position).normalized() * max_speed
	return DesiredVelocity - velocity

func FleeBehaviour(target : Vector2):
	var DesiredVelocity = Vector2()
	DesiredVelocity = (position - target).normalized() * max_speed
	return DesiredVelocity - velocity

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

func avoid_obstacles_steering() -> Vector2:
	raycasts.rotation = velocity.angle()
	
	for raycast in raycasts.get_children():
		raycast.cast_to.x = velocity.length()
		if raycast.is_colliding():
			var obstacle = raycast.get_collider()
			if obstacle is PhysicsBody2D:
				print("obstacle")
				return (position + velocity - obstacle.position).normalized() * avoid_force
			else:
				return raycast.get_collision_normal() * avoid_force
				
	return Vector2.ZERO