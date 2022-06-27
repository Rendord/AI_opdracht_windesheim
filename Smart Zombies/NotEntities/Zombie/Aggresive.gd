extends State
class_name Aggressive

onready var Zombie = owner
onready var Human = get_tree().current_scene.get_node("Human")

func Enter() -> void:
	Zombie.max_speed = 75
	print("AgressiveState")
	
func Do() -> void:
	Zombie.steering += Zombie.WanderBehaviour(50)
	if Zombie.LineOfSight.get_collider() == Zombie.target:
		stateMachine.SwitchState("Pursuit")

func Exit() -> void:
	print("Aggresive exit")
	
func get_class() -> String:
	return "AggresiveState"
