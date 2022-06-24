extends KinematicBody2D

var velocity : Vector2
var mass : float
var max_speed : float = 500
var heading : Vector2
var tagged : bool
enum Decel {slow = 3, normal = 2, fast = 1}
var target = Vector2(640,310)


func _physics_process(delta):
	velocity += SeekBehaviour(target)
	velocity += ArriveBehaviour(target, Decel.fast)
	move_and_slide(velocity)

func SeekBehaviour(target : Vector2):
	var DesiredVelocity = Vector2()
	DesiredVelocity = (target - position).normalized() * max_speed
	return DesiredVelocity - velocity

func FleeBehaviour(target : Vector2):
	var DesiredVelocity = Vector2()
	DesiredVelocity = (position - target).normalized() * max_speed
	return DesiredVelocity - velocity

func ArriveBehaviour(target : Vector2, speed : int):
	var ToTarget = target - position
	var dist = ToTarget.length()
	
	if(dist > 0):
		var DecelerationTweaker = 0.3
		var calculated_speed = dist / (speed * DecelerationTweaker)
		calculated_speed = min(calculated_speed, max_speed)
		var DesiredVelocity = ToTarget * calculated_speed / dist
		return DesiredVelocity - velocity

func PursuitBehaviour(var evader):
	var ToEvader = evader.position - position
	var Relativeheading = self.heading.dot(evader.heading)
	if ToEvader.dot(self.heading) > 0 and Relativeheading < -0.95:
		return SeekBehaviour(target)

	var LookAheadTime = ToEvader.length() / max_speed + evader.velocity.length()
	return SeekBehaviour(evader.position + evader.velocity * LookAheadTime)

