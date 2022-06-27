extends State
class_name WanderState

onready var Zombie = owner

func Enter() -> void:
	print("wander enter")

func Do() -> void:
	Zombie.steering += Zombie.WanderBehaviour(50)
	stateMachine.SwitchState("Pursuit")

func Exit() -> void:
	print("wander exit")
	
func get_class() -> String:
	return "WanderState"


