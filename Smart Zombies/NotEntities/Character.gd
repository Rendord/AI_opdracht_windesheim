class_name Character extends KinematicBody2D

export var max_speed : float = 50
export var mass : float = 10

var velocity : Vector2
var heading : Vector2
var radius : float = 8
onready var raycasts: Node2D = get_node("RayCasts")
var max_avoid_force = 100
var max_steering : float = 50;

enum Decel {slow = 3, normal = 2, fast = 1}

func _physics_process(delta):
	if velocity.length_squared() > 0.0001:
		heading = velocity.normalized()

	velocity = velocity.clamped(max_speed)
	move_and_slide(velocity)

func SeekBehaviour(target : Vector2):
	var DesiredVelocity = target - position
	return (DesiredVelocity - velocity).normalized() * max_steering

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

func ObstacleAvoidanceBehaviour() -> Vector2:
	raycasts.rotation = velocity.angle()

	for raycast in raycasts.get_children():
		raycast.force_raycast_update()
		var collisionVector = Vector2(velocity.length() * 2, raycast.cast_to.y);
		raycast.cast_to = collisionVector
		if raycast.is_colliding():

			var obstacle = raycast.get_collider()
			
			if obstacle is PhysicsBody2D:
				var avoid_force = 1.0 + (collisionVector - obstacle.global_position).length() / 1 + collisionVector.length()
				print("obstacle")
				var lateralForce = (raycast.get_collision_point() - obstacle.global_position) * avoid_force;

				lateralForce = lateralForce.clamped(max_avoid_force)
				return (velocity + lateralForce).normalized() * max_steering
			else:
				return raycast.get_collision_normal() * max_avoid_force

	return Vector2.ZERO
