extends State
class_name WanderState

onready var Zombie = owner

func Enter() -> void:
	Zombie.max_speed = 30;
	print("wander enter")

func Do() -> void:
	Zombie.steering += Zombie.WanderBehaviour(50)
	if Zombie.LineOfSight.get_collider() == Zombie.target:
		stateMachine.SwitchState("Pursuit")
	

func Exit() -> void:
	print("wander exit")
	
func get_class() -> String:
	return "WanderState"


